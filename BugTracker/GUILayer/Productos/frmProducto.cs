using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using BugTracker.BusinessLayer;
using BugTracker.Entities;

namespace BugTracker.GUILayer.Productos
{
    public partial class frmProducto : Form
    {

        private ProductoService oProductoService;

        public frmProducto()
        {
            InitializeComponent();
            InitializeDataGridView();
            oProductoService = new ProductoService();
            

        }

        private void frmUsuarios_Load(object sender, EventArgs e)
        {
          
            this.CenterToParent();
        }

       

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmABMProducto formulario = new frmABMProducto();
            formulario.ShowDialog();
            
        }

        private void chkTodos_CheckedChanged(object sender, EventArgs e)
        {
            {
                if (chkTodos.Checked)
                {
                    txtNombre.Enabled = false;
                  
                }
                else
                {
                    txtNombre.Enabled = true;
                   
                }
            }
        }

        private void btnSalir_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnConsultar_Click(System.Object sender, System.EventArgs e)
        {
            var filters = new Dictionary<string, object>();

            if (!chkTodos.Checked)
            {
                // Validar si el combo 'Perfiles' esta seleccionado.
                if (txtNombre.Text != string.Empty)
                {
                    // Si el textBox tiene un texto no vacìo entonces recuperamos el valor del texto
                    // Si el cbo tiene un texto no vacìo entonces recuperamos el valor de la propiedad ValueMember
                    filters.Add("nombre", txtNombre.Text);




                    if (filters.Count > 0)
                        dgvProductos.DataSource = oProductoService.ConsultarConFiltro(filters);
                    

                }
                else
                    MessageBox.Show("Debe ingresar al menos un criterio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // Validar si el textBox 'Nombre' esta vacio.

            }
            else
                dgvProductos.DataSource = oProductoService.ObtenerTodos();
        }

        private void btnEditar_Click(System.Object sender, System.EventArgs e)
        {
            frmABMProducto formulario = new frmABMProducto();
            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            formulario.InicializarFormulario(frmABMProducto.FormMode.actualizar, producto);
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void dgvProductos_CellClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            btnEditar.Enabled = true;
            btnQuitar.Enabled = true;
        }

        private void btnQuitar_Click(System.Object sender, System.EventArgs e)
        {
            frmABMProducto formulario = new frmABMProducto();
            var producto = (Producto)dgvProductos.CurrentRow.DataBoundItem;
            formulario.InicializarFormulario(frmABMProducto.FormMode.eliminar, producto);
            formulario.ShowDialog();
            btnConsultar_Click(sender, e);
        }

        private void InitializeDataGridView()
        {
            // Cree un DataGridView no vinculado declarando un recuento de columnas.
            dgvProductos.ColumnCount = 1;
            dgvProductos.ColumnHeadersVisible = true;

            // Configuramos la AutoGenerateColumns en false para que no se autogeneren las columnas
            dgvProductos.AutoGenerateColumns = false;

            // Cambia el estilo de la cabecera de la grilla.
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();

            columnHeaderStyle.BackColor = Color.Black;
            columnHeaderStyle.Font = new Font("Verdana", 8, FontStyle.Bold);
            dgvProductos.ColumnHeadersDefaultCellStyle = columnHeaderStyle;

            // Definimos el nombre de la columnas y el DataPropertyName que se asocia a DataSource
            dgvProductos.Columns[0].Name = "Producto";
            dgvProductos.Columns[0].DataPropertyName = "Nombre";
            // Definimos el ancho de la columna.
            dgvProductos.Columns[0].Width = 200;

            // Cambia el tamaño de la altura de los encabezados de columna.
            dgvProductos.AutoResizeColumnHeadersHeight();

            // Cambia el tamaño de todas las alturas de fila para ajustar el contenido de todas las celdas que no sean de encabezado.
            dgvProductos.AutoResizeRows(
                DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
        }
    }
}
