using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlClient : UserControl
    {
      private readonly Hotel_Management_SystemEntities2 dbContext;
        private Client_Table model = new Client_Table();
        private string ID = "";
        public UserControlClient()
        {
            InitializeComponent();
            dbContext = new Hotel_Management_SystemEntities2();
        }

        

        private void tabControlClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            textBoxFirstName.Clear();
            textBoxLastName.Clear();
            textBoxPhoneNo.Clear();
            textBoxAddress.Clear();
            tabControlClient.SelectedTab = tabPageAddClient;
        }

        public void Clear1()
        {
            textBoxFirstName1.Clear();
            textBoxLastName1.Clear();
            textBoxPhoneNo1.Clear();
            textBoxAddress1.Clear();
           
        }



        private void buttonAdd_Click(object sender, EventArgs e)
        {
           string phoneNo = textBoxPhoneNo.Text.Trim();
            if(textBoxFirstName.Text.Trim() == string.Empty && textBoxLastName.Text.Trim() == string.Empty && textBoxPhoneNo.Text.Trim() == string.Empty && textBoxAddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Required all the fields", "Required!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                var existClient = dbContext.Client_Table.FirstOrDefault(u => u.Client_Phone == phoneNo);
                if (existClient == null)
                {
                    model.Client_FirstName = textBoxFirstName.Text.Trim();
                    model.Client_LastName = textBoxLastName.Text.Trim();
                    model.Client_Phone = phoneNo;
                    model.Client_Address = textBoxAddress.Text.Trim();
                    dbContext.Client_Table.Add(model);
                    dbContext.SaveChanges();
                    MessageBox.Show("Client added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
                else
                {
                    MessageBox.Show("Client already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabPageAddClient_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageSearchClient_Enter(object sender, EventArgs e)
        {
            dataGridViewClient.DataSource = dbContext.Client_Table.ToList<Client_Table>();
        }

        private void tabPageSearchClient_Leave(object sender, EventArgs e)
        {
            textBoxSearchPhoneNo.Clear();
        }

        private void textBoxSearchPhoneNo_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearchPhoneNo.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                var users = dbContext.Client_Table.Where(u => u.Client_Phone.ToLower().Contains(searchTerm.ToLower())).ToList();
                dataGridViewClient.DataSource = users;
            }
            else
            {
                dataGridViewClient.DataSource = dbContext.Client_Table.ToList();
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            string phoneNoToUpdate = textBoxPhoneNo1.Text.Trim();
            int idToUpdate;
            if(int.TryParse(ID, out idToUpdate))
            {
                var clientToUpdate = dbContext.Client_Table.FirstOrDefault(u => u.Client_ID == idToUpdate);
                if (clientToUpdate != null)
                {
                    clientToUpdate.Client_FirstName = textBoxFirstName1.Text.Trim();
                    clientToUpdate.Client_LastName = textBoxLastName1.Text.Trim();
                    clientToUpdate.Client_Phone = textBoxPhoneNo1.Text.Trim();
                    clientToUpdate.Client_Address = textBoxAddress1.Text.Trim();
                    dbContext.SaveChanges();

                    MessageBox.Show("Client Record Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                }
                else
                {
                    MessageBox.Show("Client Record not Found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("User ID is Invalid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void dataGridViewClient_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewClient.Rows[e.RowIndex];
                ID = row.Cells[0].Value.ToString();
                textBoxFirstName1.Text = row.Cells[1].Value.ToString();
                textBoxLastName1.Text = row.Cells[2].Value.ToString();
                textBoxPhoneNo1.Text = row.Cells[3].Value.ToString();
                textBoxAddress1.Text = row.Cells[4].Value.ToString();

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int idToDelete;
                if (int.TryParse(ID, out idToDelete))
                {
                    var clientToDelete = dbContext.Client_Table.FirstOrDefault(u => u.Client_ID ==  idToDelete);
                    if (clientToDelete != null)
                    {
                        dbContext.Client_Table.Remove(clientToDelete);
                        dbContext.SaveChanges();
                        MessageBox.Show("Client Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Clear1();
                    }
                    else
                    {
                        MessageBox.Show("Record not Found !!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Invalid ID format", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
