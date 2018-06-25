using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Librería para JSON
using Newtonsoft.Json;
//Readwrite, Webclient
using System.IO;
using System.Web;
using System.Net;
//readwrite csvs
using Microsoft.VisualBasic;

namespace INvEstigacion
{
    class Manager
    {
        //Jalisco = 14, CDMX = 9, Yucatán = 31, NL = 19
        public WebClient downloader;
        public List<string> seccionesJal;
        public List<string> seccionesNln;
        public List<string> seccionesYcn;
        public List<string> seccionesCdmx;

        public Manager()
        {
            string read;
            Array split;
            seccionesJal = new List<string>();
            seccionesNln = new List<string>();
            seccionesCdmx = new List<string>();
            seccionesYcn = new List<string>();
            downloader = new WebClient();

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jalisco.csv");
            split = read.Split(',');
            foreach(string line in split) { seccionesJal.Add(line); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\nuevoleon.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesNln.Add(line); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\yucatan.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesYcn.Add(line); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\cdmx.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesCdmx.Add(line); }
        }

        public void SacarJsons(List<string> listaDeSecciones, string estado)
        {
            int cuentaDeSecciones = 0;
            string codigoEstado;
            List<JsonSeccion> datosEstado = new List<JsonSeccion>();

            switch (estado)
            {
                case "jalisco": codigoEstado = "14"; break;
                case "nuevoleon": codigoEstado = "19"; break;
                case "yucatan": codigoEstado = "31"; break;
                case "cdmx": codigoEstado = "9"; break;
                default: throw new Exception("link malo. O eres mala persona o no hay ningún código de estado asignado al estado que intentaste buscar (el programa no soporta ese estado)");
            }

            foreach (string seccion in listaDeSecciones)
            {
                cuentaDeSecciones += 1;
                var jsonDescargado = downloader.DownloadString("https://ubicatucasilla.ine.mx/api/casillas/" + codigoEstado + "/" + cuentaDeSecciones + "/casillas.json");
                datosEstado.Add(JsonConvert.DeserializeObject<JsonSeccion>(jsonDescargado));
            }

            Console.WriteLine("secciones: " + cuentaDeSecciones.ToString());
            WriteDeserialized(datosEstado, "csv");
        }

        public void WriteDeserialized(List<JsonSeccion> lista, string tipo)
        {
            if (tipo == "csv")
            {
                string csv = String.Join(",", lista);
                System.IO.File.WriteAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\outputcsv.txt", csv);
            }
            else
            {

            }
        }
    }
}
