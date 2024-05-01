using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estres
    {
        public static (bool, string, Exception, ML.Estres) GetAll()
        {
            ML.Estres encapsular = new ML.Estres { TiposEstres = new List<ML.Estres>() };
            using (DL.JAEscobarCandidatoEntities context = new DL.JAEscobarCandidatoEntities())
            {
                try
                {
                    var result = context.Estres.ToList();
                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            ML.Estres autoEstima = new ML.Estres
                            {
                                IdEstres = item.IdEstres,
                                Nombre = item.Nombre
                            };
                            encapsular.TiposEstres.Add(autoEstima);
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
