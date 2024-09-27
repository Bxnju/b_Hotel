using b_Hotel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public abstract class Habitacion
    {
        internal readonly int precioSencilla = 200000;
        internal readonly int precioEjecutiva = 350000;
        internal readonly int precioSuite = 500000;


        internal readonly List<Producto> miniEjecutiva = new()
                {
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.KitAseo),
                    new Producto(Producto.e_tipos_producto.Gaseosa),
                    new Producto(Producto.e_tipos_producto.Gaseosa)
                };

        internal readonly List<Producto> miniSuite = new()
                {
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.Licor),
                    new Producto(Producto.e_tipos_producto.KitAseo),
                    new Producto(Producto.e_tipos_producto.KitAseo),
                    new Producto(Producto.e_tipos_producto.KitAseo),
                    new Producto(Producto.e_tipos_producto.Gaseosa),
                    new Producto(Producto.e_tipos_producto.Gaseosa),
                    new Producto(Producto.e_tipos_producto.Gaseosa),
                    new Producto(Producto.e_tipos_producto.Gaseosa),
                    new Producto(Producto.e_tipos_producto.Bata),
                    new Producto(Producto.e_tipos_producto.Bata)
                };

        internal readonly List<Producto.e_tipos_producto> tiposEje = new()
        {
            Producto.e_tipos_producto.Licor,
            Producto.e_tipos_producto.KitAseo,
            Producto.e_tipos_producto.Gaseosa
        };

        internal readonly List<Producto.e_tipos_producto> tiposSui = new()
        {
            Producto.e_tipos_producto.Licor,
            Producto.e_tipos_producto.KitAseo,
            Producto.e_tipos_producto.Gaseosa,
            Producto.e_tipos_producto.Bata
        };

        private int precioNoche;
        private bool ocupada;
        private bool reservada;
        private static short contId = 100;
        private short id = 0;


        public bool Ocupada { get => ocupada; set => ocupada = value; }
        public int PrecioNoche { get => precioNoche; set => precioNoche = value; }
        public bool Reservada { get => reservada; set => reservada = value; }
        public static short ContId { get => contId; set => contId = value; }
        public short Id { get => id; set => id = value; }

        public virtual string Reportar_Estado()
        {
            try
            {
                return  $"--- Habitacion {Id} ---\n" +
                    $"Tipo -> {GetType()}\n" +
                    $"Ocupada -> {ocupada}\n" +
                    $"Reservada -> {reservada}\n";
            }
            catch (Exception e) 
            { 
                throw new Exception("Error en Reportar Estado:\n" + e);
            }
        }
    }
}
