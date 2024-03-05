using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Datos
{
    public class AccesoDatos
    {
        private SqlConnection Conexion = new SqlConnection();
        private SqlCommand Comando = new SqlCommand();
        private SqlDataReader Lector;
        public SqlDataReader lector
        {
            get { return Lector; }
        }
        public AccesoDatos()
        {
            //Conexion = new SqlConnection("server=.\\SQLEXPRESS; database=Ferreteria; integrated security=true");
            string cadenaConexion = "Data Source=LAPTOP-E58L5U4B; Initial Catalog=Ferreteria; User ID=usuario; Password=1234";
            Conexion = new SqlConnection(cadenaConexion);
            Comando = new SqlCommand();
        }
        public void setearConsulta(string Consulta)
        {
            Comando.CommandType = System.Data.CommandType.Text;
            Comando.CommandText = Consulta;
        }
        public void setearStoredProcedure(string storedProcedure)
        {
            Comando.CommandType = System.Data.CommandType.StoredProcedure;
            Comando.CommandText = storedProcedure;
        }
        //public void EjecutarLectura()
        //{
        //    Comando.Connection = Conexion;
        //    try
        //    {
        //        Conexion.Open();
        //        Lector = Comando.ExecuteReader();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public void EjecutarAccion()
        {
            Comando.Connection = Conexion;
            try
            {
                Conexion.Open();
                Comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarLectura()
        {
            Comando.Connection = Conexion;
            try
            {
                if (Conexion.State == ConnectionState.Closed)
                {
                    Conexion.Open();
                }
                Lector = Comando.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EjecutarAccionActualizarPrecios(SqlCommand comando)
        {
            comando.Connection = Conexion;
            try
            {
                if (Conexion.State == ConnectionState.Closed)
                {
                    Conexion.Open();
                }
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public int EjecutarAccionScalar()
        {
            Comando.Connection = Conexion;
            try
            {
                Conexion.Open();
                return int.Parse(Comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void setParametros(string nombre, object valor)
        {
            Comando.Parameters.AddWithValue(nombre, valor);
        }
        public void CerrarConexion()
        {
            if (Lector != null)
                Lector.Close();
            Conexion.Close();
        }
    }
}
