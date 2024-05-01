using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArchivoMasivo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "~/../../../../Files/Candidatos.txt";
            string directorio = Path.GetFullPath(path);
            if(File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                Console.WriteLine("Si existe el archivo");
            }
            Console.ReadKey();
        }
    }
}
