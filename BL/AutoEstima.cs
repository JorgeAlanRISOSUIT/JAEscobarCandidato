using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AutoEstima
    {
        public static (bool, string, Exception, ML.AutoEstima) GetAll()
        {
            ML.AutoEstima encapsular = new ML.AutoEstima { TiposAutoEstima = new List<ML.AutoEstima>() };
            using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
            {
                try
                {
                    var result = context.AutoEstimas.ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            ML.AutoEstima autoEstima = new ML.AutoEstima
                            {
                                IdAutoEstima = item.IdAutoEstima,
                                Nombre = item.Nombre
                            };
                            encapsular.TiposAutoEstima.Add(autoEstima);
                        }
                        return (true, "",  null, encapsular);
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
