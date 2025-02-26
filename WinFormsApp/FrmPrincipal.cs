using Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    ///Agregar manejo de excepciones en TODOS los lugares críticos!!!

    public delegate void DelegadoThreadConParam(object param);



    public partial class FrmPrincipal : Form
    {
        protected Task hilo;
        protected CancellationTokenSource cts;
        private Thread hiloUsuarios;
        private bool ejecutarHilo = true;

        public FrmPrincipal()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = "Cambiar por su apellido y nombre";
            MessageBox.Show(this.Text);         
        }

        ///
        /// CRUD
        ///
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmListado frm = new FrmListado();
            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show(this);
        }

        ///
        /// VER LOG
        ///
        private void verLogToolStripMenuItem_Click(object sender, EventArgs e)
        {


            // Configurar el OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Abrir archivo de usuarios",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "Log files (*.log)|*.log",
                DefaultExt = "log",
                FileName = "usuarios.log"
            };

            // Mostrar el diálogo y verificar si el usuario seleccionó un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;

                // Leer el archivo de log y mostrarlo en un TextBox o RichTextBox
                try
                {
                    string contenido = File.ReadAllText(path);
                    txtUsuariosLog.Text = contenido; // Asumiendo que tienes un TextBox llamado txtUsuariosLog
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al leer el archivo: {ex.Message}");
                }
            }

            //DialogResult rta = DialogResult.Cancel;///Reemplazar por la llamada al método correspondiente del OpenFileDialog

            //if (rta == DialogResult.OK)
            //{
            //    /// Mostrar en txtUsuariosLog.Text el contenido del archivo .log
            //}
            //else
            //{
            //    MessageBox.Show("No se muestra .log");
            //}
        }

        ///
        /// DESERIALIZAR JSON
        ///
        private void deserializarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Usuario> listado = null;
            string path = ""; /// Reemplazar por el path correspondiente

            bool todoOK = false; /// Reemplazar por la llamada al método correspondiente de Manejadora

            if (todoOK)
            {
                this.txtUsuariosLog.Clear();

                /// Mostrar en txtUsuariosLog.Text el contenido de la deserialización.
            }
            else
            {
                MessageBox.Show("NO se pudo deserializar a JSON");
            }

        }

        ///
        /// TASK
        ///
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Iniciar el hilo secundario
            ejecutarHilo = true;
            hiloUsuarios = new Thread(ActualizarListadoUsuarios);
            hiloUsuarios.Start();

            // Deshabilitar el ítem del menú para evitar múltiples clics
            this.taskToolStripMenuItem.Enabled = false;
        }

       



        ///PARA ACTUALIZAR LISTADO DESDE BD EN HILO
        public void ActualizarListadoUsuarios(object param)
        {
            while (ejecutarHilo)
            {
                try
                {
                    // Obtener la lista de usuarios desde la base de datos
                    var usuarios = ADO.ObtenerTodos();

                    // Actualizar la interfaz de usuario en el hilo principal
                    this.Invoke((MethodInvoker)delegate
                    {
                        lstUsuarios.DataSource = usuarios; // Asumiendo que tienes un ListBox llamado lstUsuarios
                        CambiarColores(lstUsuarios);
                    });

                    // Esperar 1.5 segundos
                    Thread.Sleep(1500);
                }
                catch (Exception ex)
                {
                    // Manejar excepciones (puedes loguear el error o mostrar un mensaje)
                    MessageBox.Show($"Error en el hilo: {ex.Message}");
                }
            }

        }

        private void CambiarColores(ListBox listBox)
        {
            // Alternar colores de fondo y fuente
            if (listBox.BackColor == Color.Black)
            {
                listBox.BackColor = Color.White;
                listBox.ForeColor = Color.Black;
            }
            else
            {
                listBox.BackColor = Color.Black;
                listBox.ForeColor = Color.White;
            }
        }


        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            /////CANCELAR HILO
            //this.cts.Cancel();


            // Detener el hilo cuando se cierra el formulario
            ejecutarHilo = false;

            // Esperar a que el hilo termine
            if (hiloUsuarios != null && hiloUsuarios.IsAlive)
            {
                hiloUsuarios.Join(); // Esperar a que el hilo termine
            }
        }
    }
}
