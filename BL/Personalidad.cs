using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Personalidad
    {
        public static (bool, string, Exception, ML.Personalidad) GetAll()
        {
            ML.Personalidad encapsular = new ML.Personalidad { TiposPersonalidad = new List<ML.Personalidad>() };
            using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
            {
                try
                {
                    var result = context.Personalidads.ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            ML.Personalidad autoEstima = new ML.Personalidad
                            {
                                IdPersonalidad= item.IdPersonalidad,
                                Nombre = item.Nombre
                            };
                            encapsular.TiposPersonalidad.Add(autoEstima);
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
