using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posgrec_
{
    public partial class ventas : Form
    {
        public ventas()
        {
            InitializeComponent();
        }

        public class Ventas
        {
            private void PBagregar_Click(object sender, EventArgs e)
            {
                int nu;
                string a;
                if (txtventa.Text != "" && txtcantidad.Text != "" && txtfinal.Text != "" && txtuni.Text != "" && CBcliente.Text != "" && CBEmpleado.Text != "")
                {
                    conexion.Open();
                    if (conexion.State == ConnectionState.Open)
                    {
                        int venta, ca, uni, fin;
                        venta = int.Parse(txtventa.Text);
                        ca = int.Parse(txtcantidad.Text);
                        uni = int.Parse(txtuni.Text);
                        fin = int.Parse(txtfinal.Text);

                        cmd = conexion.CreateCommand();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "registrocompra";
                        cmd.Parameters.Add("@IdCompra", SqlDbType.SmallInt).Value = venta;
                        cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DFC.Text;
                        cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = ca;
                        cmd.Parameters.Add("@Precio_Unitario", SqlDbType.Int).Value = uni;
                        cmd.Parameters.Add("@Precio_Final", SqlDbType.Int).Value = fin;
                        cmd.Parameters.Add("@Fk_IdEmpleado", SqlDbType.SmallInt).Value = CBEmpleado.SelectedValue;
                        cmd.ExecuteNonQuery();

                        cmd2 = conexion.CreateCommand();
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.CommandText = "registroseincluye";
                        cmd2.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = txtventa.Text;
                        cmd2.Parameters.Add("@Fk_IdArticulo", SqlDbType.SmallInt).Value = CBArticulo.SelectedValue;
                        cmd2.Parameters.Add("@Fk_IdServicio", SqlDbType.SmallInt).Value = CBServicio.SelectedValue;
                        cmd2.ExecuteNonQuery();

                        cmd3 = conexion.CreateCommand();
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.CommandText = "registrorealiza";
                        cmd3.Parameters.Add("@Fk_Idcliente", SqlDbType.SmallInt).Value = CBcliente.SelectedValue;
                        cmd3.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = txtventa.Text;
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("Venta Registrada");
                        Menu_principal.Show();
                        conexion.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                    limpiar();
                    txtventa.Text = consecu();
                }
            }

            private void Ventas_Load(object sender, EventArgs e)
            {
                this.ServicioTableAdapter.Fill(this.PapeleriaDataSet1.Servicio);
                this.EmpleadoTableAdapter.Fill(this.PapeleriaDataSet1.Empleado);
                this.ClienteTableAdapter.Fill(this.PapeleriaDataSet1.Cliente);
                this.ArticuloTableAdapter.Fill(this.PapeleriaDataSet.Articulo);
                txtventa.Text = consecu();
            }

            private void limpiar()
            {
                txtventa.Text = "";
                CBcliente.Text = "";
                CBServicio.Text = "";
                txtpre.Text = "0";
                CBArticulo.Text = "";
                txtuni.Text = "0";
                txtcantidad.Text = "0";
                txtfinal.Text = "0";
                CBEmpleado.Text = "";
            }

            private string consecu()
            {
                string a;
                int nex;
                conexion.Open();
                query = "select IdCompra from Compra";
                cmd = new SqlClient.SqlCommand(query, conexion);
                cmd.ExecuteNonQuery();
                sqlread = cmd.ExecuteReader();
                while (sqlread.Read())
                {
                    a = sqlread("IdCompra").ToString();
                }
                nex = int.Parse(a);
                nex = nex + 1;
                a = nex.ToString();
                sqlread.Close();
                cmd.Dispose();
                conexion.Close();
                return a;
            }
        }
        private void txtcantidad_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (txtcantidad.Text == "")
            {
                txtcantidad.Text = "0";
            }
            a = (Convert.ToDouble(txtcantidad.Text) * Convert.ToDouble(txtuni.Text)) + Convert.ToDouble(txtpre.Text);
            txtfinal.Text = a.ToString();
        }

        private void PBeliminar_Click(object sender, EventArgs e)
        {
            if (txtventa.Text != "")
            {
                conexion.Open();
                if (conexion.State == ConnectionState.Open)
                {
                    int venta = Convert.ToInt32(txtventa.Text);

                    cmd2 = conexion.CreateCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "eliminarseincluye";
                    cmd2.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = Convert.ToInt32(txtventa.Text);
                    cmd2.ExecuteNonQuery();

                    cmd3 = conexion.CreateCommand();
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = "eliminarrealiza";
                    cmd3.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = Convert.ToInt32(txtventa.Text);
                    cmd3.ExecuteNonQuery();

                    cmd = conexion.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "eliminarcompra";
                    cmd.Parameters.Add("@IdCompra", SqlDbType.SmallInt).Value = venta;
                    cmd.ExecuteNonQuery();

                    conexion.Close();
                    MessageBox.Show("Venta eliminada");
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void btnmenu_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu_principal.Show();
        }

        private void btmsalir_Click(object sender, EventArgs e)
        {
            this.Close();
            Menu_principal.Close();
        }

        private void PBactualizar_Click(object sender, EventArgs e)
        {
            if (txtventa.Text != "" && txtcantidad.Text != "" && txtfinal.Text != "" && txtuni.Text != "" && CBcliente.Text != "" && CBEmpleado.Text != "")
            {
                conexion.Open();
                if (conexion.State == ConnectionState.Open)
                {
                    int venta, ca, uni, fin;
                    venta = Convert.ToInt32(txtventa.Text);
                    ca = Convert.ToInt32(txtcantidad.Text);
                    uni = Convert.ToInt32(txtuni.Text);
                    fin = Convert.ToInt32(txtfinal.Text);

                    cmd = conexion.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "actualizarcompra";
                    cmd.Parameters.Add("@IdCompra", SqlDbType.SmallInt).Value = venta;
                    cmd.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = DFC.Text;
                    cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = ca;
                    cmd.Parameters.Add("@Precio_Unitario", SqlDbType.Int).Value = uni;
                    cmd.Parameters.Add("@Precio_Final", SqlDbType.Int).Value = fin;
                    cmd.Parameters.Add("@Fk_IdEmpleado", SqlDbType.SmallInt).Value = CBEmpleado.SelectedValue;
                    cmd.ExecuteNonQuery();

                    cmd2 = conexion.CreateCommand();
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandText = "actualizarseincluye";
                    cmd2.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = Convert.ToInt32(txtventa.Text);
                    cmd2.Parameters.Add("@Fk_IdArticulo", SqlDbType.SmallInt).Value = CBArticulo.SelectedValue;
                    cmd2.Parameters.Add("@Fk_IdServicio", SqlDbType.SmallInt).Value = CBServicio.SelectedValue;
                    cmd2.ExecuteNonQuery();

                    cmd3 = conexion.CreateCommand();
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = "actualizarrealiza";
                    cmd3.Parameters.Add("@Fk_Idcliente", SqlDbType.SmallInt).Value = CBcliente.SelectedValue;
                    cmd3.Parameters.Add("@Fk_IdCompra", SqlDbType.SmallInt).Value = Convert.ToInt32(txtventa.Text);
                    cmd3.ExecuteNonQuery();

                    MessageBox.Show("Venta actualizada");
                    Menu_principal.Show();
                    conexion.Close();
                    limpiar();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void PBBuscar_Click(object sender, EventArgs e)
        {
            if (txtventa.Text != "")
            {
                int bandera = 0;

                conexion.Open();
                if (conexion.State == ConnectionState.Open)
                {
                    query = "select * from compra where Idcompra = '" + txtventa.Text + "' ";
                    cmd = new SqlClient.SqlCommand(query, conexion);
                    cmd.ExecuteNonQuery();
                    sqlread = cmd.ExecuteReader();
                    while (sqlread.Read())
                    {
                        int a = Convert.ToInt32(sqlread["cantidad"]);
                        bandera = 1;
                        DFC.Text = Convert.ToString(sqlread["Fecha"]);
                        txtcantidad.Text = a.ToString();
                        txtuni.Text = Convert.ToString(sqlread["Precio_unitario"]);
                        txtfinal.Text = Convert.ToString(sqlread["precio_final"]);
                        CBEmpleado.SelectedValue = Convert.ToInt32(sqlread["Fk_idempleado"]);
                    }
                    sqlread.Close();

                    query = "select * from seincluye where Fk_Idcompra = '" + txtventa.Text + "' ";
                    cmd2 = new SqlClient.SqlCommand(query, conexion);
                    cmd2.ExecuteNonQuery();
                    sqlread = cmd2.ExecuteReader();
                    while (sqlread.Read())
                    {
                        bandera = 1;
                        CBArticulo.SelectedValue = Convert.ToInt32(sqlread["fk_idarticulo"]);
                        CBServicio.SelectedValue = Convert.ToInt32(sqlread["fk_idservicio"]);
                    }
                    sqlread.Close();

                    query = "select * from Realiza where Fk_Idcompra = '" + txtventa.Text + "' ";
                    cmd3 = new SqlClient.SqlCommand(query, conexion);
                    cmd3.ExecuteNonQuery();
                    sqlread = cmd3.ExecuteReader();
                    while (sqlread.Read())
                    {
                        bandera = 1;
                        CBcliente.SelectedValue = Convert.ToInt32(sqlread["fk_idcliente"]);
                    }
                    if (bandera != 1)
                    {
                        MessageBox.Show("Registro inexistente", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    sqlread.Close();
                    cmd.Dispose();
                    conexion.Close();
                    MessageBox.Show("Conexión Exitosa");
                }
                else
                {
                    MessageBox.Show("Fallo la conexión");
                }
            }
            else
            {
                MessageBox.Show("Favor de llenar la información solicitada", "Registro de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void ventas_Load(object sender, EventArgs e)
        {

        }
    }
}
