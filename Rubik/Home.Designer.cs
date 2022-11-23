namespace Rubik
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.Help = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonTime = new System.Windows.Forms.Button();
            this.buttonLearn = new System.Windows.Forms.Button();
            this.buttonSolve = new System.Windows.Forms.Button();
            this.buttonPractice = new System.Windows.Forms.Button();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelSideMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panelChildForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panelSideMenu.Controls.Add(this.Help);
            this.panelSideMenu.Controls.Add(this.panel1);
            this.panelSideMenu.Controls.Add(this.pictureBox2);
            this.panelSideMenu.Controls.Add(this.buttonExit);
            this.panelSideMenu.Controls.Add(this.buttonTime);
            this.panelSideMenu.Controls.Add(this.buttonLearn);
            this.panelSideMenu.Controls.Add(this.buttonSolve);
            this.panelSideMenu.Controls.Add(this.buttonPractice);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(300, 800);
            this.panelSideMenu.TabIndex = 1;
            // 
            // Help
            // 
            this.Help.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.Help.FlatAppearance.BorderSize = 0;
            this.Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Help.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Help.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Help.Location = new System.Drawing.Point(0, 768);
            this.Help.Margin = new System.Windows.Forms.Padding(2);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(51, 32);
            this.Help.TabIndex = 8;
            this.Help.Text = "?";
            this.Help.UseVisualStyleBackColor = false;
            this.Help.Click += new System.EventHandler(this.Help_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.panel1.Location = new System.Drawing.Point(2, 206);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(15, 80);
            this.panel1.TabIndex = 7;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(300, 200);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // buttonExit
            // 
            this.buttonExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.ForeColor = System.Drawing.Color.Green;
            this.buttonExit.Location = new System.Drawing.Point(19, 523);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(283, 80);
            this.buttonExit.TabIndex = 5;
            this.buttonExit.Text = "     Exit";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // buttonTime
            // 
            this.buttonTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonTime.FlatAppearance.BorderSize = 0;
            this.buttonTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTime.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonTime.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTime.Location = new System.Drawing.Point(19, 446);
            this.buttonTime.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTime.Name = "buttonTime";
            this.buttonTime.Size = new System.Drawing.Size(283, 80);
            this.buttonTime.TabIndex = 4;
            this.buttonTime.Text = "     Time";
            this.buttonTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonTime.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonTime.UseVisualStyleBackColor = false;
            this.buttonTime.Click += new System.EventHandler(this.buttonTime_Click);
            // 
            // buttonLearn
            // 
            this.buttonLearn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonLearn.FlatAppearance.BorderSize = 0;
            this.buttonLearn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLearn.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLearn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonLearn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLearn.Location = new System.Drawing.Point(19, 366);
            this.buttonLearn.Margin = new System.Windows.Forms.Padding(0);
            this.buttonLearn.Name = "buttonLearn";
            this.buttonLearn.Size = new System.Drawing.Size(283, 80);
            this.buttonLearn.TabIndex = 3;
            this.buttonLearn.Text = "     Learn";
            this.buttonLearn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLearn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonLearn.UseVisualStyleBackColor = false;
            this.buttonLearn.Click += new System.EventHandler(this.buttonLearn_Click);
            // 
            // buttonSolve
            // 
            this.buttonSolve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonSolve.FlatAppearance.BorderSize = 0;
            this.buttonSolve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSolve.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSolve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonSolve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSolve.Location = new System.Drawing.Point(19, 286);
            this.buttonSolve.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSolve.Name = "buttonSolve";
            this.buttonSolve.Size = new System.Drawing.Size(283, 80);
            this.buttonSolve.TabIndex = 2;
            this.buttonSolve.Text = "     Solve";
            this.buttonSolve.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSolve.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonSolve.UseVisualStyleBackColor = false;
            this.buttonSolve.Click += new System.EventHandler(this.buttonSolve_Click);
            // 
            // buttonPractice
            // 
            this.buttonPractice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.buttonPractice.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPractice.CausesValidation = false;
            this.buttonPractice.FlatAppearance.BorderSize = 0;
            this.buttonPractice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPractice.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPractice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonPractice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPractice.Location = new System.Drawing.Point(19, 206);
            this.buttonPractice.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPractice.Name = "buttonPractice";
            this.buttonPractice.Size = new System.Drawing.Size(283, 80);
            this.buttonPractice.TabIndex = 1;
            this.buttonPractice.Text = "     Practice";
            this.buttonPractice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonPractice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonPractice.UseVisualStyleBackColor = false;
            this.buttonPractice.Click += new System.EventHandler(this.buttonPractice_Click);
            // 
            // panelChildForm
            // 
            this.panelChildForm.Controls.Add(this.pictureBox1);
            this.panelChildForm.Location = new System.Drawing.Point(300, 0);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Size = new System.Drawing.Size(800, 800);
            this.panelChildForm.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, -14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(805, 814);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(1100, 800);
            this.Controls.Add(this.panelSideMenu);
            this.Controls.Add(this.panelChildForm);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "Home";
            this.Text = "Home";
            this.panelSideMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panelChildForm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Button buttonPractice;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonTime;
        private System.Windows.Forms.Button buttonLearn;
        private System.Windows.Forms.Button buttonSolve;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Help;
    }
}

