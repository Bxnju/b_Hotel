using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Producto
    {
        private readonly int precGaseosa = 3000;
        private readonly int precAgua = 3500;
        private readonly int precVino = 50000;
        private readonly int precLicor = 25000;
        private readonly int precBata = 70000;
        private readonly int precKitAseo = 9000;

        public enum e_tipos_producto { Gaseosa , Agua , Vino , Licor , Bata , KitAseo }

        private readonly int precio;
        private readonly e_tipos_producto type;

        public int Precio { get => precio; }
        public e_tipos_producto Type { get => type; }

        public Producto(e_tipos_producto tipo) 
        { 
            type = tipo;

            precio = type switch
            {
                e_tipos_producto.Gaseosa => precGaseosa,
                e_tipos_producto.Agua => precAgua,
                e_tipos_producto.Vino => precVino,
                e_tipos_producto.Licor => precLicor,
                e_tipos_producto.Bata => precBata,
                e_tipos_producto.KitAseo => precKitAseo,
                _ => throw new Exception("Objeto Inexistente"),
            };
        }
    }
}
