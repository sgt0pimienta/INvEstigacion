using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Xml;

namespace INvEstigacion
{
    public class Reserializer
    {
        public  XmlWriter xmlWrite;
        public CsvWriter csvWrite;


        public Reserializer()
        {

        }

        public void SerializeCSV()
        {
            Console.WriteLine("Transcripción básica CSV. Sólo incluye lo necesario");

        }

        public void SerializeXML(Subseccion jsonSeccion)
        {
            Console.WriteLine("Transcripción básica XML. Sólo incluye lo necesario");

        }


        //EJEMPLO, NO USAR
        private void WriteDeserialized(List<Subseccion> lista, string tipo)
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
