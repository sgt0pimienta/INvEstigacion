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
using Newtonsoft.Json.Linq;
using System.Threading;

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

        static Random hortler = new Random();

        public Manager()
        {
            
            seccionesJal = new List<string>();
            seccionesNln = new List<string>();
            seccionesCdmx = new List<string>();
            seccionesYcn = new List<string>();
            downloader = new WebClient();

        }

        public void HazTuCosa()
        {
            CargaTusCeseves();
            var seccionse = seccionesJal.Where(x => x.Equals("3099")).ToList();
            DescargarJsons(seccionesNln, "nuevoleon");
            //SacarJsons(seccionse, "jalisco");
        }

        public void CargaTusCeseves()
        {
            string read;
            Array split;

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jalisco.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesJal.Add(line); }

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

        public void DescargarJsons(List<string> listaDeSecciones, string estado)
        {
            string codigoEstado;

            switch (estado)
            {
                case "jalisco": codigoEstado = "14"; break;
                case "nuevoleon": codigoEstado = "19"; break;
                case "yucatan": codigoEstado = "31"; break;
                case "cdmx": codigoEstado = "9"; break;
                default: throw new Exception("link malo. O eres mala persona o no hay ningún código de estado asignado al estado que intentaste buscar (el programa no soporta ese estado)");
            }

            downloader.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.87 Safari/537.36");

            foreach (string idSeccion in listaDeSecciones)
            {

                Console.WriteLine($"Vamos en la {idSeccion}");

                var url = "https://ubicatucasilla.ine.mx/api/casillas/" + codigoEstado + "/" + idSeccion + "/casillas.json";

                var ruta = @"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jsons\" +  estado + "-" + idSeccion + ".json";

                downloader.DownloadFile(url, ruta);

                Thread.Sleep(hortler.Next(10, 100));

                
                
            }
        }

        public void SacarJsons(List<string> listaDeSecciones, string estado)
        {
            int cuentaDeSecciones = 0;
            List<JsonSeccion> datosEstado = new List<JsonSeccion>();

          

            foreach (string idSeccion in listaDeSecciones)
            {
                cuentaDeSecciones += 1;
                //var jsonDescargado = downloader.DownloadString(url);

                var ruta = @"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jsons\" + estado + "-" + idSeccion + ".json";

                var jsonDescargado = File.ReadAllText(ruta);

                JObject objetoCompleto = (JObject)JsonConvert.DeserializeObject(jsonDescargado);


                foreach (var dataIdes in objetoCompleto.First.First) {

                    var subseccion = JsonConvert.DeserializeObject<JsonSeccion>(dataIdes.First.ToString());

                    foreach (var jsonCasilla in subseccion.Casilla.First) {
                        var casilla = JsonConvert.DeserializeObject<Casilla>(jsonCasilla.ToString());
                        subseccion.Casillas.Add(casilla);
                    }

                    datosEstado.Add(subseccion);

                }
                
                



         
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
