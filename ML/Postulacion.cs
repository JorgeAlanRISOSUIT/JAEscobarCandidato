using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Postulacion
    {
        public int IdPostulacion { get; set; }
        public DateTime FechaPostulacion { get; set; }
        public Candidato Candidato { get; set; }
        public Vacante Vacante { get; set; }
        public StatusPostulacion StatusPostulacion { get; set; }
        public List<Postulacion> Postulaciones { get; set; }
    }
}
