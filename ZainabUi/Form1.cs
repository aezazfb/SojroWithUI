using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using ZainabUi.Myforms;

namespace ZainabUi
{
    public partial class panelmenu : Form
    {
        //feilds
        private IconButton currentbtn;
        private Panel leftborderbtn;
        private Form currentChildForm;
        public panelmenu()
        {
            InitializeComponent();
            leftborderbtn = new Panel();
            leftborderbtn.Size = new Size(7, 60);
            panel1.Controls.Add(leftborderbtn);
            //Form
            this.Text = String.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            OpenChildForm(new Sojro());



        }

        private struct RGBcolors
        {
            public static Color color1 = Color.FromArgb(235, 192, 52);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);


        }

        private void ActivateButton(object senderbtn, Color color)
        {
            if (senderbtn != null)
            {
                DisableButton();
                currentbtn = (IconButton)senderbtn;
                currentbtn.BackColor = Color.FromArgb(31, 30, 68);
                currentbtn.ForeColor = Color.Gainsboro;
                //currentbtn.TextAlign = ContentAlignment.MiddleCenter;
                //currentbtn.IconColor = color;
                //currentbtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                //currentbtn.ImageAlign = ContentAlignment.MiddleRight;
                //left bordre button
                leftborderbtn.BackColor = color;
                leftborderbtn.Location = new Point(0, currentbtn.Location.Y);
                leftborderbtn.Visible = true;
                leftborderbtn.BringToFront();
                //Icon current child form
                //iconCurrentChildForm.IconChar = currentbtn.IconChar;
                //iconCurrentChildForm.IconColor = color;

            }


        }

        private void DisableButton()
        {
            if (currentbtn != null)
            {
                currentbtn.BackColor = Color.FromArgb(27, 31, 44);
                currentbtn.ForeColor = Color.Gainsboro;
                //currentbtn.TextAlign = ContentAlignment.MiddleLeft;
                //currentbtn.IconColor = Color.FromArgb(255, 128, 0);
                //currentbtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                //currentbtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        private void OpenChildForm(Form childForm)
        {
            if (currentChildForm != null)
            {
                //open onlyform
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            paneldesktop.Controls.Add(childForm);
            paneldesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbltitlechildform.Text = childForm.Text;

        }


        private void iconButton1_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color1);

            iconCurrentChildForm.BackgroundImage = global::ZainabUi.Properties.Resources.oto;
            iconCurrentChildForm.BackgroundImageLayout = ImageLayout.Stretch;

            OpenChildForm(new Otoscope());
        }

        private void iconButton2_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color1);

            iconCurrentChildForm.BackgroundImage = global::ZainabUi.Properties.Resources.derm;
            iconCurrentChildForm.BackgroundImageLayout = ImageLayout.Stretch;

            OpenChildForm(new Dermascope());
        }

        private void iconButton3_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color3);
            OpenChildForm(new Products());
        }

        private void iconButton4_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color4);
            OpenChildForm(new Customers());
        }

        private void iconButton5_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color5);
            OpenChildForm(new Marketing());
        }

        private void iconButton6_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBcolors.color6);
            OpenChildForm(new Settings());
        }
        private void btnHome_Click(object sender, EventArgs e)
        {
            Reset();
            OpenChildForm(new Sojro());
        }
        private void Reset()
        {
            DisableButton();
            leftborderbtn.Visible = false;
            //iconCurrentChildForm.IconChar = IconChar.Home;
            iconCurrentChildForm.BackgroundImage = null;
            //iconCurrentChildForm.IconColor = Color.MediumPurple;
            lbltitlechildform.Text = "";

        }
        //Drag From
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelmenu_Load(object sender, EventArgs e)
        {

        }

        private void iconButton3_Click_1(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void paneldesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exit2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            Otoscope otoscope = new Otoscope();

        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
