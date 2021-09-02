using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Cliente
    {
        public int NumeroCliente { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Nombre { get; set; }

        public string FechaNacimiento { get; set; }

        public string RFC { get; set; }

        public List<object> Clientes { get; set; }


        public static Result ClienteGetAll()
        {
            Result result = new Result();
            try
            {
                using (DL.MMayenBancoInmobiliarioEntities context = new DL.MMayenBancoInmobiliarioEntities())
                {
                    var clientes = context.ClienteGetAll().ToList();
                    result.Objects = new List<object>();

                    if (clientes != null)
                    {
                        foreach (var obj in clientes)
                        {
                            Amortizacion amortizacion = new Amortizacion();
                            amortizacion.Credito = new Credito();
                            amortizacion.Credito.Cliente = new Cliente();

                            amortizacion.Credito.Cliente.Nombre = obj.Nombre;
                            amortizacion.Credito.Cliente.ApellidoPaterno = obj.ApellidoPaterno;
                            amortizacion.Credito.Cliente.ApellidoMaterno = obj.ApellidoMaterno;
                            amortizacion.Credito.Cliente.FechaNacimiento = obj.FechaNacimiento.ToString();
                            amortizacion.Credito.Cliente.NumeroCliente = obj.NumeroCliente;
                            amortizacion.Credito.Cliente.RFC = obj.RFC;

                            amortizacion.Credito.NumeroCredito = obj.NumeroCredito.Value;

                            amortizacion.CapitalAmortizado = obj.CapitalAmortizado.Value;
                            amortizacion.InteresPeriodo = obj.InteresPeriodo.Value;
                            amortizacion.NumeroPago = obj.NumeroPago;
                            amortizacion.PagoMensual = obj.PagoMensual.Value;
                            amortizacion.SaldoInsoluto = obj.SaldoInsoluto.Value;
                            result.Objects.Add(amortizacion);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar los clientes";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result ClienteAdd(Amortizacion amortizacion)
        {
            Result result = new Result();
            try
            {
                using(DL.MMayenBancoInmobiliarioEntities context = new DL.MMayenBancoInmobiliarioEntities())
                {
                    var query = context.ClienteAdd(amortizacion.Credito.Cliente.Nombre, amortizacion.Credito.Cliente.ApellidoPaterno, amortizacion.Credito.Cliente.ApellidoMaterno, amortizacion.Credito.Cliente.FechaNacimiento, amortizacion.Credito.NumeroCredito,
                                                    amortizacion.Credito.ImporteCredito, amortizacion.Credito.Plazo, amortizacion.Credito.TasaAnual, amortizacion.PagoMensual, amortizacion.CapitalAmortizado,
                                                    amortizacion.InteresPeriodo, amortizacion.SaldoInsoluto, amortizacion.Credito.Cliente.RFC);
                    
                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo Insertar el cliente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static Result ClienteGetById(Amortizacion amortizacion)
        {
            Result result = new Result();
            try
            {
                using (DL.MMayenBancoInmobiliarioEntities context = new DL.MMayenBancoInmobiliarioEntities())
                {
                    var Cliente = context.ClienteGetById(amortizacion.Credito.NumeroCredito).FirstOrDefault();

                    if (Cliente != null)
                    {
                        Amortizacion AUXamortizacion = new Amortizacion();

                        AUXamortizacion.CapitalAmortizado = Cliente.CapitalAmortizado.Value;
                        AUXamortizacion.InteresPeriodo = Cliente.InteresPeriodo.Value;
                        AUXamortizacion.NumeroPago = Cliente.NumeroPago;
                        AUXamortizacion.PagoMensual = Cliente.PagoMensual.Value;
                        AUXamortizacion.SaldoInsoluto = Cliente.SaldoInsoluto.Value;

                        AUXamortizacion.Credito = new Credito();

                        AUXamortizacion.Credito.ImporteCredito = Cliente.ImporteCredito.Value;
                        AUXamortizacion.Credito.Plazo = Cliente.Plazo.Value;
                        AUXamortizacion.Credito.TasaAnual = Cliente.TasaAnual.Value;
                        AUXamortizacion.Credito.NumeroCredito = Cliente.Numero_Credito.Value;

                        AUXamortizacion.Credito.Cliente = new Cliente();

                        AUXamortizacion.CapitalAmortizado = Cliente.CapitalAmortizado.Value;
                        AUXamortizacion.InteresPeriodo = Cliente.InteresPeriodo.Value;
                        AUXamortizacion.NumeroPago = Cliente.NumeroCliente;
                        AUXamortizacion.PagoMensual = Cliente.PagoMensual.Value;
                        AUXamortizacion.SaldoInsoluto = Cliente.SaldoInsoluto.Value;
                        AUXamortizacion.Credito.Cliente.RFC = Cliente.RFC;

                        

                        result.Object = AUXamortizacion;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo recuperar el cliente";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;

        }

        public static Result ClienteDelete(Amortizacion amortizacion)
        {
            Result result = new Result();

            try
            {
                using (DL.MMayenBancoInmobiliarioEntities context = new DL.MMayenBancoInmobiliarioEntities())
                {
                    var query = context.ClienteDelete(amortizacion.Credito.Cliente.NumeroCliente,amortizacion.Credito.NumeroCredito);

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al realizar la eliminacion del cliente";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
