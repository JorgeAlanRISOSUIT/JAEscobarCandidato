using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Vacante
    {
        public int IdVacante { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public Empresa Empresa { get; set; }
        public List<ML.Vacante> Vacantes { get; set; }
    }
}
