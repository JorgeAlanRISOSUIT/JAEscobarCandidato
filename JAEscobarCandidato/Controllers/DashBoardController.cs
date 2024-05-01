using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace JAEscobarCandidato.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: DashBoard
        public ActionResult Index()
        {
            var result = BL.Postulacion.GetAll();
            if (result.Item1)
            {
                var resultEmpresa = BL.Empresa.GetAll();
                result.Item4.Vacante = new ML.Vacante();
                result.Item4.Vacante.Empresa = new ML.Empresa();
                result.Item4.Vacante.Empresa.Empresas = resultEmpresa.Item1 ? resultEmpresa.Item4.Empresas : new List<ML.Empresa>();
                return View(result.Item4);
            }
            else
            {
                return View(result);
            }
        }

        [HttpPost]
        public JsonResult VacanteEmpresa(int idEmpresa)
        {
            var result = BL.Vacante.EmpresaGetById(idEmpresa);
            return Json(result);
        }

        [HttpPost]
        public JsonResult SeleccionVacante(int idVacante)
        {
            var result = BL.Postulacion.VacanteGetById(idVacante);
            return Json(result);
        }

        public JsonResult SeleccionarTodos(int idVacante)
        {
            if (idVacante != 0)
            {
                var result = BL.Postulacion.VacanteGetById(idVacante);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = BL.Postulacion.GetAll();
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult SeleccionarPausados(int idVacante, int idPausados)
        {
            if (idVacante != 0)
            {
                var result = BL.Postulacion.VacanteGetById(idVacante, idPausados);
                return Json(result);
            }
            else
            {
                var result = BL.Postulacion.VacanteGetByIdPostulacion(idPausados);
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult SeleccionarProcesos(int idVacante, int idProcesos)
        {
            if (idVacante != 0)
            {
                var result = BL.Postulacion.VacanteGetById(idVacante, idProcesos);
                return Json(result);
            }
            else
            {
                var result = BL.Postulacion.VacanteGetByIdPostulacion(idProcesos);
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult SeleccionarDescartados(int idVacante, int idDescartados)
        {
            if (idVacante != 0)
            {
                var result = BL.Postulacion.VacanteGetById(idVacante, idDescartados);
                return Json(result);
            }
            else
            {
                var result = BL.Postulacion.VacanteGetByIdPostulacion(idDescartados);
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult SeleccionarAceptados(int idVacante, int idAceptados)
        {
            if (idVacante != 0)
            {
                var result = BL.Postulacion.VacanteGetById(idVacante, idAceptados);
                return Json(result);
            }
            else
            {
                var result = BL.Postulacion.VacanteGetByIdPostulacion(idAceptados);
                return Json(result);
            }
        }

        public ActionResult ArchivoMasivo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ArchivoMasivo(int? acceso)
        {
            var archivo = Request.Files["Candidatos"];
            if (Session["directorio"] == null)
            {
                if (archivo != null)
                {
                    if (archivo.ContentLength > 0)
                    {
                        var resultExcel = BL.CargaMáxima.ComprobarExtension(archivo.FileName);
                        if (resultExcel.Item1)
                        {
                            string path = @"\Candidatos\";
                            string nuevoArchivo = Server.MapPath(path) + Path.GetFileNameWithoutExtension(archivo.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                            if (!System.IO.File.Exists(nuevoArchivo))
                            {
                                archivo.SaveAs(nuevoArchivo);
                                string conexion = System.Configuration.ConfigurationManager.AppSettings["connectionExcel"].ToString() + nuevoArchivo;
                                var resultCandidato = BL.CargaMáxima.AddExcel(conexion);
                                if (resultCandidato.Item1)
                                {
                                    var resultCampos = BL.CargaMáxima.ValidarCampos(resultCandidato.Item4.Candidatos);
                                    if (!resultCampos.Item1)
                                    {
                                        Session["direction"] = nuevoArchivo;
                                        var list = BL.Candidato.AddRange(resultCandidato.Item4.Candidatos);
                                        if(list.Item1)
                                        {
                                            ViewBag.Resultado = "Los Candidatos han sido actualizados";
                                        }
                                        else
                                        {
                                            ViewBag.Error = list.Item2;
                                        }
                                        return View();
                                    }
                                    else
                                    {
                                        ViewBag.Mensaje = resultCampos.Item2;
                                        return View();
                                    }
                                }
                                else
                                {
                                    ViewBag.Mensaje = resultCandidato.Item2;
                                    return View();
                                }
                            }
                            else
                            {
                                ViewBag.Mensaje = "Ya existe este archivo, por favor elimine el archivo";
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = "Solo se permiten archivos de excel";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.Mensaje = "Debe por lo menos ";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se ha subido un archivo";
                    return View();
                }
            }
            else
            {
                string nuevoArchivo = (string)Session["directorio"];
                if (System.IO.File.Exists(nuevoArchivo))
                {
                    string connectionString = System.Configuration.ConfigurationManager.AppSettings["connectionExcel"].ToString() + nuevoArchivo;
                    var resultConsult = BL.CargaMáxima.AddExcel(connectionString);
                    if (resultConsult.Item1)
                    {
                        var resultCampos = BL.CargaMáxima.ValidarCampos(resultConsult.Item4.Candidatos);
                        if (!resultCampos.Item1)
                        {
                            return View(resultCampos.Item4);
                        }
                        else
                        {
                            foreach (ML.Candidato item in resultConsult.Item4.Candidatos)
                            {
                                var resultProducto = BL.Candidato.Add(item);
                                if (!resultProducto.Item1)
                                {
                                    ViewBag.Mensaje = "Los datos no son correspondientes";
                                    return View();
                                }
                                else if (resultProducto.Item3 != null)
                                {
                                    ViewBag.Mensaje = resultProducto.Item2;
                                    return View();
                                }
                            }
                            Session["directorio"] = null;
                            ViewBag.Mensaje = "El mensaje se ha insertardo correctamente";
                        }
                        return View();
                    }
                    else
                    {
                        ViewBag.Mensaje = "Vuelve a corraborar tu archivo de excel";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Mensaje = "No se ha encontrado el archivo correspondiente";
                    return View();
                }
            }
        }
    }
}