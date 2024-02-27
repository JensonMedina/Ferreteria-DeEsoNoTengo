using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Dominio;

namespace Principal
{
    public partial class FrmCambiarPrecio : Form
    {
        private List<Marca> listaMarcas;
        public FrmCambiarPrecio()
        {
            InitializeComponent();
        }

        private void FrmCambiarPrecio_Load(object sender, EventArgs e)
        {
            MarcaDatos datos = new MarcaDatos();
            listaMarcas = datos.listarMarcasSP();
            cbxMarca.Items.Clear();
            cbxMarca.DataSource = listaMarcas;
            cbxMarca.SelectedIndex = -1;
        }

        private void txtPorcentaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            int maxLength = 3;
            // Verificar si la tecla presionada es un número o la tecla de retroceso
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (txtPorcentaje.Text.Length >= maxLength && e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true; // Si no es un número o retroceso, se ignora la tecla
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
                return;
            CambiarPrecio();
            MessageBox.Show("Se modifico el precio de todos los artículos de la marca: " + cbxMarca.Text);
            this.Close();
        }

        private bool Validar()
        {
            if(string.IsNullOrEmpty(cbxMarca.Text)
                || string.IsNullOrEmpty(txtPorcentaje.Text))
            {
                MessageBox.Show("Debes completar todos los campos");
                return true;
            }
            return false;
        }
        private void CambiarPrecio()
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                Marca marca = (Marca)cbxMarca.SelectedItem;
                int idMarca = marca.Id;

                // Obtener la lista de artículos de la misma marca
                List<Articulo> articulos = datos.ListarArticulosSP(idMarca);

                if (articulos != null && articulos.Count > 0)
                {
                    // Obtener el porcentaje del TextBox
                    if (int.TryParse(txtPorcentaje.Text, out int porcentaje))
                    {
                        // Verificar si se va a subir o bajar el precio
                        bool subir = rbtnSubir.Checked;

                        // Calcular el nuevo precio para cada artículo
                        foreach (var articulo in articulos)
                        {
                            decimal factor = 1 + (subir ? porcentaje / 100.0m : -porcentaje / 100.0m);
                            articulo.Precio = (float)((decimal)articulo.Precio * factor);

                        }

                        // Actualizar los precios en la base de datos
                        datos.ActualizarPreciosArticulos(articulos);

                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron artículos para la marca seleccionada.");
                }
            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show("Error al cambiar los precios: " + ex.Message);
            }
        }

    }
}
