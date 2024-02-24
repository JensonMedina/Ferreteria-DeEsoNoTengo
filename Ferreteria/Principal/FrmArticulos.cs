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
    public partial class FrmArticulos : Form
    {
        private List<Articulo> listaArticulos;
        public FrmArticulos()
        {
            InitializeComponent();
        }

        private void FrmArticulos_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            try
            {

                ArticuloDatos datos = new ArticuloDatos();
                listaArticulos = datos.listarArticulosSP();
                listaArticulos = listaArticulos.OrderByDescending(a => a.FechaModif).ToList();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["IdMarca"].Visible = false;
                dgvArticulos.Columns["FechaModif"].HeaderText = "Última Modificación";
                dgvArticulos.Columns["Codigo"].HeaderText = "Código";
                dgvArticulos.Columns["Descripcion"].HeaderText = "Descripción";

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
    }
}
