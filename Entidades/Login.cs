namespace Entidades
{
    public class Login
    {
        protected string email;
        protected string pass;

        public Login(string correo, string pass)
        {
            this.email = correo;
            this.pass = pass;
        }

        public string Email
        {
            get
            {
                return email;
            }
        }

        public string Pass
        {
            get
            {
                return pass;
            }
        }


        public bool Loguear()
        {
            List<Usuario> usuarios = ADO.ObtenerTodos();
            foreach(Usuario usuario in usuarios)
            {
                if(usuario.Correo == this.Email && usuario.Clave == this.Pass)
                {
                    return true;
                }
                
            }
            return false;

        }
    }
}