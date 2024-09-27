using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Habitacion_Sencilla : Habitacion
    {
        public Habitacion_Sencilla() : base()
        {
            Ocupada = false;
            Reservada = false;
            PrecioNoche = precioSencilla;
            ContId++;
            Id = ContId;
        }
    }
}
