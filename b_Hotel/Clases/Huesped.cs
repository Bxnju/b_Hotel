using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Huesped : Usuario
    {
        public Huesped(string nom, e_tipoID tipoid, long documento, long tel, string usu, string contra, Usuario.e_Nacionalidad nacion) : base(nom, tipoid, documento, tel, usu, contra, nacion)
        {}
    }
}
