using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Amortizacion
    {
        public int NumeroPago { get; set; }

        public BL.Credito Credito { get; set; }

        public decimal PagoMensual { get; set; }

        public decimal CapitalAmortizado { get; set; }

        public decimal InteresPeriodo { get; set; }

        public decimal SaldoInsoluto { get; set; }

        public List<object> Amortizaciones { get; set; }


    }
}
