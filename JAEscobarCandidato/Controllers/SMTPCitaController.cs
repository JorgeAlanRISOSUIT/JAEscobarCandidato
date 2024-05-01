using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using QRCoder.Extensions;
using QRCoder;

namespace JAEscobarCandidato.Controllers
{
    public class SMTPCitaController : Controller
    {
        // GET: SMTPCita
        public ActionResult FormProceso()
        {
            var resultEmpresa = BL.Empresa.GetAll();
            if (resultEmpresa.Item1)
            {
                var resultPostulacion = BL.Postulacion.GetAllProcesados(null);
                ML.Postulacion postulacion = resultPostulacion.Item1 ? resultPostulacion.Item4 : new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                postulacion.Vacante = new ML.Vacante();
                postulacion.Vacante.Empresa = resultEmpresa.Item4;
                return View(postulacion);
            }
            else
            {
                return View(resultEmpresa);
            }
        }

        public JsonResult CargarProcesados()
        {
            var resultEmpresa = BL.Empresa.GetAll();
            if (resultEmpresa.Item1)
            {
                var resultPostulaciones = BL.Postulacion.GetAllProcesados(null);
                ML.Postulacion postulacion = resultPostulaciones.Item1 ? resultPostulaciones.Item4 : new ML.Postulacion { Postulaciones = new List<ML.Postulacion>() };
                postulacion.Vacante = new ML.Vacante();
                postulacion.Vacante.Empresa = resultEmpresa.Item4;
                return Json(postulacion, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(resultEmpresa);
            }
        }

        public ActionResult Procesamiento(int idCandidato)
        {
            var result = BL.Postulacion.ProcesadoCandidato(idCandidato);
            if (result.Item1)
            {
                return View(result.Item4);
            }
            else
            {
                return RedirectToAction("FormProcesos");
            }
        }

        //[HttpPost]
        //public ActionResult Procesamiento(ML.Postulacion post)
        //{
        //    if (post != null)
        //    {
        //        string info = $"{post.Candidato.Nombre} {post.Candidato.ApellidoPaterno} {post.Candidato.ApellidoMaterno}\n" +
        //            $"{post.Candidato.FechaNacimiento} {post.Candidato.Email} {post.Candidato.Telefono}";
        //        QRCodeGenerator generator = new QRCodeGenerator();
        //        QRCodeData data = generator.CreateQrCode(info, QRCodeGenerator.ECCLevel.Q, true);
        //        PngByteQRCode png = new PngByteQRCode(data);
        //        var qRCodeByteArray = png.GetGraphic(20);
        //        QRCodeImage
        //        var smtpClien = new SmtpClient
        //        {
        //            Host = "smtp.gmail.com",
        //            UseDefaultCredentials = false,
        //            Port = 587,
        //            EnableSsl = true,
        //            Credentials = new NetworkCredential("rygenkio.minstar45@gmail.com", "imzq qzjd mdyq kajh")
        //        };
        //        var mail = new MailMessage
        //        {
        //            From = new MailAddress(post.Candidato.Email),
        //            Subject = "Seleccionado para proceso de " + post.Vacante.Nombre + " en la empresa " + post.Vacante.Empresa.Nombre,
        //            BodyEncoding = System.Text.Encoding.UTF8,
        //            Body = qr.GetGraphic(20, Color.Black, Color.White, true).
        //        };
        //        mail.To.Add(post.Candidato.Email);
        //        smtpClien.Send(mail);
        //        return View(post);
        //    }
        //    else
        //    {
        //        return RedirectToAction("FormProceso");
        //    }
        //}

        [HttpPost]
        public ActionResult Procesamiento(ML.Postulacion post)
        {
            //puedo generar el img pero en los correos no lo permite
            //se debe generar un campo de confirmación al correo para aceptar la información
            //una vez a dentro de la confirmación se genera el código qr pero ya debe de estar precargada con los datos
            if (post != null)
            {
                string html = string.Empty;
                string info = $"{Url.Action("Confirmacion", new { email = post.Candidato.Email, postulacion = post })}";
                QRCodeGenerator qrCodeGenerator = new QRCodeGenerator();
                QRCodeData data = qrCodeGenerator.CreateQrCode(info, QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode codeImg = new BitmapByteQRCode(data);
                using (var memory = new MemoryStream(codeImg.GetGraphic(40)))
                {
                    Attachment archivo = new Attachment(memory, $"QR{post.Candidato.Nombre}.jpeg", MediaTypeNames.Image.Jpeg);
                    archivo.ContentDisposition.Inline = true;
                    using (var reader = new StreamReader(Server.MapPath("~/Content/html/QRGenerator.html")))
                    {
                        html = reader.ReadToEnd();
                    }
                    html = html.Replace("{0}", archivo.ContentId);
                    var smtpClient = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        UseDefaultCredentials = false,
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("rygenkio.minstar45@gmail.com", "imzq qzjd mdyq kajh")
                    };
                    var mail = new MailMessage
                    {
                        From = new MailAddress("rygenkio.minstar45@gmail.com"),
                        Subject = "Candidato para seleccion de vacante: " + post.Vacante.Nombre,
                        IsBodyHtml = true,
                        Body = html,
                    };
                    mail.To.Add(post.Candidato.Email);
                    mail.Attachments.Add(archivo);
                    smtpClient.Send(mail);
                }
                ViewBag.Mensaje = "Mensaje enviado";
                return View(post);
            }
            else
            {
                return RedirectToAction("FormProceso");
            }
        }

        public ActionResult Confirmacion(string email, ML.Postulacion postulacion)
        {
            return View();
        }
    }
}
