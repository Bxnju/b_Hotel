using b_Hotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Habitacion_Ejecutiva : Habitacion , I_MiniBar
    {
        private List<Producto> l_minibar;
        public List<Producto> L_minibar { get => l_minibar; set => l_minibar = value; }

        public Habitacion_Ejecutiva() : base()
        {
            Ocupada = false;
            Reservada = false;
            PrecioNoche = precioEjecutiva;

            L_minibar = miniEjecutiva.ToList();
            ContId++;
            Id = ContId;
        }

        public void Llenar_MiniBar()
        {
            Console.WriteLine("ReLlenando MiniBar");
            L_minibar = miniEjecutiva.ToList();
        }
        public void Tiene_Producto()
        {
            try
            {
                foreach (Producto.e_tipos_producto tipo in tiposEje)
                {
                    bool found = false;
                    foreach (Producto producto in L_minibar)
                    {
                        if (producto.Type == tipo)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                        Llenar_MiniBar();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error en Tiene Producto Eje");
            }
        }

        public override string Reportar_Estado()
        {
            string salida = "MiniBar -> ", productos = "";
            try
            {
                foreach (Producto item in L_minibar)
                {
                    productos += $"\n  - {item.Type}";
                }

                salida += productos;

                return base.Reportar_Estado() + salida;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
