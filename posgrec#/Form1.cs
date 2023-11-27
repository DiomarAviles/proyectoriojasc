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
    public class Form1 : Form
    {
        private void BTNLOGIN_Click(object sender, EventArgs e)
        {
            valoracio();
        }

        private void valoracio()
        {
            SqlClient.SqlDataAdapter da = new SqlClient.SqlDataAdapter();
            DataTable dt = new DataTable();
            query = "select usuario,Contraseña from Usuario where usuario= '" + txtusuario.Text + "' and Contraseña = '" + txtcontra.Text + "'";

            try
            {
                da = new SqlClient.SqlDataAdapter(query, conexion);
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    Menu_principal.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El usuario o la contraseña es incorrecta ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
