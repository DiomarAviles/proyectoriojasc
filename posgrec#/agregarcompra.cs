using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Npgsql;

namespace posgrec_
{ 

    public partial class AgregarCompraForm : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("Server=localhost;Port=5432;User Id=isaias;Password=1234;Database=papeleria");
        NpgsqlCommand cmd;

        public AgregarCompraForm()
        {
            InitializeComponent();
        }

        private void agregarcompra_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet8.Proveedor' Puede moverla o quitarla según sea necesario.
            this.ProveedorTableAdapter.Fill(this.PapeleriaDataSet8.Proveedor);
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet7.Articulo' Puede moverla o quitarla según sea necesario.
            this.ArticuloTableAdapter.Fill(this.PapeleriaDataSet7.Articulo);
            TextBox2.Text = "0";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int cantidad = int.Parse(TextBox2.Text);
            int total = int.Parse(TextBox3.Text);

            using (conexion)
            {
                conexion.Open();
                using (cmd = conexion.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "registroabastece";
                    cmd.Parameters.AddWithValue("@fk_idArticulo", ComboBox1.SelectedValue);
                    cmd.Parameters.AddWithValue("@fk_idProveedor", ComboBox2.SelectedValue);
                    cmd.Parameters.AddWithValue("@cantidad", cantidad);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@fecha", DateTimePicker1.Value);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Compra registrada", "Información", MessageBoxButtons.OK);
                conexion.Close();
            }

            this.Close();
            AbasteceForm abasteceForm = new AbasteceForm();
            abasteceForm.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
            AbasteceForm abasteceForm = new AbasteceForm();
            abasteceForm.Show();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            int valor = int.Parse(TextBox1.Text);
            int cantidad = int.Parse(TextBox2.Text);
            int total = valor * cantidad;

            TextBox3.Text = total.ToString();
        }
    }

}
