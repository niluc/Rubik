namespace Rubik
{
    partial class Learn
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
            this.stepComboBox = new System.Windows.Forms.ComboBox();
            this.pictureTestCase = new System.Windows.Forms.PictureBox();
            this.lableGuide = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.pictureMain = new System.Windows.Forms.PictureBox();
            this.labelHint = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureTestCase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl
            // 
            this.glControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(12)))), ((int)(((byte)(12)))));
            this.glControl.ForeColor = System.Drawing.SystemColors.ControlText;
            this.glControl.Location = new System.Drawing.Point(200, 300);
            this.glControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.glControl.Name = "glControl";
            this.glControl.Size = new System.Drawing.Size(400, 400);
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
            // stepComboBox
            // 
            this.stepComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.stepComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.stepComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.stepComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.stepComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.stepComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.stepComboBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.stepComboBox.FormattingEnabled = true;
            this.stepComboBox.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.stepComboBox.Items.AddRange(new object[] {
            "White Cross",
            "White Face",
            "Second Layer Edges",
            "Yellow Cross",
            "Yellow Apply Edges",
            "Yellow Apply Corners",
            "Yellow Corner Orient"});
            this.stepComboBox.Location = new System.Drawing.Point(31, 27);
            this.stepComboBox.Name = "stepComboBox";
            this.stepComboBox.Size = new System.Drawing.Size(277, 26);
            this.stepComboBox.TabIndex = 1;
            this.stepComboBox.SelectionChangeCommitted += new System.EventHandler(this.stepComboBox_SelectionChangeCommitted);
            // 
            // pictureTestCase
            // 
            this.pictureTestCase.Location = new System.Drawing.Point(561, 52);
            this.pictureTestCase.Name = "pictureTestCase";
            this.pictureTestCase.Size = new System.Drawing.Size(150, 150);
            this.pictureTestCase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureTestCase.TabIndex = 2;
            this.pictureTestCase.TabStop = false;
            // 
            // lableGuide
            // 
            this.lableGuide.AutoSize = true;
            this.lableGuide.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lableGuide.ForeColor = System.Drawing.SystemColors.Control;
            this.lableGuide.Location = new System.Drawing.Point(38, 80);
            this.lableGuide.Name = "lableGuide";
            this.lableGuide.Size = new System.Drawing.Size(493, 31);
            this.lableGuide.TabIndex = 3;
            this.lableGuide.Text = "Try to do this without going to next page";
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.buttonNext.Location = new System.Drawing.Point(662, 696);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(73, 32);
            this.buttonNext.TabIndex = 4;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Arial Narrow", 9F, System.Drawing.FontStyle.Bold);
            this.buttonBack.Location = new System.Drawing.Point(44, 696);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(73, 32);
            this.buttonBack.TabIndex = 5;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Visible = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // pictureMain
            // 
            this.pictureMain.Location = new System.Drawing.Point(114, 80);
            this.pictureMain.Name = "pictureMain";
            this.pictureMain.Size = new System.Drawing.Size(550, 600);
            this.pictureMain.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureMain.TabIndex = 6;
            this.pictureMain.TabStop = false;
            // 
            // labelHint
            // 
            this.labelHint.AutoSize = true;
            this.labelHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHint.ForeColor = System.Drawing.SystemColors.Control;
            this.labelHint.Location = new System.Drawing.Point(38, 133);
            this.labelHint.Name = "labelHint";
            this.labelHint.Size = new System.Drawing.Size(0, 26);
            this.labelHint.TabIndex = 7;
            // 
            // Learn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.labelHint);
            this.Controls.Add(this.pictureMain);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.lableGuide);
            this.Controls.Add(this.pictureTestCase);
            this.Controls.Add(this.stepComboBox);
            this.Controls.Add(this.glControl);
            this.Name = "Learn";
            this.Text = "Learn";
            ((System.ComponentModel.ISupportInitialize)(this.pictureTestCase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private OpenTK.GLControl glControl;
        #endregion

        private System.Windows.Forms.ComboBox stepComboBox;
        private System.Windows.Forms.PictureBox pictureTestCase;
        private System.Windows.Forms.Label lableGuide;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.PictureBox pictureMain;
        private System.Windows.Forms.Label labelHint;
    }
}