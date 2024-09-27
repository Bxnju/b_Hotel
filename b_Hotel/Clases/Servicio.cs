using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Servicio
    {
        private readonly int precLavada= 3000;
        private readonly int precPlachada= 3500;

        public enum e_tipos_servicio { Lavada , Planchada }

        private readonly int precio;
        private readonly e_tipos_servicio type;

        public int Precio { get => precio; }
        public e_tipos_servicio Type { get => type; }

        public Servicio(e_tipos_servicio tipo)
        {
            type = tipo;

            precio = type switch
            {
                e_tipos_servicio.Lavada => precLavada,
                e_tipos_servicio.Planchada => precPlachada,
                _ => throw new Exception("Servicio Inexistente"),
            };
        }
    }
}
