using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace posgrec_
{
    public partial class Articulos : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("tu_cadena_de_conexion_a_PostgreSQL");
        NpgsqlCommand cmd;
        NpgsqlDataReader sqlread;
        string query;

        public Articulos()
        {
            InitializeComponent();
        }

        private void Articulos_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet18.Articulo'
            // Puede moverla o quitarla según sea necesario.
            this.articuloTableAdapter1.Fill(this.papeleriaDataSet18.Articulo);

            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet3.Categoria'
            // Puede moverla o quitarla según sea necesario.
            this.categoriaTableAdapter.Fill(this.papeleriaDataSet3.Categoria);

            TBart.Text = Consecu();
            TBnom.Select();
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            ComboBox1.Visible = false;
            Button1.Visible = false;

            if (!string.IsNullOrEmpty(TBart.Text) && !string.IsNullOrEmpty(TBnom.Text) &&
                !string.IsNullOrEmpty(TBEspe.Text) && !string.IsNullOrEmpty(TBmarca.Text) &&
                !string.IsNullOrEmpty(TBvent.Text) && !string.IsNullOrEmpty(tbcom.Text) &&
                !string.IsNullOrEmpty(tbexis.Text))
            {
                using (conexion)
                {
                    conexion.Open();
                    query = "insert into articulo values(@TBart, @TBnom, @TBEspe, @TBmarca, @tbexis, @tbcom, @TBvent, @Cbcat)";

                    cmd = new NpgsqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@TBart", TBart.Text);
                    cmd.Parameters.AddWithValue("@TBnom", TBnom.Text);
                    cmd.Parameters.AddWithValue("@TBEspe", TBEspe.Text);
                    cmd.Parameters.AddWithValue("@TBmarca", TBmarca.Text);
                    cmd.Parameters.AddWithValue("@tbexis", tbexis.Text);
                    cmd.Parameters.AddWithValue("@tbcom", tbcom.Text);
                    cmd.Parameters.AddWithValue("@TBvent", TBvent.Text);
                    cmd.Parameters.AddWithValue("@Cbcat", Cbcat.SelectedValue);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Artículo registrado", "Información", MessageBoxButtons.OK);
                    conexion.Close();
                    Limpiar();
                }
            }
        }

        

        private string Consecu()
        {
            string a = "";
            int nex;

            using (conexion)
            {
                conexion.Open();
                query = "select Idarticulo from articulo";
                cmd = new NpgsqlCommand(query, conexion);

                sqlread = cmd.ExecuteReader();

                while (sqlread.Read())
                {
                    a = sqlread["Idarticulo"].ToString();
                }

                nex = Convert.ToInt32(a);
                nex = nex + 1;
                a = nex.ToString();
                sqlread.Close();
            }

            return a;
        }
        private void Btnmenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu_principal.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ComboBox1.Visible = false;
            int bandera = 0;

            using (NpgsqlConnection conexion = new NpgsqlConnection("tu_cadena_de_conexion_a_PostgreSQL"))
            {
                conexion.Open();
                query = "select * from articulo where idarticulo = @idarticulo";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@idarticulo", ComboBox1.SelectedValue);
                    cmd.ExecuteNonQuery();
                    using (NpgsqlDataReader sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            bandera = 1;
                            TBnom.Text = sqlread["Nombre"].ToString();
                            TBEspe.Text = sqlread["Especificaciones"].ToString();
                            TBmarca.Text = sqlread["Marca"].ToString();
                            tbcom.Text = sqlread["Precio_compra"].ToString();
                            TBvent.Text = sqlread["precio_venta"].ToString();
                            tbexis.Text = sqlread["existencia"].ToString();
                            Cbcat.SelectedValue = sqlread["fk_idcategoria"];
                        }
                    }
                }
            }

            if (bandera != 1)
            {
                MessageBox.Show("Registro inexistente", "Articulos", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
