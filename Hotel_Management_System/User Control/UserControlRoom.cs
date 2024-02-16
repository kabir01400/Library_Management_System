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
    public partial class UserControlRoom : UserControl
    {
        private readonly Hotel_Management_SystemEntities3 dbContext;
        private Room_Table model = new Room_Table();
        private string No = "";
        private string Free = "";

        public UserControlRoom()
        {
            InitializeComponent();
            dbContext = new Hotel_Management_SystemEntities3();
        }

       private void Clear()
        {
            comboBoxType.SelectedIndex = 0;
            textBoxPhoneNo.Clear();
            radioButtonYes.Checked = false;
            radioButtonNo.Checked = false;
            tabControlRoom.SelectedTab = tabPageAddRoom;

        }

        private void Clear1()
        {
            comboBoxType1.SelectedIndex = 0;
            textBoxPhoneNo1.Clear();
            radioButtonYes1.Checked = false;
            radioButtonNo1.Checked = false;
            

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (radioButtonYes.Checked)
                Free = "Yes";
            if (radioButtonNo.Checked)
                Free = "No";

            if (comboBoxType1.SelectedIndex == 0 && textBoxPhoneNo.Text.Trim() == string.Empty && Free == "")
            {
                MessageBox.Show("Required all!!!", "Required!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string CheckPhone = textBoxPhoneNo.Text.Trim();
                var phone = dbContext.Room_Table.FirstOrDefault(p => p.Room_Phone == CheckPhone);
                if (phone == null) { 

                    model.Room_Type = comboBoxType.SelectedItem.ToString();
                    model.Room_Phone = textBoxPhoneNo.Text.Trim();
                    model.Room_Free = Free.ToString();

                    dbContext.Room_Table.Add(model);
                    dbContext.SaveChanges();
                    Clear();
                    MessageBox.Show("Room added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Phone No. already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tabPageAddRoom_Leave(object sender, EventArgs e)
        {
            Clear();
            Clear1();
        }

        private void tabPageAddRoom_Enter(object sender, EventArgs e)
        {

        }

        private void tabPageSearchRoom_Enter(object sender, EventArgs e)
        {
            dataGridViewRoom.DataSource = dbContext.Room_Table.ToList<Room_Table>();
        }

        private void tabPageSearchRoom_Leave(object sender, EventArgs e)
        {
            textBoxSearchRoomNo.Clear();
        }

        private void textBoxSearchRoomNo_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearchRoomNo.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var rooms = dbContext.Room_Table.Where(u => u.Room_Number.ToString().Contains(searchTerm)).ToList();
                dataGridViewRoom.DataSource = rooms;
            }
            else
            {
                dataGridViewRoom.DataSource = dbContext.Room_Table.ToList();
            }
        }

        private void dataGridViewRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridViewRoom.Rows[e.RowIndex];
                No = row.Cells[0].Value.ToString();
                comboBoxType1.SelectedItem = row.Cells[1].Value.ToString();
                textBoxPhoneNo1.Text = row.Cells[2].Value.ToString();
                Free = row.Cells[3].Value.ToString();

                if (Free == "Yes")
                    radioButtonYes1.Checked = true;
                if(Free == "No")
                    radioButtonNo1.Checked = true;  
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            
                if (radioButtonYes1.Checked)
                    Free = "Yes";
                if (radioButtonNo1.Checked)
                    Free = "No";
            if (!string.IsNullOrEmpty(No))
            {

                int roomNumber = int.Parse(No);
                var roomToUpdate = dbContext.Room_Table.FirstOrDefault(r => r.Room_Number == roomNumber);
            
                if (roomToUpdate != null)
                {
                    roomToUpdate.Room_Type = comboBoxType1.SelectedItem.ToString();
                    roomToUpdate.Room_Phone = textBoxPhoneNo1.Text.Trim();
                    roomToUpdate.Room_Free = Free;
                    dbContext.SaveChanges();

                    MessageBox.Show("Room Record Updated Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear1();
                }
                else
                {
                    MessageBox.Show("Room not found for updating.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a room to Update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(No))
            {
                if (MessageBox.Show("Are you sure you want to delete this record", "Message", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int roomNumber = int.Parse(No);
                    var roomToDelete = dbContext.Room_Table.FirstOrDefault(r => r.Room_Number == roomNumber);
                    if (roomToDelete != null)
                    {
                        dbContext.Room_Table.Remove(roomToDelete);
                        dbContext.SaveChanges();
                        Clear1();
                        MessageBox.Show("Room deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Room not found for deletion.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please select a room to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserControlRoom_Load(object sender, EventArgs e)
        {
            comboBoxType.SelectedItem = 0;
            comboBoxType1.SelectedItem = 0;

        }

        private void tabPageUpdateAndDeleteRoom_Leave(object sender, EventArgs e)
        {
            Clear1();
        }
    }
}
