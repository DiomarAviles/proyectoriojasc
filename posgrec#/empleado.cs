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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Npgsql;
namespace posgrec_
{
    public class Empleado: Form
    {
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
                query = "select Idempleado from empleado";
                using (cmd = new SqlClient.SqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            a = sqlread["idempleado"].ToString();
                        }
                    }
                }

                nex = Convert.ToInt32(a);
                nex = nex + 1;
                a = nex.ToString();
                conexion.Close();
            }

            return a;
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
                    query = "delete from empleado where idempleado = " + Tbcliente.Text;
                    using (cmd = new SqlClient.SqlCommand(query, conexion))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Empleado Eliminado", MessageBoxButtons.OK);
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
                        cmd.CommandText = "actualizarempleado";
                        cmd.Parameters.Add("@Idempleado", SqlDbType.SmallInt).Value = Tbcliente.Text;
                        cmd.Parameters.Add("@Nombres", SqlDbType.VarChar).Value = tbnom.Text;
                        cmd.Parameters.Add("@apellidop", SqlDbType.VarChar).Value = tbapp.Text;
                        cmd.Parameters.Add("@apellidom", SqlDbType.VarChar).Value = tbapm.Text;
                        cmd.Parameters.Add("@Estado_civil", SqlDbType.VarChar).Value = Tbestciv.Text;
                        cmd.Parameters.Add("@fecha_n", SqlDbType.DateTime).Value = DateTimePicker1.Text;
                        cmd.Parameters.Add("@telefono_e", SqlDbType.VarChar).Value = tbtel.Text;
                        cmd.Parameters.Add("@correo_e", SqlDbType.VarChar).Value = tbcorr.Text;
                        cmd.Parameters.Add("@RFC_e", SqlDbType.VarChar).Value = tbrfc.Text;
                        cmd.Parameters.Add("@Direccion_e", SqlDbType.VarChar).Value = tbdir.Text;
                        cmd.Parameters.Add("@nss", SqlDbType.VarChar).Value = TBnss.Text;
                        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = Tbciu.Text;
                        cmd.Parameters.Add("@estado", SqlDbType.VarChar).Value = tbest.Text;
                        cmd.Parameters.Add("@CP", SqlDbType.Int).Value = tbcp.Text;
                        cmd.Parameters.Add("@Estatus", SqlDbType.Int).Value = Tbestatus.Text;
                        cmd.Parameters.Add("@FK_idcargo", SqlDbType.Int).Value = Ccargo.SelectedValue;

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Empleado actualizado", MessageBoxButtons.OK);
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
                query = "select * from empleado where idempleado = " + ComboBox1.SelectedValue;
                using (cmd = new SqlClient.SqlCommand(query, conexion))
                {
                    cmd.ExecuteNonQuery();
                    using (sqlread = cmd.ExecuteReader())
                    {
                        while (sqlread.Read())
                        {
                            bandera = 1;
                            tbnom.Text = sqlread["Nombres"].ToString();
                            tbapp.Text = sqlread["apellidop"].ToString();
                            tbapm.Text = sqlread["apellidom"].ToString();
                            Tbestciv.Text = sqlread["Estado_civil"].ToString();
                            DateTimePicker1.Text = sqlread["Fecha_n"].ToString();
                            tbtel.Text = sqlread["telefono_e"].ToString();
                            tbcorr.Text = sqlread["Correo_e"].ToString();
                            tbrfc.Text = sqlread["RFC_e"].ToString();
                            tbdir.Text = sqlread["direccion_e"].ToString();
                            TBnss.Text = sqlread["@nss"].ToString();
                            Tbciu.Text = sqlread["ciudad"].ToString();
                            tbest.Text = sqlread["estado"].ToString();
                            tbcp.Text = sqlread["CP"].ToString();
                            Tbestatus.Text = sqlread["Estatus"].ToString();
                            Ccargo.SelectedValue = sqlread["Fk_idCargo"];
                        }
                    }
                }

                if (bandera != 1)
                {
                    MessageBox.Show("Registro inexistente ", "Empleado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conexion.Close();
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

        private void Empleado_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet20.Empleado' Puede moverla o quitarla según sea necesario.
            this.EmpleadoTableAdapter1.Fill(this.PapeleriaDataSet20.Empleado);
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet15.Cargo' Puede moverla o quitarla según sea necesario.
            this.CargoTableAdapter.Fill(this.PapeleriaDataSet15.Cargo);
            // TODO: esta línea de código carga datos en la tabla 'PapeleriaDataSet14.Empleado' Puede moverla o quitarla según sea necesario.
            this.EmpleadoTableAdapter.Fill(this.PapeleriaDataSet14.Empleado);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Usuario.Show();
        }

        private void FillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.EmpleadoTableAdapter.FillBy(this.PapeleriaDataSet14.Empleado);
            }
            catch

}

