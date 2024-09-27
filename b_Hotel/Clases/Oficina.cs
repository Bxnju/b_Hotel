using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Oficina
    {
        private List<Reserva> l_reservas = new();
        private readonly Recepcion recepcionHotel;

        public Oficina()
        {
            recepcionHotel = new Recepcion();
        }

        public List<Reserva> L_reservas { get => l_reservas; }

        public Recepcion RecepcionHotel => recepcionHotel;

        internal Recepcion RecepcionHotel1 => recepcionHotel;

        internal delegate void delegadoReserva();
        internal event delegadoReserva eventoReserva;

        internal delegate void delegadoRestaurante();
        internal event delegadoRestaurante eventoRestaurante;

        internal delegate void delegadoLavanderia();
        internal event delegadoLavanderia eventoLavanderia;

        internal delegate void delegadoTienda();
        internal event delegadoTienda eventoTienda;

        internal void Informar_Nueva_Reserva(Reserva res)
        {
            try
            {
                if (eventoReserva != null)
                {
                    eventoReserva();

                    res.Habreserva.Reservada = true;
                    res.Habreserva.Ocupada = false;

                    l_reservas.Add(res);
                }
                else throw new Exception("NO SUSCRITO");
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal void Eliminar_Reserva(Usuario usu)
        {
            try
            {
                if (eventoReserva != null)
                {
                    eventoReserva();
                    foreach (Reserva res in L_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            res.Habreserva.Ocupada = false;
                            res.Habreserva.Reservada = false;

                            if (res.Habreserva is Habitacion_Ejecutiva)
                            {
                                Habitacion_Ejecutiva eje = res.Habreserva as Habitacion_Ejecutiva;
                                eje.Llenar_MiniBar();
                            }
                            else if (res.Habreserva is Habitacion_Suite)
                            {
                                Habitacion_Suite sui = res.Habreserva as Habitacion_Suite;
                                sui.Llenar_MiniBar();
                            }

                            L_reservas.Remove(res);
                            Console.WriteLine($"Cancelando Reserva de {usu}");

                            break;
                        }
                    }
                }
                else throw new Exception("NO SUSCRITO");
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal Dictionary<string, string> Informar_Check_Out(Usuario usu)
        {
            try
            {
                if (eventoReserva != null)
                {
                    eventoReserva();

                    foreach (Reserva res in l_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            res.Habreserva.Reservada = false;
                            res.Habreserva.Ocupada = false;

                            if (res.Habreserva is Habitacion_Ejecutiva)
                                (res.Habreserva as Habitacion_Ejecutiva).Llenar_MiniBar();

                            else if (res.Habreserva is Habitacion_Suite)
                                (res.Habreserva as Habitacion_Suite).Llenar_MiniBar();

                            return RecepcionHotel.Check_Out(res);
                        }
                    }
                    return null;
                }
                else
                    throw new Exception("NO SUSCRITO");
            }
            catch (Exception error)
            {
                throw new Exception("Error informando Checck Out");
            }
        }
        internal void Informar_Check_In(Usuario usu)
        {
            try
            {
                if (eventoReserva != null)
                {
                    eventoReserva();

                    foreach (Reserva res in l_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            res.Habreserva.Reservada = false;
                            res.Habreserva.Ocupada = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception("Error informando Checck IN");
            }
        }
        internal void Agregar_Comida_Reserva(Usuario usu, List<Comida> listaComidas, bool alCuarto)
        {
            try
            {
                if (eventoRestaurante != null)
                {
                    eventoRestaurante();
                    foreach (Reserva res in L_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            foreach (Comida comida in listaComidas)
                            {
                                res.ResComidas.Add(comida);
                            }

                            if (alCuarto)
                                res.Nro_ServiciosCuarto++;
                            break;
                        }
                    }
                }
                else throw new Exception("NO SUSCRITO");
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal void Agregar_Servicios_Reserva(Usuario usu, List<Servicio> listaServicios, bool alCuarto)
        {
            try
            {
                if (eventoLavanderia != null)
                {
                    eventoLavanderia();
                    foreach (Reserva res in L_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            foreach (Servicio serv in listaServicios)
                            {
                                res.ResServicios.Add(serv);
                            }

                            if (alCuarto)
                                res.Nro_ServiciosCuarto++;
                            
                            break;
                        }

                    }
                }
                else throw new Exception("NO SUSCRITO");
            }
            catch (Exception)
            {
                throw;
            }
        }
        internal void Agregar_Productos_Reserva(Usuario usu, List<Producto> listaProductos, bool alCuarto)
        {         
            List<Producto> l_aux = listaProductos.ToList();

            try
            {
                if (eventoTienda != null)
                {
                    eventoTienda();
                    foreach (Reserva res in L_reservas)
                    {
                        if (res.UsuarioReserva == usu)
                        {
                            if (res.Habreserva is Habitacion_Ejecutiva)
                            {
                                Habitacion_Ejecutiva eje = (Habitacion_Ejecutiva)res.Habreserva;

                                foreach (Producto productoComprar in l_aux)
                                {
                                    foreach (Producto productoMini in eje.L_minibar)
                                    {
                                        if (productoComprar.Type == productoMini.Type)
                                        {
                                            eje.L_minibar.Remove(productoMini);
                                            eje.Tiene_Producto();
                                            break;
                                        }
                                    }
                                    res.ResProductos.Add(productoComprar);
                                }
                            }

                            else if (res.Habreserva is Habitacion_Suite)
                            {

                                Habitacion_Suite sui = (Habitacion_Suite)res.Habreserva;

                                foreach (Producto productoComprar in l_aux)
                                {
                                    foreach (Producto productoMini in sui.L_minibar)
                                    {
                                        if (productoComprar.Type.Equals(productoMini.Type))
                                        {
                                            sui.L_minibar.Remove(productoMini);
                                            sui.Tiene_Producto();
                                            break;
                                        }
                                    }
                                    res.ResProductos.Add(productoComprar);
                                }
                            }

                            else
                            {
                                foreach (Producto producto in l_aux)
                                {
                                    res.ResProductos.Add(producto);
                                }
                            }

                            if (alCuarto)
                                res.Nro_ServiciosCuarto++;

                            break;
                        }
                    }
                }
                else throw new Exception("NO SUSCRITO");
            }
            catch (Exception e)
            {
                throw new Exception("Error:\n" + e);
            }
        }

        public List<Habitacion> Buscar_Habitaciones_Disponibles(string strFechaEntrada, string strFechaSalida)
        {
            List<Habitacion> habitacionesDisponibles = new List<Habitacion>();
            bool disponible; 

            try
            {
                DateTime FechaEntrada = DateTime.ParseExact(strFechaEntrada, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime FechaSalida = DateTime.ParseExact(strFechaSalida, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                foreach (Habitacion Hab in Hotel.Obtener_Instancia_Hotel().l_habitaciones)
                {
                    disponible = true;
                    foreach (Reserva res in L_reservas)
                    {
                        if (res.Habreserva == Hab)
                        {
                            if (!(res.FechaEntrada >= FechaSalida || FechaEntrada >= res.FechaSalida))
                            {
                                disponible = false;
                                break;
                            }
                        }
                    }
                    if (disponible)
                    {
                        habitacionesDisponibles.Add(Hab);
                    }
                }
                return habitacionesDisponibles;
            }
            catch (Exception error)
            {
                throw new Exception("Error en Buscar_Habitaciones_Disponibles:\n" + error);
            }
        }
        public Dictionary<string, string> Reporte_Habitaciones()
        {
            Dictionary<string, string> reporte;
            byte contReservadas = 0, contOcupadas = 0, contDisponibles = 0;
            try
            {
                foreach (Habitacion hab in Hotel.Obtener_Instancia_Hotel().l_habitaciones)
                {
                    if (hab.Ocupada) contOcupadas++;
                    else if (hab.Reservada) contReservadas++;
                    else contDisponibles++;
                }

                reporte = new Dictionary<string, string>()
                {
                    {"reservadas",contReservadas.ToString()},
                    {"ocupadas",contOcupadas.ToString()},
                    {"disponibles",contDisponibles.ToString()}
                
                };

                return reporte;
            }
            catch (Exception error)
            {
                throw new Exception("Error en Buscar_Habitaciones_Disponibles:\n" + error);
            }
        }
        public Usuario Hacer_Login_Reservas(string usuario, string contra)
        {
            try
            {
                foreach (Reserva res in l_reservas)
                {
                    if (res.UsuarioReserva.UsuarioLogin.Equals(usuario) && 
                        res.UsuarioReserva.Contrasenia.Equals(contra))
                    {
                        return res.UsuarioReserva; 
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error en hacer login reservas");
            }
        }
    }
}
