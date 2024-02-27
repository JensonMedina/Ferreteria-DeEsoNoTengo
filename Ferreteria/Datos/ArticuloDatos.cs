using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class ArticuloDatos
    {
        //public List<Articulo> listarArticulosSP()
        //{
        //    List<Articulo> lista = new List<Articulo>();
        //    AccesoDatos datos = new AccesoDatos();
        //    try
        //    {
        //        datos.setearStoredProcedure("storedListarArticulos");
        //        datos.EjecutarLectura();

        //        while (datos.lector.Read())
        //        {
        //            Articulo aux = new Articulo();
        //            aux.Id = (int)datos.lector["Id"];
        //            aux.Codigo = (string)datos.lector["Codigo"];
        //            aux.Rubro = datos.lector["Rubro"] is DBNull ? "" : (string)datos.lector["Rubro"];
        //            aux.Descripcion = datos.lector["Descripcion"] is DBNull ? "" : (string)datos.lector["Descripcion"];
        //            aux.Marca = new Marca();
        //            aux.Marca.Id = datos.lector["IdMarca"] is DBNull ? 0 : (int)datos.lector["IdMarca"];
        //            aux.Marca.Descripcion = datos.lector["Marca"] is DBNull ? "" : (string)datos.lector["Marca"];
        //            aux.Precio = datos.lector["Precio"] is DBNull ? 0f : Convert.ToSingle(datos.lector["Precio"]);

        //            aux.Stock = datos.lector["Stock"] is DBNull ? 0f : Convert.ToSingle(datos.lector["Stock"]);
        //            aux.FechaModif = (DateTime)(datos.lector["FechaModif"] is DBNull ? (object)null : (DateTime)datos.lector["FechaModif"]);
        //            aux.IdMarca = datos.lector["IdMarca"] is DBNull ? 0 : (int)datos.lector["IdMarca"];
        //            lista.Add(aux);
        //        }

        //        return lista;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<Articulo> ListarArticulosSP(int? idMarcaFiltro = null)
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

                    // Agregar el filtro por marca si se proporciona el parámetro idMarcaFiltro
                    if (!idMarcaFiltro.HasValue || aux.IdMarca == idMarcaFiltro.Value)
                    {
                        lista.Add(aux);
                    }
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
                datos.setParametros("@Codigo", Nuevo.Codigo);
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
                throw ex;
                //throw new Exception("Error en el método Agregar Artículo: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public void ModificarArticuloSP(Articulo selecionado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearStoredProcedure("storedModificarArticulo");
                datos.setParametros("@Codigo", selecionado.Codigo);
                datos.setParametros("@Rubro", selecionado.Rubro);
                datos.setParametros("@Descripcion", selecionado.Descripcion);
                datos.setParametros("@Marca", selecionado.Marca.Descripcion);
                datos.setParametros("@PrecioVenta", selecionado.Precio);
                datos.setParametros("@stock", selecionado.Stock);
                datos.setParametros("@FechaModif", selecionado.FechaModif);
                datos.setParametros("@IdMarca", selecionado.IdMarca);
                datos.setParametros("@Id", selecionado.Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw;
                //throw new Exception("Error en el método Modificar artículo: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }
        }


        public List<Articulo> FiltrarArticulos(string codigo, string rubro, Marca idMarca)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> listaArticulos = new List<Articulo>();
            try
            {
                string consulta = "select Codigo, Rubro, Descripcion, Marca, PrecioVenta, Stock, id, FechaModif, IdMarca from tbArticulos where ";
                if(codigo != null && rubro == null && idMarca == null)
                {
                    consulta += "Codigo = '" + codigo + "'";
                }
                if(codigo != null && rubro != null && idMarca == null)
                {
                    consulta += "Codigo = '" + codigo +"'" + " and Rubro = '" + rubro +"'";
                }
                if(codigo != null && rubro != null && idMarca != null)
                {
                    consulta += "Codigo = '" + codigo +"'" + " and Rubro = '" + rubro + "'" + " and IdMarca = " + idMarca.Id;
                }
                if(codigo == null && rubro != null && idMarca == null)
                {
                    consulta += "Rubro = '" + rubro +"'";
                }
                if(codigo == null && rubro != null && idMarca != null)
                {
                    consulta += "Rubro = '" + rubro +"'" + " and IdMarca = " + idMarca.Id;
                }
                if(codigo == null && rubro == null && idMarca != null)
                {
                    consulta += "IdMarca = " + idMarca.Id;
                }
                datos.setearConsulta(consulta);
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
                    aux.Precio = datos.lector["PrecioVenta"] is DBNull ? 0f : Convert.ToSingle(datos.lector["PrecioVenta"]);

                    aux.Stock = datos.lector["Stock"] is DBNull ? 0f : Convert.ToSingle(datos.lector["Stock"]);
                    aux.FechaModif = (DateTime)(datos.lector["FechaModif"] is DBNull ? (object)null : (DateTime)datos.lector["FechaModif"]);
                    aux.IdMarca = datos.lector["IdMarca"] is DBNull ? 0 : (int)datos.lector["IdMarca"];
                    listaArticulos.Add(aux);
                }
                return listaArticulos;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void EliminarArticulo(int Id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "delete from tbArticulos where id = @id";
                datos.setearConsulta(consulta);
                datos.setParametros("@id", Id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void ActualizarPreciosArticulos(List<Articulo> articulos)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                foreach (var articulo in articulos)
                {
                    using (SqlCommand comando = new SqlCommand())
                    {
                        comando.CommandText = "UPDATE tbArticulos SET PrecioVenta = @Precio WHERE id = @Id";

                        comando.Parameters.AddWithValue("@Precio", articulo.Precio);
                        comando.Parameters.AddWithValue("@Id", articulo.Id);

                        datos.EjecutarAccionActualizarPrecios(comando);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }




    }
}
