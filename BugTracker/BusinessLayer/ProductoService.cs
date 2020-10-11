using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Entities;
using BugTracker.DataAccessLayer;

namespace BugTracker.BusinessLayer
{
    public class ProductoService
    {
        private ProductoDao oProductoDao;
        public ProductoService()
        {
            oProductoDao = new ProductoDao();
        }
        public IList<Producto> ObtenerTodos()
        {
            return oProductoDao.GetAll();
        }
        internal bool CrearProducto(Producto oUsuario)
        {
            return oProductoDao.Create(oUsuario);
        }

        internal bool ActualizarProducto(Producto oProductoSelected)
        {
            return oProductoDao.Update(oProductoSelected);
        }

        internal bool EliminarProducto(Producto oProductoSelected)
        {
            return oProductoDao.Delete(oProductoSelected);
        }

        internal object ObtenerProducto(string producto)
        {

            return oProductoDao.GetProducto(producto);
        }

        internal IList<Producto> ConsultarConFiltro(Dictionary<string, object> filtros)
        {
            return oProductoDao.GetByFilters(filtros);
        }
    }

}

