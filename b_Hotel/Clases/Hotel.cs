using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Hotel
    {
        private static string rutaUsuariosArchivo;
        
        private readonly byte nroHabSencillas, nroHabSuite, nroHabejecutivas;

        private static Hotel instanciaHotel;
        
        public Oficina oficinaHotel { get; private set; }
        public Lavanderia lavanderiaHotel{ get; private set; }
        public Tienda tiendaHotel{ get; private set; }
        public Restaurante restauranteHotel { get; private set; }
        public List<Habitacion> l_habitaciones { get; private set; }
        public List<Usuario> l_usuarios { get; set; }

        private Hotel()
        {
            nroHabejecutivas = 10;
            nroHabSencillas = 30;
            nroHabSuite = 10;
            restauranteHotel = new Restaurante();
            l_usuarios = new List<Usuario>();
            lavanderiaHotel = new Lavanderia();
            tiendaHotel = new Tienda();
            oficinaHotel = new Oficina();
            l_habitaciones = new List<Habitacion>();

            rutaUsuariosArchivo = "C:\\Users\\User\\OneDrive\\Escritorio\\Compartida Benju\\Hotel POO\\Archivos\\Usuarios.txt";

            Llenar_Hotel();
        }

        public static Hotel Obtener_Instancia_Hotel()
        {
            if (instanciaHotel == null)
            {
                instanciaHotel = new Hotel();
            }

            return instanciaHotel;
        }

        public void Agregar_Usuarios_Archivos()
        {
            try
            {

            }
            catch (Exception error)
            {
                throw new Exception("Error en Agregar Usuarios Archivos:\n" + error);
            }
        }

        private void Llenar_Hotel()
        {
            try
            {
                for (int i = 0; i < nroHabSencillas; i++)
                    l_habitaciones.Add(new Habitacion_Sencilla());
                for (int i = 0; i < nroHabejecutivas; i++)
                    l_habitaciones.Add(new Habitacion_Ejecutiva());
                for (int i = 0; i < nroHabSuite; i++)
                    l_habitaciones.Add(new Habitacion_Suite());
            }
            catch (Exception error)
            {
                throw new Exception("Error Llenando Hotel:\n" + error);
            } 
        }

        public List<Usuario> Importar()
        {
            Random rnd = new Random();
            
            try
            {
                foreach (string linea in File.ReadLines("C:\\Users\\User\\OneDrive\\Escritorio\\Compartida Benju\\Hotel POO\\Archivos\\Usuarios.txt"))
                {
                    string[] campos = linea.Split(';');
                    if (campos.Length < 7)
                    {
                        throw new ArgumentException("La línea no tiene suficientes campos: " + linea);
                    }
                    var nombre = campos[0];
                    var tipoID = (Usuario.e_tipoID)Enum.Parse(typeof(Usuario.e_tipoID), campos[1]);
                    var documento = long.Parse(campos[2]);
                    var telefono = long.Parse(campos[3]);
                    var usuario = campos[4];
                    var contrasenia = campos[5];
                    var nacionalidad = (Usuario.e_Nacionalidad)Enum.Parse(typeof(Usuario.e_Nacionalidad), campos[6]);
                    
                    if (campos.Length == 7)
                    {
                        l_usuarios.Add(new Huesped(nombre, tipoID, documento, telefono, usuario, contrasenia, nacionalidad));
                    }
                    else if (campos.Length == 8)
                    {
                        var codigo = campos[7];
                        float descuento = (float)Math.Round((float)rnd.NextDouble() * 0.1f + 0.05f,2);

                        l_usuarios.Add(new Cliente(nombre, tipoID, documento, telefono, usuario, contrasenia, nacionalidad, codigo, descuento));
                    }
                    else
                    {
                        throw new ArgumentException("La línea tiene una cantidad de campos incorrecta: " + linea);
                    }
                }
                return l_usuarios;
            }
            catch (System.IO.FileNotFoundException e)
            {
                l_usuarios.Add(new Huesped("juan", Usuario.e_tipoID.CC, 1000272699, 3023824114, "Juan123", "Juan123123", Usuario.e_Nacionalidad.Colombiano));
                l_usuarios.Add(new Cliente("andres", Usuario.e_tipoID.TI, 1001369111, 3205408414, "Andres324", "Andres23982983", Usuario.e_Nacionalidad.Otro, "CODIGO", (float)Math.Round((float)rnd.NextDouble() * 0.1f + 0.05f, 2)));
                l_usuarios.Add(new Cliente("test", Usuario.e_tipoID.CC, 1435369111, 5853508414, "test12345", "Test12345", Usuario.e_Nacionalidad.Colombiano, "CODIGOTEST", (float)Math.Round((float)rnd.NextDouble() * 0.1f + 0.05f, 2)));
                return l_usuarios;
            }
            catch (Exception e)
            {
                throw new Exception("Error en Importacion Usuarios Archivo:\n" + e);
            }
            
        }

        public Usuario Hacer_Login(string usu, string contra)
        {
            try
            {
                foreach (Usuario usuario in l_usuarios)
                {
                    if (usuario.UsuarioLogin.Equals(usu) && usuario.Contrasenia.Equals(contra))
                        return usuario;
                }
                return null;
            }
            catch (Exception error)
            {
                throw new Exception("Error en hacer Login:\n" + error);
            }
        }
        
    }
}
