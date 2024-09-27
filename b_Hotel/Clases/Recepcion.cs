using b_Hotel.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Recepcion : I_Recepcion
    {
        private readonly float iva;
        private readonly float seguroHotelero;
        private readonly short servicioCuarto;

        public Recepcion() 
        {
            iva = 0.19f;
            seguroHotelero = 0.025f;
            servicioCuarto = 5000;
        }

        public Dictionary<string, string> Check_Out(Reserva res)
        {       
            byte nroDes = 0, nroAlm = 0, nroCena = 0, nroGaseosa = 0, nroVino = 0, nroLicor = 0, 
                nroBata = 0, nroKitAseo = 0, nroAgua = 0, nroLavadas = 0, nroPlanchadas = 0, nroServicioCuarto = 0;

            float valorTotal = 0, valorRestaurante = 0, valorTienda = 0, valorLavanderia = 0,
                valorEstadia = 0, descuentoAplicado = 0, valorDescuento = 0, valorSeguroHotelero = 0, valorIVA = 0, valorServicioHabitacion = 0;

            Dictionary<string, string> data;

            try
            {
                nroServicioCuarto = (byte)res.Nro_ServiciosCuarto;
                valorServicioHabitacion = nroServicioCuarto * servicioCuarto;

                if (res.UsuarioReserva is Cliente)
                    descuentoAplicado = (res.UsuarioReserva as Cliente).Descuento;

                foreach (Producto item in res.ResProductos)
                {
                    switch (item.Type)
                    {
                        case Producto.e_tipos_producto.Gaseosa:
                            nroGaseosa++; break;

                        case Producto.e_tipos_producto.Agua:
                            nroAgua++; break;
                            
                        case Producto.e_tipos_producto.Vino:
                            nroVino++; break;
                            
                        case Producto.e_tipos_producto.Licor:
                            nroLicor++; break;
                            
                        case Producto.e_tipos_producto.Bata:
                            nroBata++; break;
                            
                        case Producto.e_tipos_producto.KitAseo:
                            nroKitAseo++; break;
                        default:
                            break;
                    }

                    valorTienda += item.Precio;
                }
                foreach (Comida comida in res.ResComidas)
                {
                    switch (comida.Type)
                    {
                        case Comida.e_tipos_comida.Desayuno:
                            nroDes++; break;

                        case Comida.e_tipos_comida.Almuerzo:
                            nroAlm++; break;

                        case Comida.e_tipos_comida.Cena:
                            nroCena++; break;

                        default:
                            break;
                    }

                    valorRestaurante += comida.Precio;
                }
                foreach (Servicio service in res.ResServicios)
                {
                    switch (service.Type)
                    {
                        case Servicio.e_tipos_servicio.Lavada:
                            nroLavadas++; break;

                        case Servicio.e_tipos_servicio.Planchada:
                            nroPlanchadas++; break;

                        default:
                            break;
                    }

                    valorLavanderia += service.Precio;
                }

                valorSeguroHotelero = (res.Habreserva.PrecioNoche * res.NroNoches) * seguroHotelero;

                valorEstadia = (res.Habreserva.PrecioNoche * res.NroNoches) + valorSeguroHotelero;

                valorTotal = valorEstadia + valorLavanderia + valorRestaurante + valorTienda + valorServicioHabitacion;

                if (res.UsuarioReserva.Nacionalidad == Usuario.e_Nacionalidad.Colombiano)
                {
                    valorIVA = valorTotal * iva;
                    valorTotal += valorIVA;
                }

                valorDescuento = valorTotal * descuentoAplicado;
                valorTotal -= valorDescuento;

                data = new Dictionary<string, string>()
                {
                    {"nroDesayunos", nroDes.ToString()},
                    {"nroAlmuerzos", nroAlm.ToString()},
                    {"nroCenas", nroCena.ToString()},
                    {"nroPlanchadas", nroPlanchadas.ToString()},
                    {"nroLavadas", nroLavadas.ToString()},
                    {"nroGaseosas", nroGaseosa.ToString()},
                    {"nroKitAseo", nroKitAseo.ToString()},
                    {"nroBatas", nroBata.ToString()},
                    {"nroLicores", nroLicor.ToString()},
                    {"nroVinos", nroVino.ToString()},
                    {"nroAguas", nroAgua.ToString()},
                    {"nroServicioCuarto", nroServicioCuarto.ToString()},
                    {"valorRestaurante", valorRestaurante.ToString()},
                    {"valorLavanderia", valorLavanderia.ToString()},
                    {"valorTienda", valorTienda.ToString()},
                    {"valorIVA", valorIVA.ToString()},
                    {"valorEstadia", valorEstadia.ToString()},
                    {"valorSeguroHotelero", valorSeguroHotelero.ToString()},
                    {"valorDescuento", valorDescuento.ToString()},
                    {"descuentoAplicado", descuentoAplicado.ToString()},
                    {"valorServiciosCuarto", valorServicioHabitacion.ToString()},
                    {"tipoCuarto", res.Habreserva.GetType().ToString()},
                    {"TOTAL", valorTotal.ToString()}
                };

                return data;
            }
            catch (Exception error)
            {
                throw new Exception ($"Error en Check Out:\n {error}");
            }
        }
    }
}
