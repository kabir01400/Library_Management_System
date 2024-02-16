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
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }

        public Color BackColorAlertBox { 
            get{return this.BackColor;} 
            set { this.BackColor = value;}
        }

        public Color ColorAlertBox
        {
            get { return LinAlertBox.BackColor; }
            set {LinAlertBox.BackColor = lblTextAlertBox.ForeColor = lblTextAlertBox.ForeColor = value; }
        }

        public Image IconAlertBox
        {
            get { return PicAlertBox.Image; }
            set { PicAlertBox.Image = value; }
        }

        public string TitleAlertBox
        {
            get { return lblTitleAlertBox.Text; }
            set { lblTitleAlertBox.Text = value; }
        }

        public string TextAlertBox
        {
            get { return lblTextAlertBox.Text; }
            set { lblTextAlertBox.Text = value; }
        }

        public void PositionAlertBox()
        {
            int xPos = 0;
            int yPos = 0;
            xPos = Screen.GetWorkingArea(this).Width;
            yPos = Screen.GetWorkingArea(this).Height;
            //this.Location = new Point(xPos- this.Width, yPos - this.Height);
            this.Location = new Point(xPos - this.Width, yPos);
        }

        private void TimerAnimation_Tick(object sender, EventArgs e)
        {
            LinAlertBox.Width = LinAlertBox.Width + 2;
            if(LinAlertBox.Width == 500)
            {
                this.Dispose();   
            }
        }

        private void Alert_Load(object sender, EventArgs e)
        {
            PositionAlertBox();
            //for(int i = 0; i < 500; i++) 
          
            //    TimerAnimation.Start();
                TimerVisibility.Start();


        }

        private void TimerVisibility_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                Opacity -= 0.0020;
            }
            else
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
