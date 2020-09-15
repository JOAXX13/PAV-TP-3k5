using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pavlog3.datos
{
    public class Conexion
    {
        private SqlConnection Conexion = new SqlConnection("Data Source = DESKTOP - PAH0KJH\\SQLEXPRESS; Initial Catalog = db_bugs_extendidotpi; Integrated Security = True;";

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }
    }
}