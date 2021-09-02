using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ClienteController : Controller
    {
        //
        // GET: /Cliente/

        public ActionResult GetAll()
        {
            BL.Result result = BL.Cliente.ClienteGetAll();

            BL.Amortizacion amortizacion = new BL.Amortizacion();
            amortizacion.Amortizaciones = result.Objects;

            return View(amortizacion);
        }



        [HttpGet]
        public ActionResult Form(BL.Amortizacion amortizacion)
        {
            if(amortizacion.NumeroPago != 0)
            {
                return View(amortizacion);
            }
            else
            {
                return View(amortizacion);
            }
            
        }

        [HttpPost]
        public ActionResult FormAdd(BL.Amortizacion amortizacion)
        {
            decimal TasaMensual = amortizacion.Credito.TasaAnual / 12;
            //Pago Mensual
            double TasaMensualAux = double.Parse(amortizacion.Credito.ImporteCredito.ToString()) * double.Parse(TasaMensual.ToString());
            double TasaMensualAux1=1 + double.Parse(TasaMensual.ToString());
            double TasaMensualAux2 = (1 - Math.Pow(TasaMensualAux1, -amortizacion.Credito.Plazo));
            amortizacion.PagoMensual = decimal.Parse(TasaMensualAux.ToString()) / decimal.Parse(TasaMensualAux2.ToString());
            //Interes del Periodo
            amortizacion.InteresPeriodo = amortizacion.Credito.ImporteCredito - TasaMensual;
            //CapitalAmortizado
            amortizacion.CapitalAmortizado = amortizacion.Credito.ImporteCredito - amortizacion.InteresPeriodo;
            //SandoInsoluto
            amortizacion.SaldoInsoluto = amortizacion.Credito.ImporteCredito - amortizacion.CapitalAmortizado;
            
            //RFC
            amortizacion.Credito.Cliente.RFC = amortizacion.Credito.Cliente.ApellidoPaterno.Substring(0, 2).ToUpper() + amortizacion.Credito.Cliente.ApellidoMaterno.Substring(0, 1).ToUpper() + amortizacion.Credito.Cliente.Nombre.Substring(0, 1).ToUpper() +
                         amortizacion.Credito.Cliente.FechaNacimiento.Substring(8, 2) + amortizacion.Credito.Cliente.FechaNacimiento.Substring(3, 2) + amortizacion.Credito.Cliente.FechaNacimiento.Substring(0, 2);

            BL.Result resultAdd = BL.Cliente.ClienteAdd(amortizacion);
            if(resultAdd.Correct != true)
            {
                ViewBag.Message = "No se pudo insetar el cliente ";
            }

            BL.Result resultGetById = BL.Cliente.ClienteGetById(amortizacion);

            BL.Amortizacion Auxamortizacion = new BL.Amortizacion();
            if (resultGetById.Object != null)
            {
                Auxamortizacion = (BL.Amortizacion)resultGetById.Object;
            }
            
            return View("Form",Auxamortizacion);
        }


        public ActionResult Delete(int NumeroCliente, int NumeroCredito)
        {
            BL.Amortizacion amortizacion = new BL.Amortizacion();
            amortizacion.Credito = new BL.Credito();
            amortizacion.Credito.Cliente = new BL.Cliente();

            amortizacion.Credito.Cliente.NumeroCliente = NumeroCliente;
            amortizacion.Credito.NumeroCredito = NumeroCredito;
            BL.Result result = BL.Cliente.ClienteDelete(amortizacion);

            return RedirectToAction("GetAll");
        }
	}
}
