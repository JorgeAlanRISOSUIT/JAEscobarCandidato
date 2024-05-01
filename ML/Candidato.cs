using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Candidato
    {
        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno{ get; set; }
        public string ApellidoMaterno{ get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono{ get; set; }
        public Sinceridad Sinceridad { get; set; }
        public Estres Estres { get; set; }
        public Personalidad Personalidad { get; set; }
        public AutoEstima AutoEstima { get; set; }
        public List<Candidato> Candidatos { get; set; }
    }
}
