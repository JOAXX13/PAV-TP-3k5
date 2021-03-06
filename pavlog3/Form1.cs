﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace pavlog3
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            Environment.Exit(0);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            
            if ((txtUsuario.Text == ""))
            {
                MessageBox.Show("Se debe ingresar un usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

      
            if ((txtPassword.Text == ""))
            {
                MessageBox.Show("Se debe ingresar una contraseña.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

             
            if (ValidarCredenciales(txtUsuario.Text, txtPassword.Text))
            {
                
                MessageBox.Show("Usted a ingresado al sistema.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                
                txtPassword.Text = "";
               
                txtPassword.Focus();
               
                MessageBox.Show("Debe ingresar usuario y/o contraseña válidos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool ValidarCredenciales(string pUsuario, string pPassword)
        {
            
            bool usuarioValido = false;

            

            
            SqlConnection conexion = new SqlConnection();

            
            conexion.ConnectionString = "Data Source=DESKTOP-PAH0KJH\\SQLEXPRESS;Initial Catalog=db_bugs_extendidotpi;Integrated Security=True;";

            
            try
            {
               
                conexion.Open();

                String consultaSql = string.Concat(" SELECT * ",
                                                   "   FROM Usuarios ",
                                                   "  WHERE usuario =  '", pUsuario, "'");

                
                SqlCommand command = new SqlCommand(consultaSql, conexion);

                
                SqlDataReader reader = command.ExecuteReader();

                
                if (reader.Read())
                {
                    
                    if (reader["password"].ToString() == pPassword)
                    {
                        usuarioValido = true;
                    }
                }

            }
            catch (SqlException ex)
            {
               
                MessageBox.Show(string.Concat("Error de base de datos: ", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                
                if (conexion.State == ConnectionState.Open)
                {
                    
                    conexion.Close();
                }
            }

            
            return usuarioValido;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            this.CenterToParent();
        }
    }

}