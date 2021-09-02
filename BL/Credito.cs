using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Credito
    {
        public int NumeroCredito { get; set; }

        public decimal ImporteCredito { get; set; }

        public int Plazo { get; set; }

        public decimal TasaAnual { get; set; }

        public BL.Cliente Cliente { get; set; }
    }
}
