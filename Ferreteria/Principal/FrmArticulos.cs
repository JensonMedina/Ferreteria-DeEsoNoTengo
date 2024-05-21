using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Datos;
using Dominio;

namespace Principal
{
    public partial class FrmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        private List<Marca> listaMarcas;

        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            try
            {
                CargarGrilla();
                CargarComboBox();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
   
        private void CargarComboBox()
        {
            MarcaDatos datos = new MarcaDatos();
            try
            {
                listaMarcas = datos.listarMarcasSP();
                cbxMarca.DataSource = null;
                cbxMarca.Items.Clear();
                cbxMarca.DataSource = listaMarcas;
                cbxMarca.SelectedIndex = -1;
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error. Intente de nuevo más tarde.");
            }
        }

        private void CargarGrilla()
        {
            ArticuloDatos datos = new ArticuloDatos();

            try
            {
                listaArticulos = datos.ListarArticulosSP();
                listaArticulos = listaArticulos.OrderByDescending(a => a.FechaModif).ToList();


                dgvArticulos.DataSource = listaArticulos;


                // Configuración de las columnas de la grilla
                CultureInfo Culture = new CultureInfo("es-AR");
                Culture.NumberFormat.CurrencySymbol = "ARS";
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["IdMarca"].Visible = false;
                dgvArticulos.Columns["FechaModif"].HeaderText = "Última Modificación";
                dgvArticulos.Columns["Codigo"].HeaderText = "Código";
                dgvArticulos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvArticulos.Columns["Precio"].DefaultCellStyle.FormatProvider = Culture;

                // Limpiar los filtros
                txtDescripcion.Text = "";
                txtRubro.Text = "";
                cbxMarca.Text = "";
            }
            catch (Exception)
            {
                //throw;
                MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.");
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmFormulario frmFormulario = new FrmFormulario();
            frmFormulario.ShowDialog();
            CargarGrilla();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo Seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                Articulo Seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                FrmFormulario frmFormulario = new FrmFormulario(Seleccionado);
                frmFormulario.ShowDialog();
                CargarGrilla();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un artículo", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void BuscarArticulos()
        {
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                string descripcion = string.IsNullOrEmpty(txtDescripcion.Text) ? null : txtDescripcion.Text;
                string rubro = string.IsNullOrEmpty(txtRubro.Text) ? null : txtRubro.Text;
                Marca idMarca = string.IsNullOrEmpty(cbxMarca.Text) ? null : (Marca)cbxMarca.SelectedItem;
                dgvArticulos.DataSource = datos.FiltrarArticulos(descripcion, rubro, idMarca);
            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.");
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ValidarFiltro())
                return;
            BuscarArticulos();
        }
        private bool ValidarFiltro()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text) &&
                string.IsNullOrEmpty(txtRubro.Text) &&
                (string.IsNullOrEmpty(cbxMarca.Text) ||
                cbxMarca.SelectedIndex < 0))
            {
                MessageBox.Show("Debes completar al menos uno de los campos");
                return true;
            }
            return false;
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            txtDescripcion.Text = "";
            txtRubro.Text = "";
            cbxMarca.SelectedIndex = -1;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null && dgvArticulos.CurrentRow.DataBoundItem != null)
            {
                ArticuloDatos datos = new ArticuloDatos();
                try
                {
                    Articulo seleccionado;
                    DialogResult Resultado = MessageBox.Show("¿Esta seguro que quiere eliminar?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (Resultado == DialogResult.Yes)
                    {
                        seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                        datos.EliminarArticulo(seleccionado.Id);
                        MessageBox.Show("Artículo eliminado exitosamente");
                        CargarGrilla();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.");
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un artículo", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCambiarPrecio_Click(object sender, EventArgs e)
        {
            FrmCambiarPrecio frmCambiarPrecio = new FrmCambiarPrecio();
            frmCambiarPrecio.ShowDialog();
            CargarGrilla();
        }

        private void btnMarcas_Click(object sender, EventArgs e)
        {
            FrmMarcas frmMarcas = new FrmMarcas();
            frmMarcas.ShowDialog();
            CargarComboBox();
        }

        private void txtRubro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (ValidarFiltro())
                    return;
                BuscarArticulos();
            }
        }

        private void cbxMarca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!cbxMarca.DroppedDown) // Verificar si el ComboBox no está desplegado (modo de edición)
                {
                    if (ValidarFiltro())
                        return;
                    BuscarArticulos();
                    e.SuppressKeyPress = true; // Evitar que el ComboBox maneje la tecla Enter
                }
            }
        }

   
    }
}
