namespace PROG7312_POE_REDO
{
    partial class BookControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookControl));
            this.codeDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // codeDisplay
            // 
            this.codeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codeDisplay.Location = new System.Drawing.Point(82, 126);
            this.codeDisplay.Name = "codeDisplay";
            this.codeDisplay.Size = new System.Drawing.Size(144, 29);
            this.codeDisplay.TabIndex = 0;
            this.codeDisplay.Text = "Example data";
            // 
            // BookControl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.Controls.Add(this.codeDisplay);
            this.Name = "BookControl";
            this.Size = new System.Drawing.Size(238, 206);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BookControl_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label codeDisplay;
    }
}
