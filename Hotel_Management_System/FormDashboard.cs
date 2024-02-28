using Hotel_Management_System.User_Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Management_System
{
    public partial class FormDashboard : Form
    {
        private Button lastClickedButton;
        public FormDashboard()
        {
            InitializeComponent();
            InitializeButtons();
            //UserControlClient();
        }

        private void MovePanel(Control btn)
        {
            //panelSlide.Top = btn.Top;
            //panelSlide.Height = btn.Height;
        }
               
        private void pictureBoxCross(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBoxClose_MouseHover(object sender, EventArgs e)
        {
            pictureBoxClose.BackColor = Color.Red;
        }

        private void pictureBoxClose_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxClose.BackColor = Color.WhiteSmoke;
        }

        private void InitializeButtons()
        {
            // Assuming you have two buttons named btnOK and btnAnother
            btnDashboard.Click += btnDashboard_Click;
            btnClient.Click += btnDashboard_Click;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {            
            //// Reset the color of the last clicked button, if any
            //if (lastClickedButton != null)
            //{
            //    lastClickedButton.BackColor = Color.Blue; // Set to default color
            //}

            //// Change the color of the currently clicked button
            //Button clickedButton = (Button)sender;
            //clickedButton.BackColor = Color.Green; // Change to the desired color
            //// Store the currently clicked button for future reference
            //lastClickedButton = clickedButton;

            ClientContainer.Hide();
            SettingContainer.Hide();
            RoomContainer.Hide();



        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            ClientContainer.Visible = false;
            RoomContainer.Visible = false;
            SettingContainer.Height = 557;
            SettingContainer.Visible = true;
        }

      

        private void LogoutLbl_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            formLogin.Show();
            this.Hide()
;        }

        private void btnReservation_Click(object sender, EventArgs e)
        {
            userControlSetting1.Visible = false;
            ReservationContainer.Height = 557;
            ReservationContainer.Visible = true; 
            userControlReservation1.Visible = true;



        }

       

        private void btnRoom_Click(object sender, EventArgs e)
        {
            SettingContainer.Visible = false;
            ClientContainer.Visible = false; 
            RoomContainer.Height = 557;
            RoomContainer.Visible = true; 
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            SettingContainer.Visible = false;
            RoomContainer.Visible = false;
            ClientContainer.Height = 556;
            ClientContainer.Visible = true;
        }
    }
}
