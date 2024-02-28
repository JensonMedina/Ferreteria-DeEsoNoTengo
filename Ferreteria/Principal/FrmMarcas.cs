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
    public partial class FrmMarcas : Form
    {
        private List<Marca> listaMarcas;
        public FrmMarcas()
        {
            InitializeComponent();
        }

        private void FrmMarcas_Load(object sender, EventArgs e)
        {
            CargarGrilla();
        }
        private void CargarGrilla()
        {
            try
            {

                MarcaDatos datos = new MarcaDatos();
                listaMarcas = datos.listarMarcasSP();
                dgvMarcas.DataSource = listaMarcas;
                dgvMarcas.Columns["Id"].Visible = false;

            }
            catch (Exception)
            {
                MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMarca.Text))
            {
                MarcaDatos datos = new MarcaDatos();
                Marca marca = new Marca();
                try
                {
                    marca.Descripcion = txtMarca.Text;
                    datos.AgregarMarcasSP(marca);
                    MessageBox.Show("Se agregó la marca exitosamente");
                    CargarGrilla();
                    txtMarca.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Hubo un error. Intente de nuevo mas tarde.");
                }
            }
            else
            {
                MessageBox.Show("Debes completar el campo");
            }
        }
    }
}
