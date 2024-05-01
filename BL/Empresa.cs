using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static (bool, string, Exception, ML.Empresa) GetAll()
        {
            try
            {
                ML.Empresa collections = new ML.Empresa{ Empresas = new List<ML.Empresa>() };
                using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
                {
                    var result = context.GetAllEmpresa().ToList();
                    if (result.Count > 0)
                    {
                        foreach (var empresa in result)
                        {
                            ML.Empresa objEmpresa = new ML.Empresa
                            {
                                IdEmpresa = empresa.IdEmpresa,
                                Nombre = empresa.Nombre,
                            };
                            collections.Empresas.Add(objEmpresa);
                        }
                        return (true, "", null, collections);
                    }
                    else
                    {
                        return (false, "No se encuentran empresas", null, null);
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
