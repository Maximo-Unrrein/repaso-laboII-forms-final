using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Entidades
{
    public class ADO
    {
        // Definir el delegado y el evento
        public delegate void ApellidoUsuarioExistenteDelegado(object sender, EventArgs e);
        public event ApellidoUsuarioExistenteDelegado ApellidoUsuarioExistente;

        private string conexion;


        public ADO() 
        {
            this.conexion = "Server=.;Database=laboratorio_2;Trusted_Connection=True;";
        }

        public bool Agregar(Usuario usuario)
        {

            var usuarios = ObtenerTodos();

            // Verificar si el apellido ya existe
            if (usuarios.Any(u => u.Apellido == usuario.Apellido))
            {
                // Disparar el evento si el apellido ya existe
                ApellidoUsuarioExistente?.Invoke(this, EventArgs.Empty);
                return false;
            }
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(this.conexion))
                    {
                        connection.Open();
                        string query = "INSERT INTO Usuarios(nombre, apellido, dni, correo, clave) values (@nombre, @apellido, @dni, @correo, @clave)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                            command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                            command.Parameters.AddWithValue("@dni", usuario.Dni);
                            command.Parameters.AddWithValue("@correo", usuario.Correo);
                            command.Parameters.AddWithValue("@clave", usuario.Clave);

                            command.ExecuteNonQuery();

                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

                
            
        }


        public bool Eliminar(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.conexion))
                {
                    connection.Open();
                    string query = "DELETE FROM Usuarios Where dni = @dni";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        //command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        //command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@dni", usuario.Dni);
                        //command.Parameters.AddWithValue("@correo", usuario.Correo);
                        

                        command.ExecuteNonQuery();

                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        public bool Modificar(Usuario usuario)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(this.conexion))
                {
                    connection.Open();
                    string query = "UPDATE Usuarios SET nombre = @nombre, apellido = @apellido, correo = @correo, clave = @clave WHERE dni = @dni";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@dni", usuario.Dni);
                        command.Parameters.AddWithValue("@correo", usuario.Correo);
                        command.Parameters.AddWithValue("@clave", usuario.Clave);

                        command.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Usuario> ObtenerTodos()
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();
                using (SqlConnection connection = new SqlConnection("Server=.;Database=laboratorio_2;Trusted_Connection=True;"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Usuarios";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaUsuarios.Add(new Usuario(
                                reader["nombre"].ToString(),
                                reader["apellido"].ToString(),
                                Convert.ToInt32(reader["dni"]),
                                reader["correo"].ToString(),
                                reader["clave"].ToString()
                            ));
                        }
                    }
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<Usuario> ObtenerTodos(string apellidoUsuario)
        {
            try
            {
                List<Usuario> listaUsuarios = new List<Usuario>();
                using (SqlConnection connection = new SqlConnection("Server=.;Database=laboratorio_2;Trusted_Connection=True;"))
                {
                    connection.Open();
                    string query = "SELECT * FROM Usuarios WHERE apellido = @apellido";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@apellido", apellidoUsuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaUsuarios.Add(new Usuario(
                                    reader["nombre"].ToString(),
                                    reader["apellido"].ToString(),
                                    Convert.ToInt32(reader["dni"]),
                                    reader["correo"].ToString(),
                                    reader["clave"].ToString()
                                ));
                            }
                        }
                    }
                }
                return listaUsuarios;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
