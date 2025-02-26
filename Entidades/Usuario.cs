using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuario
    {
        private string apellido;
        private string nombre;
        private int dni;
        private string correo;
        private string clave;

        public Usuario() { }
        public Usuario(string apellido, string nombre, int dni, string correo)
        {
            this.Apellido = apellido;
            this.Nombre = nombre;
            this.Dni = dni;
            this.Correo = correo;
        }
        public Usuario(string apellido, string nombre, int dni, string correo, string clave) : this(apellido, nombre, dni, correo )
        {
            
            this.Clave = clave;
        }

        public string Apellido { get => apellido; set => apellido = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Dni { get => dni; set => dni = value; }
        public string Correo { get => correo; set => correo = value; }
        public string Clave { get => clave; set => clave = value; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Nombre);
            sb.AppendLine(Apellido);
            sb.AppendLine($"{Dni}");
            sb.AppendLine(Correo);

            return sb.ToString();
        }
    }
}
