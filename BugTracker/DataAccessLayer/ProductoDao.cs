

using System;
using System.Collections.Generic;
using BugTracker.Entities;
using System.Data;

namespace BugTracker.DataAccessLayer
{
    public class ProductoDao
    {
        public IList<Producto> GetAll()
        {
            List<Producto> listadoBugs = new List<Producto>();

            var strSql = "SELECT id_producto, nombre FROM Productos p WHERE p.borrado = 'False'";

            var resultadoConsulta = DataManager.GetInstance().ConsultaSQL(strSql);

            foreach (DataRow row in resultadoConsulta.Rows)
            {
                listadoBugs.Add(ObjectMapping(row));
            }

            return listadoBugs;
        }

        private Producto ObjectMapping(DataRow row)
        {
            Producto oProducto = new Producto
            {
                IdProducto = Convert.ToInt32(row["id_producto"].ToString()),
                Nombre = row["nombre"].ToString()
            };

            return oProducto;
        }
        internal bool Create(Producto oProducto)
        {
            string str_sql = "     INSERT INTO Productos (nombre, borrado)" +
                             "     VALUES (@nombre,'False')";

            var parametros = new Dictionary<string, object>();
            parametros.Add("nombre", oProducto.Nombre);
            

            // Si una fila es afectada por la inserción retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }
        internal bool Update(Producto oProducto)
        {
            string str_sql = "  UPDATE Productos" +
                            "     SET nombre = @nombre" +
                            " WHERE id_producto = @id_producto";

            var parametros = new Dictionary<string, object>();
            parametros.Add("id_producto", oProducto.IdProducto);
            parametros.Add("nombre", oProducto.Nombre);
           
            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }
        internal bool Delete(Producto oProducto)
        {
            string str_sql = "  UPDATE Productos" +
                            "     SET borrado = 'True'" +
                            "   WHERE id_producto = @id_producto";

            var parametros = new Dictionary<string, object>();
            parametros.Add("id_Producto", oProducto.IdProducto);

            // Si una fila es afectada por la actualización retorna TRUE. Caso contrario FALSE
            return (DataManager.GetInstance().EjecutarSQL(str_sql, parametros) == 1);
        }
        public IList<Producto> GetByFilters(Dictionary<string, object> parametros)
        {
            List<Producto> lst = new List<Producto>();
            String strSql = string.Concat("SELECT p.id_producto, ",
                                              "        p.nombre ",
                                              "   FROM Productos p",
                                              "  WHERE p.borrado = 'False'");

           


            if (parametros.ContainsKey("nombre"))
                strSql += " AND (p.nombre LIKE '%' + @nombre + '%') ";

            var resultado = DataManager.GetInstance().ConsultaSQL(strSql, parametros);

            foreach (DataRow row in resultado.Rows)
                lst.Add(ObjectMapping(row));

            return lst;
        }
        public Producto GetProducto(string nombreProducto)
        {
            //Construimos la consulta sql para buscar el usuario en la base de datos.
            String strSql = string.Concat(" SELECT p.id_producto, ",
                                          "        p.nombre ",
                                          "   FROM Productos p",
                                          "  WHERE p.nombre = @nombre AND p.borrado = 'False'");

            var parametros = new Dictionary<string, object>();
            parametros.Add("nombre", nombreProducto);
            //Usando el método GetDBHelper obtenemos la instancia unica de DBHelper (Patrón Singleton) y ejecutamos el método ConsultaSQL()
            var resultado = DataManager.GetInstance().ConsultaSQL(strSql, parametros);

            // Validamos que el resultado tenga al menos una fila.
            if (resultado.Rows.Count > 0)
            {
                return ObjectMapping(resultado.Rows[0]);
            }

            return null;
        }
    }

}