using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SDT_pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-8BL3MIG;Initial Catalog=Std_Form;Integrated Security=True");
        public int StudentID;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();

        }

        private void GetStudentsRecord()
        {
            
            SqlCommand cmd = new SqlCommand("Select * from StudentsTb", con);
            DataTable dt = new DataTable();

            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();

            dt.Load(sdr);

            con.Close();

            StudentRecorddataGridView.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentsTb VALUES (@Name, @FatherName, @Roll, @Address, @Mobile)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name",txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName",txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address",txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile",txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New Student is  Successfully Added", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecord();
                ResetFormControl();

            
            }
        }

        private bool IsValid()
        {
            if (txtStudentName.Text == string.Empty)
            {
                MessageBox.Show("Student Name is required", "Falied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControl();

        }

        private void ResetFormControl()
        {
            StudentID = 0;
            txtStudentName.Clear();
            txtFatherName.Clear();
            txtRollNo.Clear();
            txtAddress.Clear();
            txtMobile.Clear();

            txtStudentName.Focus();
        }

        private void StudentRecorddataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecorddataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentName.Text = StudentRecorddataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtFatherName.Text = StudentRecorddataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtRollNo.Text = StudentRecorddataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecorddataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecorddataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("Update StudentsTb SET Name = @Name,FatherName = @FatherName,RollNo = @Roll,Address = @Address,Mobile = @Mobile WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtStudentName.Text);
                cmd.Parameters.AddWithValue("@FatherName", txtFatherName.Text);
                cmd.Parameters.AddWithValue("@Roll", txtRollNo.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Data is Updated  Successfully ", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecord();
                ResetFormControl();
            }
            else
            {
                MessageBox.Show("Please select a student to update his information ", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM StudentsTb WHERE StudentID = @ID", con);
                cmd.CommandType = CommandType.Text;
  
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Data is Deleted Successfully ", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetStudentsRecord();
                ResetFormControl();
            }
            else
            {
                MessageBox.Show("Please select a student to Delete ", "Select", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}