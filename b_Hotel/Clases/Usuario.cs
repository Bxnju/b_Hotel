using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public abstract class Usuario
    {
        public enum e_tipoID { CC , TI , PA , CE }
        public enum e_Nacionalidad { Colombiano, Otro }
        private bool tieneReserva;
        private Hotel hotel;

        private string nombre;
        private e_tipoID tipoID;
        private e_Nacionalidad nacionalidad;
        private long nroDoc;
        private long telefono;
        private string usuarioLogin;
        private string contrasenia;
        private bool checkedIN;
        
        public string Nombre 
        { 
            get => nombre;

            set 
            {
                if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value) && value.Length > 3)
                {
                    nombre = value;
                }
                else
                    throw new ArgumentException("El nombre debe ser mayor a 3 Caracteres minimo.");
            } 
        }
        public e_tipoID TipoID { get => tipoID; set => tipoID = value; }
        public long NroDoc 
        {
            get => nroDoc;
            set
            {
                if (value.ToString().Length > 5 && value != 0)
                {
                    nroDoc = value;
                }
                else throw new ArgumentException("El Nro de Documento debe contener mas de 5 digitos y no deben ser 0");
            } 
        }
        public long Telefono 
        { 
            get => telefono;

            set
            {
                if (value.ToString().Length > 5 && value != 0)
                {
                    nroDoc = value;
                }
                else throw new ArgumentException("El telefono debe contener mas de 5 digitos y no deben ser 0");

            }
        }
        public string UsuarioLogin
        {
            get => usuarioLogin;
            
            set
            {
                if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value) || value.Length < 6)
                {
                    throw new ArgumentException($"El Usuario debe tener mas de 6 caracteres.");
                }

                foreach (Usuario usu in Hotel.Obtener_Instancia_Hotel().l_usuarios)
                {
                    if (usu.UsuarioLogin.Equals(value))
                    {
                        throw new ArgumentException($"El usuario \"{value}\" ya se encuentra en uso. Intente de Nuevo");
                    }
                }

                usuarioLogin = value;
            } 
        }
        public string Contrasenia
        {
            get => contrasenia;
            
            set
            {   
                if (string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException("La contraseña debe tener al menos 5 caracteres.");
                }

                if (!value.Any(char.IsUpper))
                {
                    throw new ArgumentException("La contraseña debe contener al menos una letra mayúscula.");
                }

                if (!value.Any(char.IsLower))
                {
                    throw new ArgumentException("La contraseña debe contener al menos una letra minúscula.");
                }

                if (!value.Any(char.IsDigit))
                {
                    throw new ArgumentException("La contraseña debe contener al menos un número.");
                }

                contrasenia = value;
            }
        }
        public bool CheckedIN { get => checkedIN; set => checkedIN = value; }
        public e_Nacionalidad Nacionalidad { get => nacionalidad; set => nacionalidad = value; }
		public bool TieneReserva { get => tieneReserva; set => tieneReserva = value; }

		public Usuario(string nom, e_tipoID tipoid, long documento, long tel, string usu, string contra, e_Nacionalidad nacion)
        {
            Nombre = nom;
            TipoID = tipoid;
            NroDoc = documento;
            Telefono = tel;
            UsuarioLogin = usu;
            Contrasenia = contra;
            Nacionalidad = nacion;

            tieneReserva = false;
            checkedIN = false;
            hotel = Hotel.Obtener_Instancia_Hotel();
        }

        public void Event_Handler_Reservas()
        {
            Console.WriteLine("Procesando Reserva...");
        }

        public void Crear_Reserva(short habId, string strFechaEntrada, string strFechaSalida)
        {
            Reserva reservaUsu;
            Oficina office = hotel.oficinaHotel;
            Habitacion hab = null;

            try
            {
                foreach (Habitacion item in hotel.l_habitaciones)
                {
                    if (item.Id == habId)
                    hab = item;
                }

                if (hab == null)
                    throw new Exception("Id de Habitacion Invalido");

                reservaUsu = new Reserva(hab, this, strFechaEntrada, strFechaSalida);

                if (!tieneReserva)
                {
                    office.eventoReserva += Event_Handler_Reservas;
                    office.Informar_Nueva_Reserva(reservaUsu);
                    office.eventoReserva -= Event_Handler_Reservas;
                }
                else
                    throw new Exception("Usted ya tiene Reserva");
            }
            catch (Exception error)
            {
                throw new Exception("Error en crear reserva " + error);
            }
        }
        public Dictionary<string, string> Check_Out()
        {
            Dictionary<string, string> data;
            Oficina office;
            try
            {
                if (checkedIN)
                {
                    office = hotel.oficinaHotel;

                    office.eventoReserva += Event_Handler_Reservas;
                    data = office.Informar_Check_Out(this);
                    office.eventoReserva -= Event_Handler_Reservas;

                    return data;
                }
                else
                {
                    throw new Exception("No puedes hacer Check-Out sin hacer Check-In");
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error en CheckOut desde el USUARIO:\n{error}");
            }
        }
        public void Check_In()
        {
            Oficina office;
            try
            {
                if (!checkedIN)
                {
                    office = hotel.oficinaHotel;

                    checkedIN = true;

                    office.eventoReserva += Event_Handler_Reservas;
                    office.Informar_Check_In(this);
                    office.eventoReserva -= Event_Handler_Reservas;
                }
                else
                {
                    throw new Exception("Ya hiciste Check-In");
                }
            }
            catch (Exception error)
            {
                throw new Exception($"Error en CheckOut desde el USUARIO:\n{error}");
            }
        }
        public void Eliminar_Reserva()
        {
            Oficina office = hotel.oficinaHotel;
            try
            {
                if (tieneReserva)
                {
                    office.eventoReserva += Event_Handler_Reservas;
                    office.Eliminar_Reserva(this);
                    office.eventoReserva -= Event_Handler_Reservas;
                }
                else
                    Console.WriteLine("Usted ya tiene Reserva");
            }
            catch (Exception error)
            {
                throw new Exception("Error en crear reserva " + error);
            }
        }
        public void Comprar_Comida(int nro_Des, int nro_Alm, int nro_Cena, bool alCuarto)
        {
            try
            {
                List<Comida> lista = new List<Comida>();

                for (int i = 0; i < nro_Alm; i++)
                {
                    lista.Add(new Comida(Comida.e_tipos_comida.Almuerzo));
                }

                for (int i = 0; i < nro_Des; i++)
                {
                    lista.Add(new Comida(Comida.e_tipos_comida.Desayuno));
                }

                for (int i = 0; i < nro_Cena; i++)
                {
                    lista.Add(new Comida(Comida.e_tipos_comida.Cena));
                }

                hotel.restauranteHotel.Vender_Comida(lista, alCuarto, this);
            }
            catch(Exception error)
            {
                throw new Exception("Error en Comprar Comida:\n" + error);
            }
        }
        public void Solicitar_Servicio(int nro_Lavadas, int nro_Planchadas, bool alCuarto)
        {
            try
            {
                List<Servicio> lista = new List<Servicio>();

                for (int i = 0; i < nro_Lavadas; i++)
                {
                    lista.Add(new Servicio(Servicio.e_tipos_servicio.Lavada));
                }

                for (int i = 0; i < nro_Planchadas; i++)
                {
                    lista.Add(new Servicio(Servicio.e_tipos_servicio.Planchada));
                }

                hotel.lavanderiaHotel.Lavar_Planchar(lista, alCuarto, this);
            }
            catch
            {
                throw new Exception("Error en Comprar Comida");
            }
        }
        public void Comprar_Productos(int nro_Gaseosa, int nro_Agua, int nro_Vino, int nro_Licor, int nro_Bata, int nro_KitAseo, bool alCuarto)
        {
            try
            {
                List<Producto> lista = new List<Producto>();

                for (int i = 0; i < nro_Agua; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.Agua));
                }

                for (int i = 0; i < nro_Bata; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.Bata));
                }

                for (int i = 0; i < nro_Gaseosa; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.Gaseosa));
                }

                for (int i = 0; i < nro_KitAseo; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.KitAseo));
                }

                for (int i = 0; i < nro_Licor; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.Licor));
                }

                for (int i = 0; i < nro_Vino; i++)
                {
                    lista.Add(new Producto(Producto.e_tipos_producto.Vino));
                }

                hotel.tiendaHotel.Vender_Productos(lista, alCuarto, this);
            }
            catch(Exception error)
            {
                throw new Exception("Error en Comprar Comida:\n"+error);
            }
        }
    }
}
