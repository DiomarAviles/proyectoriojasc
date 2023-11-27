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

public partial class AbasteceForm : Form
    {
        NpgsqlConnection conexion = new NpgsqlConnection("Server=localhost;Port=5432;User Id=isaias;Password=1234;Database=papeleria");
        string query;

        public AbasteceForm()
        {
            InitializeComponent();
        }

        private void abastece_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet9.Articulo' Puede moverla o quitarla según sea necesario.
            this.ArticuloTableAdapter2.Fill(this.PapeleriaDataSet9.Articulo);
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet6.Articulo' Puede moverla o quitarla según sea necesario.
            this.ArticuloTableAdapter1.Fill(this.PapeleriaDataSet6.Articulo);
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string query = "SELECT a.Nombre, Fk_IdArticulo, p.nombres, ab.Fk_IdProveedor as 'Nombre Proveedor', ab.Cantidad, ab.Total, ab.Fecha FROM Abastece as ab, articulo as a, proveedor as p where a.IdArticulo = Fk_IdArticulo and p.idProveedor = fk_idproveedor";

            using (NpgsqlDataAdapter sqladap = new NpgsqlDataAdapter(query, conexion))
            {
                ds.Tables.Add("tabla");
                sqladap.Fill(ds.Tables["tabla"]);
                DataGridView1.DataSource = ds.Tables["tabla"];
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            AgregarCompraForm agregarcompra = new AgregarCompraForm();
            agregarcompra.Show();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string query = "SELECT a.Nombre, Fk_IdArticulo, p.nombres, ab.Fk_IdProveedor, ab.Cantidad, ab.Total, ab.Fecha " +
            "FROM Abastece as ab, articulo as a, proveedor as p " +
                           "where a.idarticulo = Fk_IdArticulo and a.idarticulo = " + ComboBox1.SelectedValue;

            using (NpgsqlDataAdapter sqladap = new NpgsqlDataAdapter(query, conexion))
            {
                ds.Tables.Add("tabla");
                sqladap.Fill(ds.Tables["tabla"]);
                DataGridView1.DataSource = ds.Tables["tabla"];
            }
        }
    }

}
}
