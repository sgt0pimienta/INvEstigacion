using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Mis adiciones haha
//Librería para JSON
using Newtonsoft.Json;
//No sé por que lo puse
using System.IO;
using System.Web;
//Trae WebClient
using System.Net;
//Para leer y escribir CSVs
using Microsoft.VisualBasic;

namespace INvEstigacion
{
    class Program
    {
        //Jalisco = 14, CDMX = 9, Yucatán = 31, NL = 19
        private WebClient downloader;
        private List<string> seccionesJal;
        private List<string> seccionesNln;
        private List<string> seccionesYcn;
        private List<string> seccionesCdmx;
        private List<JsonSeccion> listaJsons;

        static void Main(string[] args)
        {

        }

        public void SacarJsons(List<string> listaDeSecciones, string estado)
        {
            int cuentaDeSecciones = 0;
            string codigoEstado;
            List<string> jsonEstado;

            switch (estado)
            {
                case "Jalisco": codigoEstado = "14"; break;
                case "Nuevo Leon": codigoEstado = "19"; break;
                case "Yucatan": codigoEstado = "31"; break;
                case "CDMX": codigoEstado = "9"; break;
                default: throw new Exception("link malo. O eres mala persona o no hay ningún código de estado asignado al estado que intentaste buscar (el programa no soporta ese estado)");
            }

            foreach (string seccion in listaDeSecciones)
            {
                cuentaDeSecciones += 1;
                var jsonDescargado = downloader.DownloadString("https://ubicatucasilla.ine.mx/api/casillas/" + codigoEstado + "/" + cuentaDeSecciones + "/casillas.json");
                JsonSeccion datosSeccion = JsonConvert.DeserializeObject<JsonSeccion>(jsonDescargado);
                listaJsons.Add(datosSeccion);
            }
        }

        public void JsonsToText(List<JsonSeccion> lista, string tipo)
        {
            if (tipo == "csv")
            {
                string csv = String.Join(",", lista);

            }
            else
            {

            }
        }
    }

    class JsonSeccion
    {

    }
}
