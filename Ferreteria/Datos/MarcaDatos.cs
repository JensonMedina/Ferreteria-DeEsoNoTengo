﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class MarcaDatos
    {
        public List<Marca> listarMarcasSP()
        {
            try
            {
                List<Marca> listaMembresias = new List<Marca>();
                AccesoDatos datos = new AccesoDatos();
                datos.setearStoredProcedure("storedListarMarcas");
                datos.EjecutarLectura();
                while (datos.lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.lector["Id"];
                    aux.Descripcion = datos.lector["Descripcion"] is DBNull ? null : (string)datos.lector["Descripcion"];
                    if(aux.Descripcion != null)
                        listaMembresias.Add(aux);
                }
                return listaMembresias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el metodo listar marcas: " + ex.Message);
            }
        }
        public void AgregarMarcasSP(Marca Nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "insert into tbmarcas values (@Descripcion)";
                datos.setearConsulta(consulta);
                datos.setParametros("@Descripcion", Nuevo.Descripcion);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en el método Agregar Marca: " + ex.Message);
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
    }
}
