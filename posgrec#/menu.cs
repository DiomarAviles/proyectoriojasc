using System;
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
    public partial class menu : Form
    {
        Public Class Menu_principal
     Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
 
         Ventas.Show()
     End Sub

     Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
 
         Articulos.Show()
     End Sub

     Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
 
         cate.Show()
     End Sub

     Private Sub PictureBox5_Click(sender As Object, e As EventArgs) Handles PictureBox5.Click
 
         proveedor.Show()
     End Sub

     Private Sub PictureBox6_Click(sender As Object, e As EventArgs) Handles PictureBox6.Click
 
         abastece.Show()

     End Sub

     Private Sub PictureBox7_Click(sender As Object, e As EventArgs) Handles PictureBox7.Click
 
         Clientes.Show()

     End Sub

     Private Sub PictureBox9_Click(sender As Object, e As EventArgs) Handles PictureBox9.Click
 
         servicios.Show()
     End Sub

     Private Sub PictureBox8_Click(sender As Object, e As EventArgs) Handles PictureBox8.Click
 
         Empleado.Show()

     End Sub

     Private Sub PictureBox10_Click(sender As Object, e As EventArgs) Handles PictureBox10.Click
 
         dashboard.Show()

     End Sub

 End Class
}
