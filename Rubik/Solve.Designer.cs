namespace Rubik
{
    partial class Solve
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
            this.glControl1 = new OpenTK.GLControl();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.Previous = new System.Windows.Forms.Button();
            this.Auto = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.camera = new System.Windows.Forms.Button();
            this.reset = new System.Windows.Forms.Button();
            this.curstep = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nex = new System.Windows.Forms.Label();
            this.pre = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.Color.Black;
            this.glControl1.Location = new System.Drawing.Point(300, 117);
            this.glControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(600, 615);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glcontrol_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glcontrol_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glcontrol_KeyDown);
            this.glControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.glcontrol_KeyUp);
            this.glControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseClick);
            this.glControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glcontrol_MouseDown);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glcontrol_MouseMove);
            this.glControl1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glcontrol_MouseUp);
            // 
            // comboBox1
            // 
            this.comboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "Yellow",
            "White",
            "Red",
            "Orange"});
            this.comboBox1.Location = new System.Drawing.Point(522, 771);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 30);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(621, 1064);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 45);
            this.button1.TabIndex = 2;
            this.button1.Text = "Solve";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Next
            // 
            this.Next.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Next.Location = new System.Drawing.Point(778, 969);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(122, 45);
            this.Next.TabIndex = 3;
            this.Next.Text = "Next";
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Previous
            // 
            this.Previous.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Previous.Location = new System.Drawing.Point(300, 969);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(122, 45);
            this.Previous.TabIndex = 4;
            this.Previous.Text = "Back";
            this.Previous.UseVisualStyleBackColor = true;
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // Auto
            // 
            this.Auto.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Auto.Location = new System.Drawing.Point(463, 969);
            this.Auto.Name = "Auto";
            this.Auto.Size = new System.Drawing.Size(122, 45);
            this.Auto.TabIndex = 5;
            this.Auto.Text = "Play";
            this.Auto.UseVisualStyleBackColor = true;
            this.Auto.Click += new System.EventHandler(this.Auto_Click);
            // 
            // Stop
            // 
            this.Stop.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stop.Location = new System.Drawing.Point(621, 969);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(122, 45);
            this.Stop.TabIndex = 6;
            this.Stop.Text = "Pause";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(518, 747);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "Choose color to fill";
            // 
            // camera
            // 
            this.camera.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.camera.Location = new System.Drawing.Point(946, 755);
            this.camera.Name = "camera";
            this.camera.Size = new System.Drawing.Size(116, 46);
            this.camera.TabIndex = 8;
            this.camera.Text = "Camera";
            this.camera.UseVisualStyleBackColor = true;
            this.camera.Click += new System.EventHandler(this.camera_Click);
            // 
            // reset
            // 
            this.reset.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reset.Location = new System.Drawing.Point(463, 1064);
            this.reset.Name = "reset";
            this.reset.Size = new System.Drawing.Size(122, 45);
            this.reset.TabIndex = 9;
            this.reset.Text = "Reset";
            this.reset.UseVisualStyleBackColor = true;
            this.reset.Click += new System.EventHandler(this.reset_Click);
            // 
            // curstep
            // 
            this.curstep.AutoSize = true;
            this.curstep.Font = new System.Drawing.Font("Tahoma", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curstep.ForeColor = System.Drawing.Color.White;
            this.curstep.Location = new System.Drawing.Point(573, 49);
            this.curstep.Name = "curstep";
            this.curstep.Size = new System.Drawing.Size(75, 63);
            this.curstep.TabIndex = 10;
            this.curstep.Text = "...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(560, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "next step:";
            // 
            // nex
            // 
            this.nex.AutoSize = true;
            this.nex.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nex.ForeColor = System.Drawing.Color.White;
            this.nex.Location = new System.Drawing.Point(640, 65);
            this.nex.Name = "nex";
            this.nex.Size = new System.Drawing.Size(0, 43);
            this.nex.TabIndex = 12;
            // 
            // pre
            // 
            this.pre.AutoSize = true;
            this.pre.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pre.ForeColor = System.Drawing.Color.White;
            this.pre.Location = new System.Drawing.Point(481, 65);
            this.pre.Name = "pre";
            this.pre.Size = new System.Drawing.Size(0, 43);
            this.pre.TabIndex = 13;
            // 
            // Solve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1176, 1171);
            this.Controls.Add(this.pre);
            this.Controls.Add(this.nex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.curstep);
            this.Controls.Add(this.reset);
            this.Controls.Add(this.camera);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Auto);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.glControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Solve";
            this.Text = "Solve";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button Previous;
        private System.Windows.Forms.Button Auto;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button camera;
        private System.Windows.Forms.Button reset;
        private System.Windows.Forms.Label curstep;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label nex;
        private System.Windows.Forms.Label pre;
    }
}