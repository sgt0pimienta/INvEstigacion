using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INvEstigacion
{
    public class Casilla
    {
        public string Nombre_largo { get; set; }
        public string Nombre { get; set; }
        public string Id { get; set; }
        public Letra Letra { get; set; }
    }

    public class Letra {
        public string Inicio { get; set; }
        public string Termino { get; set; }
        public int Mujeres { get; set; }
        public int Hombres { get; set; }
    }
}
