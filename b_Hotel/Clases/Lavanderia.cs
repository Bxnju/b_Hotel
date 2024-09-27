using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Lavanderia
    {
        Oficina office;

        public Lavanderia()
        {

        }

        public void Event_Handler_Lavanderia()
        {
            Console.WriteLine("Procesando Lavanderia...");
        }

        public void Lavar_Planchar(List<Servicio> listaProductos, bool alCuarto, Usuario usu)
        {
            office = Hotel.Obtener_Instancia_Hotel().oficinaHotel;
            try
            {
                office.eventoLavanderia += Event_Handler_Lavanderia;
                office.Agregar_Servicios_Reserva(usu, listaProductos, alCuarto);
                office.eventoLavanderia -= Event_Handler_Lavanderia;
            }
            catch (Exception error)
            {
                throw new Exception("Error en Vender Comida " + error);
            }
        }
    }
}
