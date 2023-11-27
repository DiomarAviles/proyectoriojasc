using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace posgrec_
{
    public partial class CateForm : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("Server=localhost;Port=5432;User Id=isaias;Password=1234;Database=papeleria");
        NpgsqlCommand cmd;
        NpgsqlDataReader sqlread;
        string query;

        public CateForm()
        {
            InitializeComponent();
        }

        private void buscar_Click(object sender, EventArgs e)
        {
            int bandera = 0;
            using (conexion)
            {
                conexion.Open();
                query = "select * from categoria where idctg = @idctg";
                using (cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idctg", Tbcat.Text);
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            bandera = 1;
                            tbnom.Text = sqlread["Nombre_ctg"].ToString();
                            Tbdes.Text = sqlread["descripcion"].ToString();
                        }
                    }
                }
            }

            if (bandera != 1)
            {
                MessageBox.Show("Registro inexistente", "Categoría", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void limpear()
        {
            Tbcat.Text = "";
            tbnom.Text = "";
            Tbdes.Text = "";
        }

        private string consecu()
        {
            string a = "";
            int nex;

            using (conexion)
            {
                conexion.Open();
                query = "select Idctg from categoria";
                using (cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            a = sqlread["Idctg"].ToString();
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
            if (!string.IsNullOrEmpty(Tbcat.Text) && !string.IsNullOrEmpty(tbnom.Text) && !string.IsNullOrEmpty(Tbdes.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    query = "insert into categoria values(@Tbcat, @tbnom, @Tbdes)";
                    using (cmd = new NpgsqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Tbcat", Tbcat.Text);
                        cmd.Parameters.AddWithValue("@tbnom", tbnom.Text);
                        cmd.Parameters.AddWithValue("@Tbdes", Tbdes.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Categoría registrada", "Información", MessageBoxButtons.OK);
                    conexion.Close();
                    limpear();
                    Tbcat.Text = consecu();
                }
            }
            else
            {
                MessageBox.Show("Llene la información necesaria", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void eliminar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Tbcat.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    query = "delete from categoria where idctg = @Tbcat";
                    using (cmd = new NpgsqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Tbcat", Tbcat.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Categoría eliminada", "Información", MessageBoxButtons.OK);
                    conexion.Close();
                    limpear();
                }
            }
        }

        private void actualizar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Tbcat.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    query = "update categoria set Nombre_ctg = @tbnom, descripcion = @Tbdes where idctg = @Tbcat";
                    using (cmd = new NpgsqlCommand(query, conexion))
                    {
                        cmd.Parameters.AddWithValue("@Tbcat", Tbcat.Text);
                        cmd.Parameters.AddWithValue("@tbnom", tbnom.Text);
                        cmd.Parameters.AddWithValue("@Tbdes", Tbdes.Text);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Categoría actualizada", "Información", MessageBoxButtons.OK);
                    conexion.Close();
                }
            }
        }

        private void cate_Load(object sender, EventArgs e)
        {
            Tbcat.Text = consecu();
        }

        private void Bmenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu_principal.Close();
        }
    }
}
