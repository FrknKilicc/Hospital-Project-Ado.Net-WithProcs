using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Hospital_Project_Ado.Net_WithProcs
{
    public partial class DoctorPage : Form
    {
        public DoctorPage()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Server=FURKAN\\FURKANKILIC;Database=Hospital;Integrated Security=True;");

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void tabPage1_Load(object sender, EventArgs e)
        {

        }
        private void ListPatient()
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListPatient";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable filldata = new DataTable();
            adapter.Fill(filldata);
            dataGridView1.DataSource = filldata;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListPatient();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            label5.Text = Form1.DoctorName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpPatient";
            cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(textBox1.Text));
            cmd.Parameters.AddWithValue("@NameSurname", textBox2.Text);
            cmd.Parameters.AddWithValue("@PatientAge", Convert.ToInt32(textBox3.Text));
            cmd.ExecuteNonQuery();
            MessageBox.Show("Güncelleme İşlemi Başarıyla Tamamlandı");
            ListPatient();
            con.Close();



        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow column = dataGridView1.CurrentRow;
            textBox1.Text = column.Cells["PatientID"].Value.ToString();
            textBox2.Text = column.Cells["NameSurname"].Value.ToString();
            textBox3.Text = column.Cells["PatientAge"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchPatient";
                cmd.Parameters.AddWithValue("@Keywordss", textBox2.Text);



                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable filldata = new DataTable();
                adapter.Fill(filldata);
                dataGridView1.DataSource = filldata;
                con.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Bölümüne Değer Giriniz ");
                con.Close();
            }
            con.Close();

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }
        private void PatientList()
        {

            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "ListPatient";
            SqlDataAdapter dr = new SqlDataAdapter(command);
            DataTable filldata = new DataTable();
            dr.Fill(filldata);
            dataGridView2.DataSource = filldata;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            PatientList();
        }
        int selectedvalue = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex > 0)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ListDiagnostick";

                SqlDataAdapter dr = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dr.Fill(dataTable);

                if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int selectedValue))
                {
                    List<DataRow> filteredRows = dataTable.AsEnumerable()
                        .Where(row => row.Field<int>("Hasta No") == selectedValue)
                        .ToList();

                    if (filteredRows.Count > 0)
                    {

                        ClearTextBoxValues();

                        foreach (DataRow row in filteredRows)
                        {

                            textBox6.Tag = row["Hasta No"].ToString();
                            textBox6.Text = row["Hasta Adı Soyadı"].ToString();
                            textBox4.Text = row["DiagnosticName"].ToString();
                            textBox4.Tag = row["DiagnosticID"].ToString();
                            textBox5.Text = row["DiagnosticDescription"].ToString();
                            textBox8.Tag = row["DrID"].ToString();
                            textBox8.Text = row["Branches"].ToString();
                            textBox9.Text = row["Hasta Yaşı"].ToString();
                        }
                    }
                    else
                    {

                        ClearTextBoxValues();

                        textBox6.Tag = string.Empty;
                        MessageBox.Show("Seçilen Hasta No ile ilgili veri bulunamadı.");
                    }
                }
                else
                {

                    ClearTextBoxValues();

                    textBox6.Tag = string.Empty;
                    MessageBox.Show("Geçerli bir seçim yapmadınız.");
                }

                con.Close();
            }



        }
        private void ClearTextBoxValues()
        {
            textBox6.Tag = string.Empty;
            textBox6.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Tag = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;
        }
        private void UpdateTextBoxValues(DataTable dataTable)
        {
            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[0];
                textBox6.Tag = selectedvalue;
                textBox6.Text = row["Hasta Adı Soyadı"].ToString();
                textBox4.Text = row["DiagnosticName"].ToString();
                textBox5.Text = row["DiagnosticDescription"].ToString();
                textBox7.Text = row["Doktor Adı"].ToString();
                textBox8.Tag = row["DrID"].ToString();
                textBox8.Text = row["Branches"].ToString();
                textBox9.Text = row["Hasta Yaşı"].ToString();
            }
            else
            {
                textBox6.Tag = string.Empty;
                textBox6.Text = string.Empty;
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
                textBox7.Text = string.Empty;
                textBox8.Tag = string.Empty;
                textBox8.Text = string.Empty;
                textBox9.Text = string.Empty;
                MessageBox.Show("Seçilen Hasta No ile ilgili veri bulunamadı.");
            }
        }


        private void DoctorPage_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            textBox7.Text = Form1.DoctorName;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;


            textBox15.ReadOnly = true;
            textBox14.ReadOnly = true;
            textBox10.ReadOnly = true;



            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListDiagnosick";
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            dr.Fill(dataTable);

            DataRow newRow = dataTable.NewRow();
            newRow["Hasta Adı Soyadı"] = "Seçiniz";
            newRow["Hasta No"] = -1;
            dataTable.Rows.InsertAt(newRow, 0);

            comboBox1.DataSource = dataTable;
            comboBox1.DisplayMember = "Hasta Adı Soyadı";
            comboBox1.ValueMember = "Hasta No";

            dataGridView2.DataSource = dataTable;

            /////


            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PatientList";
            SqlDataAdapter dr1 = new SqlDataAdapter(cmd);
            DataTable dataTable1 = new DataTable();
            dr1.Fill(dataTable1);

            DataRow newRoww = dataTable1.NewRow();
            newRoww["Hasta Adı Soyadı"] = "Seçiniz";
            newRoww["Hasta No"] = -1;
            dataTable1.Rows.InsertAt(newRoww, 0);

            comboBox2.DataSource = dataTable1;
            comboBox2.DisplayMember = "Hasta Adı Soyadı";
            comboBox2.ValueMember = "Hasta No";
            ////

            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListDiagnosick";
            SqlDataAdapter dr11 = new SqlDataAdapter(cmd);
            DataTable dataTable11 = new DataTable();
            dr11.Fill(dataTable11);

            DataRow newRowww = dataTable11.NewRow();
            newRoww["Hasta Adı Soyadı"] = "Seçiniz";
            newRoww["Hasta No"] = -1;
            dataTable11.Rows.InsertAt(newRowww, 0);

            comboBox3.DataSource = dataTable11;
            comboBox3.DisplayMember = "Hasta Adı Soyadı";
            comboBox3.ValueMember = "Hasta No";
            con.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "UpDiagnostick";
            cmd.Parameters.AddWithValue("@DiagnosticID", Convert.ToInt32(textBox4.Tag));
            cmd.Parameters.AddWithValue("@DoctorID", Convert.ToInt32(textBox8.Tag));
            cmd.Parameters.AddWithValue("@PatientID", Convert.ToInt32(textBox6.Tag));
            cmd.Parameters.AddWithValue("@DiagnosticName", textBox4.Text);
            cmd.Parameters.AddWithValue("@DiagnosticDescription", textBox5.Text);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Güncelleme İşemi Başarılı");
            PatientList();

            con.Close();


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex > 0)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PatientList";

                SqlDataAdapter dr = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dr.Fill(dataTable);

                if (comboBox2.SelectedValue != null && int.TryParse(comboBox2.SelectedValue.ToString(), out int selectedValue))
                {
                    List<DataRow> filteredRows = dataTable.AsEnumerable()
                        .Where(row => row.Field<int>("Hasta No") == selectedValue)
                        .ToList();

                    if (filteredRows.Count > 0)
                    {

                        ClearTextBoxValues();

                        foreach (DataRow row in filteredRows)
                        {

                            textBox15.Tag = row["Hasta No"].ToString();
                            textBox15.Text = row["Hasta Adı Soyadı"].ToString();
                            textBox14.Text = row["Hasta Yaşı"].ToString();
                            textBox10.Text = Form1.DoctorName;


                        }
                    }
                    else
                    {

                        ClearTextBoxValues();

                        textBox15.Tag = string.Empty;
                        MessageBox.Show("Seçilen Hasta No ile ilgili veri bulunamadı.");
                    }
                }
                else
                {

                    ClearTextBoxValues();

                    textBox15.Tag = string.Empty;
                    MessageBox.Show("Geçerli bir seçim yapmadınız.");
                }

                con.Close();
            }



        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > 0)
            {
                con.Open();

               
                SqlCommand getDoctorIdCmd = new SqlCommand();
                getDoctorIdCmd.Connection = con;
                getDoctorIdCmd.CommandType = CommandType.Text;
                getDoctorIdCmd.CommandText = "SELECT DoctorID FROM Doctors WHERE NameSurname = @DoctorName";
                getDoctorIdCmd.Parameters.AddWithValue("@DoctorName", Form1.DoctorName);

                int doctorId = Convert.ToInt32(getDoctorIdCmd.ExecuteScalar()); 

                
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "AdddDiagnostic";
                sqlCommand.Parameters.AddWithValue("@DoctorID", doctorId); 
                sqlCommand.Parameters.AddWithValue("@PatientID", Convert.ToInt32(textBox15.Tag));
                sqlCommand.Parameters.AddWithValue("@NameSurname", Form1.DoctorName);
                sqlCommand.Parameters.AddWithValue("@DiagnosticName", textBox12.Text);
                sqlCommand.Parameters.AddWithValue("@DiagnosticDescription", textBox11.Text);
                sqlCommand.ExecuteNonQuery();

                MessageBox.Show("Kayıt Başarıyla Eklendi.");
                con.Close();
            }
            else
            {
                MessageBox.Show("Hasta Seçimi Yapınız");
                con.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ListDrugs";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand getDoctorIdCmd = new SqlCommand();
            getDoctorIdCmd.Connection = con;
            getDoctorIdCmd.CommandType = CommandType.Text;
            getDoctorIdCmd.CommandText = "SELECT DoctorID FROM Doctors WHERE NameSurname = @DoctorName";
            getDoctorIdCmd.Parameters.AddWithValue("@DoctorName", Form1.DoctorName);

            int doctorId = Convert.ToInt32(getDoctorIdCmd.ExecuteScalar());

            
            SqlCommand command = new SqlCommand();
            command.Connection = con;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "AddRecipes";
            command.Parameters.AddWithValue("@DoctorIDD", doctorId);
            command.Parameters.AddWithValue("@PatientID", Convert.ToInt32(textBox16.Tag));
            command.Parameters.AddWithValue("@DrugName", textBox18.Text);
            command.ExecuteNonQuery();
            MessageBox.Show("Kayıt Başarılı");
            con.Close();

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex > 0)
            {
                con.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PatientList";

                SqlDataAdapter dr = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();

                dr.Fill(dataTable);

                if (comboBox3.SelectedValue != null && int.TryParse(comboBox3.SelectedValue.ToString(), out int selectedValue))
                {
                    List<DataRow> filteredRows = dataTable.AsEnumerable()
                        .Where(row => row.Field<int>("Hasta No") == selectedValue)
                        .ToList();

                    if (filteredRows.Count > 0)
                    {

                        ClearTextBoxValues();

                        foreach (DataRow row in filteredRows)
                        {

                            textBox16.Tag = row["Hasta No"].ToString();
                            textBox16.Text = row["Hasta Adı Soyadı"].ToString();
                            textBox17.Text = row["Hasta Yaşı"].ToString();
                            textBox10.Text = Form1.DoctorName;
                        }
                    }
                    else
                    {

                        ClearTextBoxValues();

                        textBox6.Tag = string.Empty;
                        MessageBox.Show("Seçilen Hasta No ile ilgili veri bulunamadı.");
                    }
                }
                else
                {

                    ClearTextBoxValues();

                    textBox6.Tag = string.Empty;
                    MessageBox.Show("Geçerli bir seçim yapmadınız.");
                }

                con.Close();
            }

        }
    }

}


