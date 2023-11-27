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
using static posgrec_.Usuario;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace posgrec_
{
    public partial class Usuario : Form
    {
        public class Usuario
        {
            private void PictureBox3_Click(object sender, EventArgs e)
            {
                conexion.Open();
                query = "insert into usuario values('" + tbusuario.Text + "','" + tbcontra.Text + "'," + ComboBox1.SelectedValue + ")";
                cmd = new SqlClient.SqlCommand(query, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario registrado", MessageBoxButtons.OK);
                conexion.Close();

                this.Close();
            }

            private void eliminar_Click(object sender, EventArgs e)
            {
                conexion.Open();
                query = "delete from usuario where idempleado=" + ComboBox1.SelectedValue;
                cmd = new SqlClient.SqlCommand(query, conexion);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Usuario eliminado", MessageBoxButtons.OK);
                conexion.Close();
                this.Close();
            }

            private void Button1_Click(object sender, EventArgs e)
            {
                // Lógica para Button1
            }
        }
    }
}
