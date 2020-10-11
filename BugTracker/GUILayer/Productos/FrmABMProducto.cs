using BugTracker.BusinessLayer;
using BugTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker.GUILayer.Productos
{
    public partial class frmABMProducto : Form
    {

        private FormMode formMode = FormMode.nuevo;

        private readonly ProductoService oProductoService;
        private Producto oProductoSelected;

        public frmABMProducto()
        {
            InitializeComponent();
            oProductoService = new ProductoService();
            
        }

        public enum FormMode
        {
            nuevo,
            actualizar,
            eliminar
        }


        private void frmABMProducto_Load(System.Object sender, System.EventArgs e)
        {
            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        this.Text = "Nuevo Usuario";
                        txtNombre.Enabled = true;
                        break;
                    }

                case FormMode.actualizar:
                    {
                        this.Text = "Actualizar Usuario";
                        // Recuperar usuario seleccionado en la grilla 
                        MostrarDatos();
                        txtNombre.Enabled = true;
                        break;
                    }

                case FormMode.eliminar:
                    {
                        MostrarDatos();
                        this.Text = "Habilitar/Deshabilitar Usuario";
                        txtNombre.Enabled = false;
                        break;
                    }
            }
        }

        public void InicializarFormulario(FormMode op, Producto ProductoSelected)
        {
            formMode = op;
            oProductoSelected = ProductoSelected;
        }

        private void MostrarDatos()
        {
            if (oProductoSelected != null)
            {
                txtNombre.Text = oProductoSelected.Nombre;
            }
        }

        private void btnAceptar_Click(System.Object sender, System.EventArgs e)
        {
            switch (formMode)
            {
                case FormMode.nuevo:
                    {
                        if (ExisteProducto() == false)
                                                 
                        {
                            if (ValidarCampos())
                            {
                                var oProducto = new Producto();
                                oProducto.Nombre = txtNombre.Text;

                                if (oProductoService.CrearProducto(oProducto))
                                {
                                    MessageBox.Show("Usuario insertado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    this.Close();
                                }
                            }
                            
                        }
                        else
                            MessageBox.Show("Nombre de usuario encontrado!. Ingrese un nombre diferente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    }

                case FormMode.actualizar:
                    {
                        if (ValidarCampos())
                        {
                            oProductoSelected.Nombre = txtNombre.Text;

                            if (oProductoService.ActualizarProducto(oProductoSelected))
                            {
                                MessageBox.Show("Usuario actualizado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Dispose();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }

                case FormMode.eliminar:
                    {
                        if (MessageBox.Show("Seguro que desea habilitar/deshabilitar el usuario seleccionado?", "Aviso", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            if (oProductoService.EliminarProducto(oProductoSelected))
                            {
                                MessageBox.Show("Usuario Habilitado/Deshabilitado!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                                MessageBox.Show("Error al actualizar el usuario!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
            }
        }

        private bool ValidarCampos()
        {
            // campos obligatorios
            if (txtNombre.Text == string.Empty)
            {
                txtNombre.BackColor = Color.Red;
                txtNombre.Focus();
                return false;
            }
            else
                txtNombre.BackColor = Color.White;

           

            return true;
        }

        private bool ExisteProducto()
        {
            return oProductoService.ObtenerProducto(txtNombre.Text) != null;
        }


        private void LlenarCombo(ComboBox cbo, Object source, string display, String value)
        {
            cbo.DataSource = source;
            cbo.DisplayMember = display;
            cbo.ValueMember = value;
            cbo.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir_Click(System.Object sender, System.EventArgs e)
        {
            this.Close();
        }

        
    }
}
