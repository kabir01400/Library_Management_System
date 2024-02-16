using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System.User_Control
{
    public partial class UserControlReservation : UserControl
    {
        private readonly Hotel_Management_SystemEntities4 dbContext;
        private Reservation_Table model = new Reservation_Table();
        private readonly Hotel_Management_SystemEntities3 dbcon = new Hotel_Management_SystemEntities3();
        private Room_Table room_table = new Room_Table();
        private string RTD = "", No;
        public UserControlReservation()
        {
            InitializeComponent();
            dbContext= new Hotel_Management_SystemEntities4();
        }

        public void Clear()
        {
            comboBoxType.SelectedIndex = 0;
            comboBoxNo.SelectedIndex = 0;
            textBoxClientID.Clear();
            dateTimePickerIn.Value = DateTime.Now;
            dateTimePickerOut.Value = DateTime.Now;
            tabControlReservation.SelectedTab = tabPageAddReservation;
        }
        public void Clear1()
        {
            comboBoxType1.SelectedIndex = 0;
            comboBoxNo1.SelectedIndex = 0;
            textBoxClientID1.Clear();
            dateTimePickerIn1.Value = DateTime.Now;
            dateTimePickerOut1.Value = DateTime.Now;
            
        }

        private void UserControlReservation_Load(object sender, EventArgs e)
        {

            comboBoxType.SelectedIndex = 0;
            comboBoxType1.SelectedIndex = 0;
            comboBoxNo.SelectedIndex = 0;
            comboBoxNo1.SelectedIndex = 0;

            comboBoxType.DataSource = GetRoomType();
            comboBoxNo.DataSource = GetRoomNumbers();

            

            comboBoxType1.DataSource = GetRoomType();
            comboBoxNo1.DataSource = GetRoomNumbers();
        }

        private void tabPageAddReservation_Leave(object sender, EventArgs e)
        {
            Clear();
        }

        private void tabPageUpdateAndDeleteReservation_Leave(object sender, EventArgs e)
        {
            Clear1();
        }

        private void tabPageSearchReservation_Enter(object sender, EventArgs e)
        {
            dataGridViewReservation.DataSource = dbContext.Reservation_Table.ToList<Reservation_Table>();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxType.SelectedIndex == 0 && comboBoxNo.SelectedIndex == 0 && textBoxClientID.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Required all the fields", "Required!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
               model.Reservation_Room_Type = comboBoxType.SelectedItem.ToString();
               model.Reservation_Room_Number = Convert.ToInt32(comboBoxNo.SelectedItem);
               model.Reservation_Client_ID = Convert.ToInt32(textBoxClientID.Text.Trim());
               model.Reservation_In = dateTimePickerIn.Value.ToString();
               model.Reservation_Out = dateTimePickerOut.Value.ToString();

                dbContext.Reservation_Table.Add(model);
                dbContext.SaveChanges();
                Clear();
                MessageBox.Show("Reservation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        // Adding this method to fetch Room_Type data from Room_Table
        private List<string> GetRoomType()
        {
            return dbcon.Room_Table.Select(r => r.Room_Type).Distinct().ToList();
        }
        // Adding this method to fetch Room_Number data from Room_Table
        private List<int> GetRoomNumbers()
        {
            return dbcon.Room_Table.Select(r => r.Room_Number).ToList();
        }
    }
}
