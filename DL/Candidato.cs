//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Candidato
    {
        public int IdCandidato { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Genero { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public Nullable<int> IdSinceridad { get; set; }
        public Nullable<int> IdAutoEstima { get; set; }
        public Nullable<int> IdPersonalidad { get; set; }
        public Nullable<int> IdEstres { get; set; }
    
        public virtual AutoEstima AutoEstima { get; set; }
        public virtual Estre Estre { get; set; }
        public virtual Personalidad Personalidad { get; set; }
        public virtual Sinceridad Sinceridad { get; set; }
    }
}
