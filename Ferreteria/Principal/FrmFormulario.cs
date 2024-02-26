using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Datos;

namespace Principal
{
    public partial class FrmFormulario : Form
    {
        private List<Marca> listaMarcas;
        private Articulo Articulo = null;
        public FrmFormulario()
        {
            InitializeComponent();
        }
        public FrmFormulario(Articulo articulo)
        {
            InitializeComponent();
            this.Articulo = articulo;
        }

        private void FrmFormulario_Load(object sender, EventArgs e)
        {
            MarcaDatos datos = new MarcaDatos();
            listaMarcas = datos.listarMarcasSP();
            cbxMarca.Items.Clear();
            cbxMarca.DataSource = listaMarcas;
            cbxMarca.SelectedIndex = -1;
            if(Articulo != null)
            {
                txtCodigo.Text = Articulo.Codigo;
                txtRubro.Text = Articulo.Rubro;
                txtDescripcion.Text = Articulo.Descripcion;
                cbxMarca.Text = Articulo.Marca.Descripcion;
                string textoFormateado = String.Format("{0:0.################}", Articulo.Precio);
                txtPrecio.Text = textoFormateado;
                txtStock.Text = Articulo.Stock.ToString();
            }

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ValidarArticulo())
                return;
            
            if (Articulo == null)
            {
                //si articulo es nulo significa que se va a agregar un nuevo articulo.
                Articulo = new Articulo();
                ArticuloDatos datos = new ArticuloDatos();
                try
                {
                    CargarDatos();
                    datos.AgregarArticuloSP(Articulo);
                    MessageBox.Show("Artículo agregado exitosamente");
                    this.Close();
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else 
            {
                //si articulo no es nulo, significa que se va a modificar un articulo
                //Aca voy a hacer la logica para modificar un articulo
                ArticuloDatos datos = new ArticuloDatos();
                try
                {
                    CargarDatos();
                    datos.ModificarArticuloSP(Articulo);
                    MessageBox.Show("Artículo modificado exitosamente");
                    this.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void CargarDatos()
        {
            Articulo.Codigo = txtCodigo.Text;

            Articulo.Descripcion = string.IsNullOrEmpty(txtDescripcion.Text) ? "" : txtDescripcion.Text;
            Articulo.Rubro = string.IsNullOrEmpty(txtRubro.Text) ? "" : txtRubro.Text;
            Articulo.Marca = new Marca();
            if (cbxMarca.SelectedIndex >= 0)
            {
                Articulo.Marca = (Marca)cbxMarca.SelectedItem;
                Articulo.Marca.Descripcion = cbxMarca.Text;
                Articulo.IdMarca = cbxMarca.SelectedIndex; 
            }
            else
            {
                Articulo.Marca.Id = 0;
                Articulo.Marca.Descripcion = "";
                Articulo.IdMarca = 0;
            }
            Articulo.Precio = string.IsNullOrEmpty(txtPrecio.Text) ? 0f : float.Parse(txtPrecio.Text);
            Articulo.Stock = string.IsNullOrEmpty(txtStock.Text) ? 0f : float.Parse(txtStock.Text);
            Articulo.FechaModif = DateTime.Now;
        }

        private bool ValidarArticulo()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debes completar el campo de código");
                return true; //Indica que la validacion ha fallado
            }
                

            return false; //Indica que la validacion no falló.
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si la tecla presionada no es un número ni la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Si no es un número ni retroceso, se ignora la tecla
            }
        }
    }
}
