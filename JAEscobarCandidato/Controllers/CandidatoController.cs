using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JAEscobarCandidato.Controllers
{
    public class CandidatoController : Controller
    {
        // GET: Candidato
        public ActionResult Candidatos()
        {
            var result = BL.Candidato.GetAll();
            if (result.Item1)
            {
                return View(result.Item4);
            }
            else
            {
                ViewBag.Mensaje = "No existen candidatos";
                return View(result);
            }
        }

        public ActionResult ConsultarCandidatos(int? idCandidato)
        {
            if (idCandidato.HasValue)
            {
                var result = BL.Candidato.GetById(idCandidato.Value);
                if (result.Item1)
                {
                    var resultAutoEstima = BL.AutoEstima.GetAll();
                    var resultPersonalidad = BL.Personalidad.GetAll();
                    var resultEstres = BL.Estres.GetAll();
                    var resultSinceridad = BL.Sinceridad.GetAll();
                    result.Item4.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                    result.Item4.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                    result.Item4.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                    result.Item4.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();
                    return View(result.Item4);
                }
                else
                {
                    return RedirectToAction("Candidatos", "Candidato");
                }
            }
            else
            {
                ML.Candidato nuevo = new ML.Candidato();
                nuevo.Personalidad = new ML.Personalidad();
                nuevo.Estres = new ML.Estres();
                nuevo.AutoEstima = new ML.AutoEstima();
                nuevo.Sinceridad = new ML.Sinceridad();
                var resultAutoEstima = BL.AutoEstima.GetAll();
                var resultPersonalidad = BL.Personalidad.GetAll();
                var resultEstres = BL.Estres.GetAll();
                var resultSinceridad = BL.Sinceridad.GetAll();
                nuevo.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                nuevo.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                nuevo.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                nuevo.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();

                return View(nuevo);
            }
        }

        public JsonResult ObtenerTodos()
        {
            var result = BL.Candidato.GetAll();
            return Json(result);
        }

        [HttpPost]
        public ActionResult ConsultarCandidatos(ML.Candidato candidato)
        {
            if (candidato.IdCandidato == 0)
            {
                var result = BL.Candidato.Add(candidato);
                if (result.Item1)
                {
                    var resultAutoEstima = BL.AutoEstima.GetAll();
                    var resultPersonalidad = BL.Personalidad.GetAll();
                    var resultEstres = BL.Estres.GetAll();
                    var resultSinceridad = BL.Sinceridad.GetAll();
                    candidato.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                    candidato.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                    candidato.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                    candidato.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();
                    ViewBag.Mensaje = "Se agrego correctamente el usuario";
                    return View(candidato);
                }
                else
                {
                    var resultAutoEstima = BL.AutoEstima.GetAll();
                    var resultPersonalidad = BL.Personalidad.GetAll();
                    var resultEstres = BL.Estres.GetAll();
                    var resultSinceridad = BL.Sinceridad.GetAll();
                    candidato.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                    candidato.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                    candidato.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                    candidato.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();
                    ViewBag.Mensaje = "No se agrego el candidato";
                    return View(candidato);
                }
            }
            else
            {
                var result = BL.Candidato.UpdateCandidato(candidato);
                if (result.Item1)
                {
                    var resultAutoEstima = BL.AutoEstima.GetAll();
                    var resultPersonalidad = BL.Personalidad.GetAll();
                    var resultEstres = BL.Estres.GetAll();
                    var resultSinceridad = BL.Sinceridad.GetAll();
                    candidato.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                    candidato.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                    candidato.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                    candidato.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();
                    return View(candidato);
                }
                else
                {
                    var resultAutoEstima = BL.AutoEstima.GetAll();
                    var resultPersonalidad = BL.Personalidad.GetAll();
                    var resultEstres = BL.Estres.GetAll();
                    var resultSinceridad = BL.Sinceridad.GetAll();
                    candidato.Personalidad.TiposPersonalidad = resultPersonalidad.Item1 ? resultPersonalidad.Item4.TiposPersonalidad : new List<ML.Personalidad>();
                    candidato.Sinceridad.TiposSinceridad = resultSinceridad.Item1 ? resultSinceridad.Item4.TiposSinceridad : new List<ML.Sinceridad>();
                    candidato.Estres.TiposEstres = resultEstres.Item1 ? resultEstres.Item4.TiposEstres : new List<ML.Estres>();
                    candidato.AutoEstima.TiposAutoEstima = resultAutoEstima.Item1 ? resultAutoEstima.Item4.TiposAutoEstima : new List<ML.AutoEstima>();
                    ViewBag.Mensaje = "No se actualizo el candidato";
                    return View(candidato);
                }
            }
        }

        [HttpPost]
        public JsonResult EliminarCandidato(int idCandidato) 
        {
            var result = BL.Candidato.DeleteCandidato(idCandidato);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}