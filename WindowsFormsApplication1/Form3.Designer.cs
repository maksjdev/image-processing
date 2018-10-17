namespace WindowsFormsApplication1
{
    partial class Form3
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
			this.richTextBoxForm3 = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// richTextBoxForm3
			// 
			this.richTextBoxForm3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBoxForm3.Location = new System.Drawing.Point(0, 0);
			this.richTextBoxForm3.Margin = new System.Windows.Forms.Padding(2);
			this.richTextBoxForm3.Name = "richTextBoxForm3";
			this.richTextBoxForm3.Size = new System.Drawing.Size(857, 753);
			this.richTextBoxForm3.TabIndex = 0;
			this.richTextBoxForm3.Text = "";
			this.richTextBoxForm3.TextChanged += new System.EventHandler(this.richTextBoxForm3_TextChanged);
			// 
			// Form3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(3F, 6F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(857, 753);
			this.Controls.Add(this.richTextBoxForm3);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 3F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "Form3";
			this.Text = "Полученый BitArray";
			this.Load += new System.EventHandler(this.Form3_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxForm3;
    }
}