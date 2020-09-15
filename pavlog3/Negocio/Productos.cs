using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using pavlog3.datos;

namespace pavlog3.Negocio

{
    public class Productos
    {
        private ProductosAD objetoCD = new ProductosAD();

        public DataTable MostrarProd()
        {

            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
        public void InsertarPRod(string nombre)
        {

            objetoCD.Insertar(nombre);
        }

        public void EditarProd(string nombre)
        {
            objetoCD.Editar(nombre);
        }

        public void EliminarPRod(string id)
        {

            objetoCD.Eliminar(Convert.ToInt32(id));
        }

    }
}
