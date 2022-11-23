namespace Rubik
{
    partial class Time
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
            this.Timing = new System.Windows.Forms.Label();
            this.TextGuide = new System.Windows.Forms.Label();
            this.scrambleMovesLabel = new System.Windows.Forms.Label();
            this.message = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Timing
            // 
            this.Timing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Timing.AutoSize = true;
            this.Timing.Font = new System.Drawing.Font("Microsoft Sans Serif", 70F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Timing.Location = new System.Drawing.Point(400, 200);
            this.Timing.Name = "Timing";
            this.Timing.Size = new System.Drawing.Size(0, 107);
            this.Timing.TabIndex = 0;
            this.Timing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextGuide
            // 
            this.TextGuide.AutoSize = true;
            this.TextGuide.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGuide.Location = new System.Drawing.Point(400, 500);
            this.TextGuide.Name = "TextGuide";
            this.TextGuide.Size = new System.Drawing.Size(0, 91);
            this.TextGuide.TabIndex = 1;
            // 
            // scrambleMovesLabel
            // 
            this.scrambleMovesLabel.AllowDrop = true;
            this.scrambleMovesLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.scrambleMovesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scrambleMovesLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scrambleMovesLabel.Location = new System.Drawing.Point(12, 51);
            this.scrambleMovesLabel.Name = "scrambleMovesLabel";
            this.scrambleMovesLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scrambleMovesLabel.Size = new System.Drawing.Size(760, 134);
            this.scrambleMovesLabel.TabIndex = 2;
            this.scrambleMovesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // message
            // 
            this.message.AllowDrop = true;
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.message.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.message.Location = new System.Drawing.Point(21, 628);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(751, 58);
            this.message.TabIndex = 5;
            this.message.Text = "PRESS SPACE TO START INSPECT";
            this.message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.AllowDrop = true;
            this.timer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.timer.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timer.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.timer.Location = new System.Drawing.Point(12, 272);
            this.timer.Name = "timer";
            this.timer.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.timer.Size = new System.Drawing.Size(760, 134);
            this.timer.TabIndex = 6;
            this.timer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Time
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(784, 761);
            this.Controls.Add(this.timer);
            this.Controls.Add(this.message);
            this.Controls.Add(this.scrambleMovesLabel);
            this.Controls.Add(this.TextGuide);
            this.Controls.Add(this.Timing);
            this.KeyPreview = true;
            this.Name = "Time";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Time_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Time_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Timing;
        private System.Windows.Forms.Label TextGuide;
        private System.Windows.Forms.Label scrambleMovesLabel;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.Label timer;
    }
}