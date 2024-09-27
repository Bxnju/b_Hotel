using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Restaurante
    {
        Oficina office;

        public Restaurante()
        {

        }

        public void Event_Handler_Comida()
        {
            Console.WriteLine("Procesando Alimentos");
        }

        public void Vender_Comida(List<Comida> listaComidas, bool alCuarto, Usuario usu)
        {
            office = Hotel.Obtener_Instancia_Hotel().oficinaHotel;
            try
            {
                office.eventoRestaurante += Event_Handler_Comida;
                office.Agregar_Comida_Reserva(usu, listaComidas, alCuarto);
                office.eventoRestaurante -= Event_Handler_Comida;
            }
            catch (Exception error)
            {
                throw new Exception("Error en Vender Comida " + error);
            }
        }
    }
}
