using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class ArticuloDatos
    {
        public List<Articulo> listarArticulosSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearStoredProcedure("storedListarArticulos");
                datos.EjecutarLectura();

                while (datos.lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.lector["Id"];
                    aux.Codigo = (string)datos.lector["Codigo"];
                    aux.Rubro = datos.lector["Rubro"] is DBNull ? "" : (string)datos.lector["Rubro"];
                    aux.Descripcion = datos.lector["Descripcion"] is DBNull ? "" : (string)datos.lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = datos.lector["IdMarca"] is DBNull ? 0 : (int)datos.lector["IdMarca"];
                    aux.Marca.Descripcion = datos.lector["Marca"] is DBNull ? "" : (string)datos.lector["Marca"];
                    aux.Precio = datos.lector["Precio"] is DBNull ? 0f : Convert.ToSingle(datos.lector["Precio"]);

                    aux.Stock = datos.lector["Stock"] is DBNull ? 0f : Convert.ToSingle(datos.lector["Stock"]);
                    aux.FechaModif = (DateTime)(datos.lector["FechaModif"] is DBNull ? (object)null : (DateTime)datos.lector["FechaModif"]);
                    aux.IdMarca = datos.lector["IdMarca"] is DBNull ? 0 : (int)datos.lector["IdMarca"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgregarArticuloSP(Articulo Nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearStoredProcedure("storedAgregarArticulo");
                datos.setParametros("@Rubro", Nuevo.Rubro);
                datos.setParametros("@Descripcion", Nuevo.Descripcion);
                datos.setParametros("@Marca", Nuevo.Marca.Descripcion);
                datos.setParametros("@Precio", Nuevo.Precio);
                datos.setParametros("@Stock", Nuevo.Stock);
                datos.setParametros("@FechaModif", Nuevo.FechaModif);
                datos.setParametros("@IdMarca", Nuevo.IdMarca);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw new Exception("Error en el método Agregar Artículo: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
    }
}
