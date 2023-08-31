using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Hospital_Project_Ado.Net_WithProcs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //string connectionString = "Server=FURKAN\\FURKANKILIC;Database=Customers;Integrated Security=True;";
        SqlConnection con = new SqlConnection("Server=FURKAN\\FURKANKILIC;Database=Hospital;Integrated Security=True;");
        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;




        }
        public static string DoctorName = "";

        private void button1_Click(object sender, EventArgs e)
        {
         
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "LoginDoctor";
            cmd.Parameters.AddWithValue("@NameSurname", textBox1.Text);
            cmd.Parameters.AddWithValue("@Password", textBox2.Text);
            
            con.Open();
            SqlDataReader reader;
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                DoctorName = textBox1.Text;
                MessageBox.Show("Girişi Başarılı");
                con.Close();
                DoctorPage dp = new DoctorPage();
                dp.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı, Lütfen Tekrar Deneyiniz");
                con.Close();
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {


                panel1.Visible = true;
                panel2.Visible = false;
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
           panel2.Visible= true;
            panel1.Visible=false;
        }
    }
}
