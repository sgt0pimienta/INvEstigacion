using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INvEstigacion
{
    class JsonSeccion
    {
        string Entidad { get; set; }
        string Localidad { get; set; }
        string Referencia { get; set; }
        string Municipio { get; set; }
        string Manzana { get; set; }
        string Id { get; set; }
        string Ubicacion { get; set; }
        string Seccion { get; set; }
        List<Casilla> Casillas { get; set; }
        string NumLocalidad { get; set; }
        string Titulo { get; set; }
        string Distrito_federal { get; set; }
        List<Point> Coordinates { get; set; }
        string Distrito_local { get; set; }
        string Domicilio { get; set; }
    }
}
