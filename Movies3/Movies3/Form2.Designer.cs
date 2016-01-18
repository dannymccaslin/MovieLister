namespace Movies3
{
    partial class Form2
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
            this.editButton2 = new System.Windows.Forms.Button();
            this.editMovieDate = new System.Windows.Forms.DateTimePicker();
            this.editMovieTitle = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // editButton2
            // 
            this.editButton2.Location = new System.Drawing.Point(161, 116);
            this.editButton2.Name = "editButton2";
            this.editButton2.Size = new System.Drawing.Size(75, 23);
            this.editButton2.TabIndex = 0;
            this.editButton2.Text = "Edit";
            this.editButton2.UseVisualStyleBackColor = true;
            this.editButton2.Click += new System.EventHandler(this.editButton2_Click);
            // 
            // editMovieDate
            // 
            this.editMovieDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.editMovieDate.Location = new System.Drawing.Point(36, 35);
            this.editMovieDate.Name = "editMovieDate";
            this.editMovieDate.Size = new System.Drawing.Size(200, 20);
            this.editMovieDate.TabIndex = 1;
            // 
            // editMovieTitle
            // 
            this.editMovieTitle.Location = new System.Drawing.Point(36, 75);
            this.editMovieTitle.Name = "editMovieTitle";
            this.editMovieTitle.Size = new System.Drawing.Size(200, 20);
            this.editMovieTitle.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 160);
            this.Controls.Add(this.editMovieTitle);
            this.Controls.Add(this.editMovieDate);
            this.Controls.Add(this.editButton2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button editButton2;
        private System.Windows.Forms.DateTimePicker editMovieDate;
        private System.Windows.Forms.TextBox editMovieTitle;
    }
}