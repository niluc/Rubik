using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using OpenTK;

namespace Rubik
{
    public partial class Home : Form
    {
        //public static int check = 1;
        //public static uint[][] textures = new uint[14][];

        public Home()
        {
            InitializeComponent();
            // Centers the form on the current screen
            openChildForm1(new Practice());
            CenterToScreen();
            
            //var timer = new Timer();
            //timer.Tick += GameLoop;
            //timer.Interval = 1000 / 60;
            //timer.Start();
            
        }

        private Form activeForm = null;

        private void openChildForm1(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            //childForm.BringToFront();
            childForm.Show();
        }

        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            childForm.Select();
        }

        private void buttonPractice_Click(object sender, EventArgs e)
        {
            //check = 1;
            panel1.Height = buttonPractice.Height;
            buttonSolve.ForeColor = Color.FromArgb(224, 224, 224);
            buttonLearn.ForeColor = Color.FromArgb(224, 224, 224);
            buttonTime.ForeColor = Color.FromArgb(224, 224, 224);

            panel1.BackColor = Color.FromArgb(255,175,95);
            buttonPractice.ForeColor = Color.FromArgb(255, 175, 95);
            panel1.Top = buttonPractice.Top;
            openChildForm(new Practice());
        }
        private void buttonTime_Click(object sender, EventArgs e)
        {
            //check = 2;
            panel1.Height = buttonTime.Height;
            buttonSolve.ForeColor = Color.FromArgb(224, 224, 224);
            buttonPractice.ForeColor = Color.FromArgb(224, 224, 224);
            buttonLearn.ForeColor = Color.FromArgb(224, 224, 224);

            buttonTime.ForeColor = Color.FromArgb(54, 199, 208);
            panel1.BackColor = Color.FromArgb(54, 199, 208);
            panel1.Top = buttonTime.Top;
            openChildForm(new Time());
        }
        private void buttonSolve_Click(object sender, EventArgs e)
        {
            panel1.Height = buttonSolve.Height;
            buttonLearn.ForeColor = Color.FromArgb(224, 224, 224);
            buttonPractice.ForeColor = Color.FromArgb(224, 224, 224);
            buttonTime.ForeColor = Color.FromArgb(224, 224, 224);

            buttonSolve.ForeColor = Color.FromArgb(74,207,172);
            panel1.BackColor = Color.FromArgb(74, 207, 172);
            panel1.Top = buttonSolve.Top;
            //check = 2;
            openChildForm(new Solve());
        }

        private void buttonLearn_Click(object sender, EventArgs e)
        {
            //check = 3;
            panel1.Height = buttonLearn.Height;
            buttonSolve.ForeColor = Color.FromArgb(224, 224, 224);
            buttonPractice.ForeColor = Color.FromArgb(224, 224, 224);
            buttonTime.ForeColor = Color.FromArgb(224, 224, 224);

            buttonLearn.ForeColor = Color.FromArgb(126, 140, 224);
            panel1.BackColor = Color.FromArgb(126, 140, 224);
            panel1.Top = buttonLearn.Top;
            openChildForm(new Learn());
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Help_Click(object sender, EventArgs e)
        {
            string message = Constants.language == 1 ? "Bạn có muốn chuyển ngôn ngữ qua tiếng Anh?" : "Do you want to change the language to Vietnamese?";
            string title = Constants.language == 1 ? "Chuyển ngôn ngữ" : "Change language";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Constants.language = Constants.language == 1 ? 0 : 1;
            }
            else
            { 
                // Do something  
            }
        }
    }
}