namespace Rubik
{
    partial class Practice
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
            this.glControl = new OpenTK.GLControl();
            this.buttonL = new System.Windows.Forms.Button();
            this.buttonR = new System.Windows.Forms.Button();
            this.buttonU = new System.Windows.Forms.Button();
            this.buttonD = new System.Windows.Forms.Button();
            this.buttonF = new System.Windows.Forms.Button();
            this.buttonB = new System.Windows.Forms.Button();
            this.buttonLp = new System.Windows.Forms.Button();
            this.buttonRp = new System.Windows.Forms.Button();
            this.buttonUp = new System.Windows.Forms.Button();
            this.buttonDp = new System.Windows.Forms.Button();
            this.buttonFp = new System.Windows.Forms.Button();
            this.buttonBp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            this.Scramble = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.glControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.glControl.Location = new System.Drawing.Point(300, 31);
            this.glControl.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(600, 615);
            this.glControl.TabIndex = 0;
            this.glControl.VSync = false;
            this.glControl.Load += new System.EventHandler(this.glControl_Load);
            this.glControl.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl_Paint);
            this.glControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyDown);
            this.glControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glControl_KeyUp);
            this.glControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseDown);
            this.glControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseMove);
            this.glControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControl_MouseUp);
            // 
            // buttonL
            // 
            this.buttonL.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonL.Location = new System.Drawing.Point(239, 744);
            this.buttonL.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonL.Name = "buttonL";
            this.buttonL.Size = new System.Drawing.Size(112, 35);
            this.buttonL.TabIndex = 1;
            this.buttonL.Text = "L";
            this.buttonL.UseVisualStyleBackColor = true;
            this.buttonL.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonR
            // 
            this.buttonR.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonR.Location = new System.Drawing.Point(359, 744);
            this.buttonR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonR.Name = "buttonR";
            this.buttonR.Size = new System.Drawing.Size(112, 35);
            this.buttonR.TabIndex = 2;
            this.buttonR.Text = "R";
            this.buttonR.UseVisualStyleBackColor = true;
            this.buttonR.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonU
            // 
            this.buttonU.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonU.Location = new System.Drawing.Point(479, 744);
            this.buttonU.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonU.Name = "buttonU";
            this.buttonU.Size = new System.Drawing.Size(112, 35);
            this.buttonU.TabIndex = 3;
            this.buttonU.Text = "U";
            this.buttonU.UseVisualStyleBackColor = true;
            this.buttonU.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonD
            // 
            this.buttonD.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonD.Location = new System.Drawing.Point(599, 744);
            this.buttonD.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonD.Name = "buttonD";
            this.buttonD.Size = new System.Drawing.Size(112, 35);
            this.buttonD.TabIndex = 4;
            this.buttonD.Text = "D";
            this.buttonD.UseVisualStyleBackColor = true;
            this.buttonD.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonF
            // 
            this.buttonF.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonF.Location = new System.Drawing.Point(719, 744);
            this.buttonF.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonF.Name = "buttonF";
            this.buttonF.Size = new System.Drawing.Size(112, 35);
            this.buttonF.TabIndex = 5;
            this.buttonF.Text = "F";
            this.buttonF.UseVisualStyleBackColor = true;
            this.buttonF.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonB
            // 
            this.buttonB.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonB.Location = new System.Drawing.Point(839, 744);
            this.buttonB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonB.Name = "buttonB";
            this.buttonB.Size = new System.Drawing.Size(112, 35);
            this.buttonB.TabIndex = 6;
            this.buttonB.Text = "B";
            this.buttonB.UseVisualStyleBackColor = true;
            this.buttonB.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonLp
            // 
            this.buttonLp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLp.Location = new System.Drawing.Point(239, 789);
            this.buttonLp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonLp.Name = "buttonLp";
            this.buttonLp.Size = new System.Drawing.Size(112, 35);
            this.buttonLp.TabIndex = 1;
            this.buttonLp.Text = "L\'";
            this.buttonLp.UseVisualStyleBackColor = true;
            this.buttonLp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonRp
            // 
            this.buttonRp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRp.Location = new System.Drawing.Point(359, 789);
            this.buttonRp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonRp.Name = "buttonRp";
            this.buttonRp.Size = new System.Drawing.Size(112, 35);
            this.buttonRp.TabIndex = 2;
            this.buttonRp.Text = "R\'";
            this.buttonRp.UseVisualStyleBackColor = true;
            this.buttonRp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonUp
            // 
            this.buttonUp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonUp.Location = new System.Drawing.Point(479, 788);
            this.buttonUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonUp.Name = "buttonUp";
            this.buttonUp.Size = new System.Drawing.Size(112, 35);
            this.buttonUp.TabIndex = 3;
            this.buttonUp.Text = "U\'";
            this.buttonUp.UseVisualStyleBackColor = true;
            this.buttonUp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonDp
            // 
            this.buttonDp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDp.Location = new System.Drawing.Point(599, 789);
            this.buttonDp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDp.Name = "buttonDp";
            this.buttonDp.Size = new System.Drawing.Size(112, 35);
            this.buttonDp.TabIndex = 4;
            this.buttonDp.Text = "D\'";
            this.buttonDp.UseVisualStyleBackColor = true;
            this.buttonDp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonFp
            // 
            this.buttonFp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFp.Location = new System.Drawing.Point(719, 789);
            this.buttonFp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonFp.Name = "buttonFp";
            this.buttonFp.Size = new System.Drawing.Size(112, 35);
            this.buttonFp.TabIndex = 5;
            this.buttonFp.Text = "F\'";
            this.buttonFp.UseVisualStyleBackColor = true;
            this.buttonFp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonBp
            // 
            this.buttonBp.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBp.Location = new System.Drawing.Point(839, 789);
            this.buttonBp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonBp.Name = "buttonBp";
            this.buttonBp.Size = new System.Drawing.Size(112, 35);
            this.buttonBp.TabIndex = 6;
            this.buttonBp.Text = "B\'";
            this.buttonBp.UseVisualStyleBackColor = true;
            this.buttonBp.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(685, 979);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 49);
            this.button1.TabIndex = 7;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reset
            // 
            this.Reset.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reset.Location = new System.Drawing.Point(537, 979);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(110, 49);
            this.Reset.TabIndex = 10;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Scramble
            // 
            this.Scramble.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Scramble.Location = new System.Drawing.Point(389, 979);
            this.Scramble.Name = "Scramble";
            this.Scramble.Size = new System.Drawing.Size(110, 49);
            this.Scramble.TabIndex = 11;
            this.Scramble.Text = "Scramble";
            this.Scramble.UseVisualStyleBackColor = true;
            this.Scramble.Click += new System.EventHandler(this.Scramble_Click);
            // 
            // Practice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1176, 1171);
            this.Controls.Add(this.Scramble);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonBp);
            this.Controls.Add(this.buttonFp);
            this.Controls.Add(this.buttonB);
            this.Controls.Add(this.buttonDp);
            this.Controls.Add(this.buttonF);
            this.Controls.Add(this.buttonUp);
            this.Controls.Add(this.buttonD);
            this.Controls.Add(this.buttonRp);
            this.Controls.Add(this.buttonU);
            this.Controls.Add(this.buttonLp);
            this.Controls.Add(this.buttonR);
            this.Controls.Add(this.buttonL);
            this.Controls.Add(this.glControl);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Practice";
            this.Text = "Practice";
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glControl;
        private System.Windows.Forms.Button buttonL;
        private System.Windows.Forms.Button buttonR;
        private System.Windows.Forms.Button buttonU;
        private System.Windows.Forms.Button buttonD;
        private System.Windows.Forms.Button buttonF;
        private System.Windows.Forms.Button buttonB;
        private System.Windows.Forms.Button buttonLp;
        private System.Windows.Forms.Button buttonRp;
        private System.Windows.Forms.Button buttonUp;
        private System.Windows.Forms.Button buttonDp;
        private System.Windows.Forms.Button buttonFp;
        private System.Windows.Forms.Button buttonBp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.Button Scramble;
    }
}