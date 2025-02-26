using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Entidades
{
    public class Manejadora
    {
        public static bool EscribirArchivo(List<Usuario> usuarios, string path)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine($"{DateTime.Now}: Apellido repetido - {usuarios[0].Apellido}");
                    foreach (var usuario in usuarios)
                    {
                        sw.WriteLine($"Email: {usuario.Correo}");
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SerializarJSON(List<Usuario> usuarios, string path)
        {
            try
            {
                string json = JsonConvert.SerializeObject(usuarios, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(path, json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeserializarJSON(string path, out List<Usuario> usuarios)
        {
            usuarios = new List<Usuario>();

            try
            {
                string json = File.ReadAllText(path);
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
