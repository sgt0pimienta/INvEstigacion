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
        private WebClient downloader;
        private List<string> seccionesJalisco;
        private List<string> seccionesMty;
        private List<string> seccionesYcn;
        private List<string> seccionesCDMX;

        static void Main(string[] args)
        {

        }

        public void SacarJsons(List<string> listaDeSecciones)
        {
            foreach (string seccion in listaDeSecciones)
            {
                //downloader.DownloadFile();    
            }
        }
    }
}
