using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Tienda
    {
        Oficina office;

        public Tienda()
        {

        }

        public void Event_Handler_Tienda()
        {
            Console.WriteLine("Procesando Productos");
        }

        public void Vender_Productos(List<Producto> listaProductos, bool alCuarto, Usuario usu)
        {
            office = Hotel.Obtener_Instancia_Hotel().oficinaHotel;
            try
            {
                office.eventoTienda += Event_Handler_Tienda;
                office.Agregar_Productos_Reserva(usu, listaProductos, alCuarto);
                office.eventoTienda -= Event_Handler_Tienda;
            }
            catch (Exception error)
            {
                throw new Exception("Error en Vender Comida " + error);
            }
        }
    }
}
