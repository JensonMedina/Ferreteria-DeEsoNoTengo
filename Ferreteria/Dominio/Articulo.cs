using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Rubro { get; set; }
        public string Descripcion { get; set; }
        public Marca Marca { get; set; }
        public float Precio { get; set; }
        public float Stock { get; set; }
        public DateTime FechaModif { get; set; }
        public int IdMarca { get; set; }
    }
}
