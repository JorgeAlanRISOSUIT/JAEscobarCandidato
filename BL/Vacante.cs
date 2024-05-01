using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Vacante
    {
        public static (bool, string, Exception, ML.Vacante) EmpresaGetById(int idEmpresa)
        {
            try
            {
                ML.Vacante collections = new ML.Vacante  { Vacantes = new List<ML.Vacante>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.EmpresaGetByIdVacante(idEmpresa).ToList();
                    if(result.Count > 0)
                    {
                        foreach(var busquedas in result)
                        {
                            ML.Vacante vacante = new ML.Vacante()
                            {
                                IdVacante = busquedas.IdVacante,
                                Nombre = busquedas.NombreVacante,
                                FechaPublicacion = busquedas.FechaPublicación,
                                Empresa = new ML.Empresa
                                {
                                    IdEmpresa = busquedas.IdEmpresa,
                                    Nombre = busquedas.NombreEmpresa,
                                }
                            };
                            collections.Vacantes.Add(vacante);
                        }
                        return (true, "", null, collections);
                    }
                    else
                    {
                        return (false, "No existen vacantes para esta empresa", null, null);
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, ex.Message, ex, null);
            }
        }
    }
}
