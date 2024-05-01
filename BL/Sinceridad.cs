using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sinceridad
    {
        public static (bool, string, Exception, ML.Sinceridad) GetAll()
        {
            ML.Sinceridad encapsular = new ML.Sinceridad { TiposSinceridad = new List<ML.Sinceridad>() };
            using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
            {
                try
                {
                    var result = context.Sinceridads.ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            ML.Sinceridad autoEstima = new ML.Sinceridad
                            {
                                IdSinceridad= item.IdSinceridad,
                                Nombre = item.Nombre
                            };
                            encapsular.TiposSinceridad.Add(autoEstima);
                        }
                        return (true, "", null, encapsular);
                    }
                    else
                    {
                        return (false, "No existen resultados de autoestimas", null, encapsular);
                    }
                }
                catch (Exception ex)
                {
                    return (false, ex.Message, ex, null);
                }
            }
        }
    }
}
