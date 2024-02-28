using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlSetting : UserControl
    {
        private readonly Hotel_Management_SystemEntities1 dbContext;
        private User_Table model = new User_Table();
        private string ID = "";
        public UserControlSetting()
        {
            InitializeComponent();
            dbContext = new Hotel_Management_SystemEntities1();
        }

        //void AlertBox(Color backColor, Color color, string title, string text, Image icon)
        //{
        //    Alert alert = new Alert();
        //    alert.BackColor = backColor;
        //    alert.ColorAlertBox = color;
        //    alert.TitleAlertBox = title;
        //    alert.TextAlertBox = text;
        //    alert.IconAlertBox = icon;

        //    //alert.TopMost = true; // Make the alert topmost
        //    //alert.PositionAlertBox(); // Call the method to set the position
        //    //alert.Show(); // Show the alert box

        ////    alert.TopMost = true; // Make the alert topmost
        ////    alert.Owner = UserControlSetting; // Set the owner to yourForm
        ////    alert.PositionAlertBox(); // Call the method to set the position
        ////    alert.Show(); // Show the alert box
       // }

        public void Clear()
        {
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            tabControlUser.SelectedTab = tabPageAddUser;
        }

        public void Clear1()
        {
            textBoxUsername1.Clear();
            textBoxPassword1.Clear();
            ID = "";
        }

        private void tabPageAddUser_Leave(object sender, EventArgs e)
        {
            Clear();
        }

        private void tabPageSearchUser_Enter(object sender, EventArgs e)
        {
            //textBoxSearchUsername.Clear();
           // if(dataGridViewUser.DataSource == null)
            dataGridViewUser.DataSource = dbContext.User_Table.ToList<User_Table>();
        }


        private void tabPageSearchUser_Leave(object sender, EventArgs e)
        {
            textBoxSearchUsername.Clear();
        }

        private void tabPageUpdateAndDeleteUser_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            string username = textBoxUsername.Text.Trim();
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(textBoxPassword.Text.Trim()))
            {

                //AlertBox(Color.LightGray, Color.SeaGreen, "Information", "Required all the fields", Properties.Resources.Information);
                MessageBox.Show("Required all the fields", "Required!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
               
                var existingUser = dbContext.User_Table.FirstOrDefault(u => u.User_Name == username);
                if (existingUser == null)
                {
                    model.User_Name = username;
                    model.User_Password = textBoxPassword.Text.Trim();

                    dbContext.User_Table.Add(model);
                    dbContext.SaveChanges();

                    MessageBox.Show("User added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                }
                 else
                 {
                    MessageBox.Show("User already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
            }
        }

        private bool IsExist(string username)
        {
            var existingUser = dbContext.User_Table.FirstOrDefault( u => u.User_Name == username);
            return existingUser != null;
        }

        // For Updating the user records
        private void buttonUpdate_Click(object sender, EventArgs e)
        {

         string usernameToUpdate = textBoxUsername1.Text.Trim();
            var userToUpdate = dbContext.User_Table.FirstOrDefault(u => u.User_Name == usernameToUpdate);
            if (userToUpdate != null)
            {
                userToUpdate.User_Password = textBoxPassword1.Text.Trim();
                dbContext.SaveChanges();
                MessageBox.Show("User Record Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Record not Found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //For deleting the user
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
              string usernameToDelete = textBoxUsername.Text.Trim();

                // Finding the user in the database;

                var userToDelete = dbContext.User_Table.FirstOrDefault( u => u.User_Name == usernameToDelete);
                if (userToDelete != null)
                {
                    //Deleting the user from the database;
                    dbContext.User_Table.Remove(userToDelete);
                    dbContext.SaveChanges();
                    MessageBox.Show("User Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();

                }
                else
                {
                    MessageBox.Show("Record not Found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1) 
            {
                DataGridViewRow row = dataGridViewUser.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxUsername1.Text = row.Cells[1].Value.ToString();
                textBoxPassword1.Text = row.Cells[2].Value.ToString();
            }
        }

        private void textBoxSearchUsername_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearchUsername.Text.Trim();
            if(!string.IsNullOrEmpty(searchTerm))
            {
                var users = dbContext.User_Table.Where( u => u.User_Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                dataGridViewUser.DataSource = users;
            }
            else
            {
                dataGridViewUser.DataSource = dbContext.User_Table.ToList();
            }

        }
    }
}
