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
        public Reserializer csvWrite;
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
            csvWrite = new Reserializer();

        }

        public void HazTuCosa()
        {
            CargaTusCeseves();
            var jasons = SacarJsons(seccionesJal, "jalisco");
            Cesevear(jasons, "jalisco");
        }

        public void CargaTusCeseves()
        {
            string read;
            Array split;

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jalisco.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesJal.Add(line.Trim()); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\nuevoleon.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesNln.Add(line.Trim()); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\yucatan.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesYcn.Add(line.Trim()); }

            read = File.ReadAllText(@"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\cdmx.csv");
            split = read.Split(',');
            foreach (string line in split) { seccionesCdmx.Add(line.Trim()); }
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

        public List<Seccion> SacarJsons(List<string> listaDeSecciones, string estado)
        {
            int cuentaDeSecciones = 0;
            List<Seccion> datosEstado = new List<Seccion>();

            foreach (string idSeccion in listaDeSecciones)
            {
                var seccion = new Seccion() { IdSeccion = idSeccion };

                cuentaDeSecciones += 1;
                var ruta = @"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\jsons\" + estado + "-" + idSeccion + ".json";

                var jsonDescargado = File.ReadAllText(ruta);

                JObject objetoCompleto = (JObject)JsonConvert.DeserializeObject(jsonDescargado);


                foreach (var dataIdes in objetoCompleto.First.First) {

                    var subseccion = JsonConvert.DeserializeObject<Subseccion>(dataIdes.First.ToString());

                    var t = 0;
                    foreach (var jsonCasilla in subseccion.Casilla.First) {
                        t++;
                        var casilla = JsonConvert.DeserializeObject<Casilla>(jsonCasilla.ToString());
                        subseccion.Casillas.Add(casilla);
                    }

                    seccion.Subsecciones.Add(subseccion);

                }

                datosEstado.Add(seccion);

            }

            Console.WriteLine("secciones: " + cuentaDeSecciones.ToString());

            return datosEstado;
        }

        public void Cesevear(List<Seccion> seccions, string estado)
        {
            var ceseves = new List<PuntoCsv>();

            foreach(var seccion in seccions)
            {
                foreach( var subseccion in seccion.Subsecciones)
                {
                    var csv = new PuntoCsv()
                    {
                        Seccion = seccion.IdSeccion,
                        Direccion = subseccion.Domicilio,
                        TipoDeCasillas = subseccion.Titulo,
                        Casillas = subseccion.Titulo.Split(',').Count(),
                        Ubicacion = subseccion.Ubicacion,
                        Lng = subseccion.Punto.Coordinates[0],
                        Lat = subseccion.Punto.Coordinates[1],
                    };
                    ceseves.Add(csv);
                }
            }


            var ruta = @"C:\Users\sgt0pimienta\Desktop\INvEstigate Output\" + estado + "-puntos.csv";
            using (TextWriter wr = File.CreateText(ruta))
            {
                var csv = new CsvHelper.CsvWriter(wr);
                csv.WriteRecords(ceseves);
            }
                
        }

    }
}
