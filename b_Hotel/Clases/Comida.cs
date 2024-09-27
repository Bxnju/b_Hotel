using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace b_Hotel.Clases
{
    public class Comida
    {
        private readonly int precDesayuno = 15000;
        private readonly int precCena = 20000;
        private readonly int precAlmuerzo = 25000;
        
        public enum e_tipos_comida { Desayuno, Cena, Almuerzo }

        private readonly int precio;
        private readonly e_tipos_comida type;

        public int Precio { get => precio; }
        public e_tipos_comida Type { get => type; }

        public Comida(e_tipos_comida tipo)
        {
            type = tipo;

            precio = type switch
            {
                e_tipos_comida.Cena => precCena,
                e_tipos_comida.Almuerzo => precAlmuerzo,
                e_tipos_comida.Desayuno => precDesayuno,
                _ => throw new Exception("Comida Inexistente"),
            };
        }
    }
}
