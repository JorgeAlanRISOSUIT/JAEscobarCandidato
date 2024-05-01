using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Personalidad
    {
        public int IdPersonalidad { get; set; }
        public string Nombre { get; set; }
        public List<Personalidad> TiposPersonalidad { get; set; }
    }
}
