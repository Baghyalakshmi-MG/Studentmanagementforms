using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Studentmanagementforms
{
    public partial class Form1 : Form
    {
        string ConnectionString = "Data Source=localhost;Initial Catalog=StudentDB;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public Form1()
        {
            InitializeComponent();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            { MessageBox.Show("Please select a student to delete");
                return;
            }

            int id = int.Parse(textBox1.Text);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string query = "Delete from students Where Id=@Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.ExecuteNonQuery();

            }
            MessageBox.Show("Student Deleted Successfully!");

            button2_Click(null, null);

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox2.Text;
            string ageText = textBox3.Text;
            string email = textBox4.Text;

            if (name == "" || ageText == "" || email == "")
            {
                MessageBox.Show("please fill all fields");
                return;
            }

            int age = int.Parse(ageText);

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string query = "Insert into Students (Name, Age, Email) Values (@Name,@Age,@Email)";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();

            }
            MessageBox.Show("Student Added Successfully!");

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string Query = "Select * from Students";
                SqlDataAdapter adapter = new SqlDataAdapter(Query, con);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = row.Cells["Id"].Value.ToString();
                textBox2.Text = row.Cells["Name"].Value.ToString();
                textBox3.Text = row.Cells["Age"].Value.ToString();
                textBox4.Text = row.Cells["Email"].Value.ToString();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Please select a student");
                return;
            }
            string name;
            int age;
            int id = int.Parse(textBox1.Text);
            name = textBox2.Text;
            age = int.Parse(textBox3.Text);
            string email = textBox4.Text;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                string Query = "Update Students Set Name=@Name, Age= @Age, Email=@Email Where Id=@Id";

                SqlCommand cmd = new SqlCommand(Query, con);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();

            }

            MessageBox.Show("Student Updated Successfully!");
            button2_Click(null, null);

        }
    }
}
