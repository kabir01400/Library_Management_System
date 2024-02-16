using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hotel_Management_System
{

    public partial class FormLogin : Form
    {
        private readonly Hotel_Management_SystemEntities1 dbContext;
        private ErrorProvider errorProvider = new ErrorProvider();

        public FormLogin()
        {
            InitializeComponent();
            dbContext = new Hotel_Management_SystemEntities1();
        }

        private void pictureBoxCross_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBoxCross_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxCross.BackColor = Color.WhiteSmoke;
        }

        private void pictureBoxShow_Click(object sender, EventArgs e)
        {
            pictureBoxShow.Hide();
            textBoxPassword.UseSystemPasswordChar = false;
            pictureBoxHide.Show();
        }

        private void pictureBoxHide_Click(object sender, EventArgs e)
        {
            pictureBoxHide.Hide();
            textBoxPassword.UseSystemPasswordChar=true;
            pictureBoxShow.Show();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text.Trim();
            string password = textBoxPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                errorProvider.SetError(textBoxUsername, "Username is required");
            }
            else if (string.IsNullOrEmpty(password))
            {
                errorProvider.SetError(textBoxPassword, "Password is required");
            }
            else
            {
                var user = dbContext.User_Table.FirstOrDefault(u => u.User_Name == username && u.User_Password == password);

                if (user != null)
                {
                    MessageBox.Show("Login Successful", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormDashboard formDashboard = new FormDashboard();
                    formDashboard.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
