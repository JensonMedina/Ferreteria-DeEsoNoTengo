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
            CargarGrilla();
            MarcaDatos datos = new MarcaDatos();
            listaMarcas = datos.listarMarcasSP();
            cbxMarca.Items.Clear();
            cbxMarca.DataSource = listaMarcas;
            cbxMarca.SelectedIndex = -1;
        }
        private void CargarGrilla()
        {
            try
            {

                ArticuloDatos datos = new ArticuloDatos();
                CultureInfo Culture = new CultureInfo("es-AR");
                Culture.NumberFormat.CurrencySymbol = "ARS";
                listaArticulos = datos.listarArticulosSP();
                listaArticulos = listaArticulos.OrderByDescending(a => a.FechaModif).ToList();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["IdMarca"].Visible = false;
                dgvArticulos.Columns["FechaModif"].HeaderText = "Última Modificación";
                dgvArticulos.Columns["Codigo"].HeaderText = "Código";
                dgvArticulos.Columns["Descripcion"].HeaderText = "Descripción";
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "C2";
                dgvArticulos.Columns["Precio"].DefaultCellStyle.FormatProvider = Culture;
                txtCodigo.Text = "";
                txtRubro.Text = "";
                cbxMarca.Text = "";

            }
            catch (Exception ex)
            {
                throw;
                //MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.", ex.ToString());
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (ValidarFiltro())
                return;
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                string codigo = string.IsNullOrEmpty(txtCodigo.Text) ? null : txtCodigo.Text;
                string rubro = string.IsNullOrEmpty(txtRubro.Text) ? null : txtRubro.Text;
                Marca idMarca = string.IsNullOrEmpty(cbxMarca.Text) ? null : (Marca)cbxMarca.SelectedItem;
                dgvArticulos.DataSource =  datos.FiltrarArticulos(codigo, rubro, idMarca);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool ValidarFiltro()
        {
            if (string.IsNullOrEmpty(txtCodigo.Text) &&
                string.IsNullOrEmpty(txtRubro.Text) &&
                string.IsNullOrEmpty(cbxMarca.Text))
            {
                MessageBox.Show("Debes completar al menos uno de los campos");
                return true;
            }
            return false;
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            CargarGrilla();
            txtCodigo.Text = "";
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

                    throw;
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un artículo", "Error de seleccion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
