//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APIRestAlergenos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAlerg_Ing
    {
        public int id_Alerg_ing { get; set; }
        public int id_Alergeno { get; set; }
        public int id_Ingrediente { get; set; }
    
        public virtual tbAlergenos tbAlergenos { get; set; }
        public virtual tbIngredientes tbIngredientes { get; set; }
    }
}