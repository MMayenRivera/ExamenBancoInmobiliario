//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credito
    {
        public Credito()
        {
            this.Amortizacions = new HashSet<Amortizacion>();
        }
    
        public int NumeroCredito { get; set; }
        public Nullable<int> ImporteCredito { get; set; }
        public Nullable<int> Plazo { get; set; }
        public Nullable<int> TasaAnual { get; set; }
        public Nullable<int> NumeroCliente { get; set; }
    
        public virtual ICollection<Amortizacion> Amortizacions { get; set; }
        public virtual Cliente Cliente { get; set; }
    }
}
