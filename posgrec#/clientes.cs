using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace posgrec_
{
    public partial class ClientesForm : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("Server=localhost;Port=5432;User Id=isaias;Password=1234;Database=papeleria");
        NpgsqlCommand cmd;
        NpgsqlDataReader sqlread;
        string query;

        public ClientesForm()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet17.Cliente' Puede moverla o quitarla según sea necesario.
            this.ClienteTableAdapter2.Fill(this.PapeleriaDataSet17.Cliente);

            Button1.Hide();
        }

        private void limpear()
        {
            Button1.Hide();
            Tbcliente.Text = "";
            tbnom.Text = "";
            tbapp.Text = "";
            tbapm.Text = "";
            tbtel.Text = "";
            tbcorr.Text = "";
            tbrfc.Text = "";
            tbdir.Text = "";
            Tbciu.Text = "";
            tbest.Text = "";
            tbcp.Text = "";
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            Tbcliente.Text = "";
            Button1.Show();
            ComboBox1.Visible = true;
        }

        private string consecu()
        {
            string a = "";
            int nex;

            using (conexion)
            {
                conexion.Open();
                query = "select Idcliente from cliente";
                using (cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            a = sqlread["Idcliente"].ToString();
                        }
                    }
                }
                nex = Convert.ToInt32(a);
                nex = nex + 1;
                a = nex.ToString();
            }

            return a;
        }

        private void agregar_Click(object sender, EventArgs e)
        {
            Button1.Hide();
            ComboBox1.Visible = false;

            if (!string.IsNullOrEmpty(Tbcliente.Text) && !string.IsNullOrEmpty(tbnom.Text) && !string.IsNullOrEmpty(tbapp.Text) && !string.IsNullOrEmpty(tbapm.Text) && !string.IsNullOrEmpty(tbtel.Text) && !string.IsNullOrEmpty(tbcorr.Text) && !string.IsNullOrEmpty(tbrfc.Text) && !string.IsNullOrEmpty(tbdir.Text) && !string.IsNullOrEmpty(Tbciu.Text) && !string.IsNullOrEmpty(tbest.Text) && !string.IsNullOrEmpty(tbcp.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    using (cmd = conexion.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "registrocliente";
                        cmd.Parameters.AddWithValue("@Idcliente", Convert.ToInt16(Tbcliente.Text));
                        cmd.Parameters.AddWithValue("@Nombres", tbnom.Text);
                        cmd.Parameters.AddWithValue("@apellidop", tbapp.Text);
                        cmd.Parameters.AddWithValue("@apellidom", tbapm.Text);
                        cmd.Parameters.AddWithValue("@telefono_c", tbtel.Text);
                        cmd.Parameters.AddWithValue("@correo_c", tbcorr.Text);
                        cmd.Parameters.AddWithValue("@RFC_c", tbrfc.Text);
                        cmd.Parameters.AddWithValue("@Direccion_c", tbdir.Text);
                        cmd.Parameters.AddWithValue("@ciudad", Tbciu.Text);
                        cmd.Parameters.AddWithValue("@estado", tbest.Text);
                        cmd.Parameters.AddWithValue("@CP", tbcp.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cliente registrado", MessageBoxButtons.OK);
                    conexion.Close();
                    limpear();
                    Tbcliente.Text = consecu();
                }
            }
            else
            {
                MessageBox.Show("Llene la información necesaria", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            Button1.Hide();
            ComboBox1.Visible = false;

            if (!string.IsNullOrEmpty(Tbcliente.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    query = "delete from cliente where idcliente = @Tbcliente";
                    using (cmd = new NpgsqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Tbcliente", Tbcliente.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cliente eliminado", "Información", MessageBoxButtons.OK);
                    conexion.Close();
                    limpear();
                }
            }
        }

        private void actu_Click(object sender, EventArgs e)
        {
            Button1.Hide();
            ComboBox1.Visible = false;

            if (!string.IsNullOrEmpty(Tbcliente.Text) && !string.IsNullOrEmpty(tbnom.Text) && !string.IsNullOrEmpty(tbapp.Text) && !string.IsNullOrEmpty(tbapm.Text) && !string.IsNullOrEmpty(tbtel.Text) && !string.IsNullOrEmpty(tbcorr.Text) && !string.IsNullOrEmpty(tbrfc.Text) && !string.IsNullOrEmpty(tbdir.Text) && !string.IsNullOrEmpty(Tbciu.Text) && !string.IsNullOrEmpty(tbest.Text) && !string.IsNullOrEmpty(tbcp.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    using (cmd = conexion.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "actualizarcliente";
                        cmd.Parameters.Add("@Idcliente", SqlDbType.SmallInt).Value = Tbcliente.Text;
                        cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = tbnom.Text;
                        cmd.Parameters.Add("@apellidop", SqlDbType.VarChar).Value = tbapp.Text;
                        cmd.Parameters.Add("@apellidom", SqlDbType.VarChar).Value = tbapm.Text;
                        cmd.Parameters.Add("@telefono_c", SqlDbType.VarChar).Value = tbtel.Text;
                        cmd.Parameters.Add("@correo_c", SqlDbType.VarChar).Value = tbcorr.Text;
                        cmd.Parameters.Add("@RFC_c", SqlDbType.VarChar).Value = tbrfc.Text;
                        cmd.Parameters.Add("@Direccion_c", SqlDbType.VarChar).Value = tbdir.Text;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = Tbciu.Text;
                        cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value = tbest.Text;
                        cmd.Parameters.Add("@CP", SqlDbType.VarChar).Value = tbcp.Text;
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Cliente actualizado", MessageBoxButtons.OK);
                    conexion.Close();
                    limpear();
                    Tbcliente.Text = consecu();
                }
            }
            else
            {
                MessageBox.Show("Llene la información necesaria");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ComboBox1.Visible = false;

            int bandera = 0;
            using (conexion)
            {
                conexion.Open();
                query = "select * from Cliente where idcliente = @ComboBox1Value";
                using (cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@ComboBox1Value", Convert.ToInt32(ComboBox1.SelectedValue));
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            bandera = 1;
                            Tbcliente.Text = sqlread["idcliente"].ToString();
                            tbnom.Text = sqlread["Nombres"].ToString();
                            tbapp.Text = sqlread["apellidop"].ToString();
                            tbapm.Text = sqlread["apellidom"].ToString();
                            tbtel.Text = sqlread["telefono_c"].ToString();
                            tbcorr.Text = sqlread["Correo_c"].ToString();
                            tbrfc.Text = sqlread["RFC_c"].ToString();
                            tbdir.Text = sqlread["direccion_c"].ToString();
                            Tbciu.Text = sqlread["ciudad"].ToString();
                            tbest.Text = sqlread["estado"].ToString();
                            tbcp.Text = sqlread["CP"].ToString();
                        }
                    }

                    if (bandera != 1)
                    {
                        MessageBox.Show("Registro inexistente", "Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    conexion.Close();
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            limpear();
            consecu();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu_principal.Show();
        }

    }
