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
        public FrmFormulario()
        {
            InitializeComponent();
        }

        private void FrmFormulario_Load(object sender, EventArgs e)
        {
            MarcaDatos datos = new MarcaDatos();
            listaMarcas = datos.listarMarcasSP();
            cbxMarca.DataSource = listaMarcas;
            cbxMarca.SelectedIndex = -1;

        }
    }
}
