using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INvEstigacion
{
    public class JsonSeccion
    {
        public string Entidad { get; set; }
        public string Localidad { get; set; }
        public string Referencia { get; set; }
        public string Municipio { get; set; }
        public string Manzana { get; set; }
        public string Id { get; set; }
        public string Ubicacion { get; set; }
        public string Seccion { get; set; }
        public JObject Casilla { get; set; }
        public List<Casilla> Casillas { get; set; } = new List<Casilla>();
        public string NumLocalidad { get; set; }
        public string Titulo { get; set; }
        public string Distrito_federal { get; set; }
        public Point Punto { get; set; }
        public string Distrito_local { get; set; }
        public string Domicilio { get; set; }
    }
}
