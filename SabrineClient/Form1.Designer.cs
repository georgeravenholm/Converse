namespace SabrineClient
{
	partial class Form1
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
            this.tMessage = new System.Windows.Forms.TextBox();
            this.Stend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tMessage
            // 
            this.tMessage.Location = new System.Drawing.Point(13, 6);
            this.tMessage.Name = "tMessage";
            this.tMessage.Size = new System.Drawing.Size(283, 20);
            this.tMessage.TabIndex = 0;
            this.tMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tMessage_KeyDown);
            // 
            // Stend
            // 
            this.Stend.Location = new System.Drawing.Point(303, 6);
            this.Stend.Name = "Stend";
            this.Stend.Size = new System.Drawing.Size(81, 23);
            this.Stend.TabIndex = 1;
            this.Stend.Text = "Stend";
            this.Stend.UseVisualStyleBackColor = true;
            this.Stend.Click += new System.EventHandler(this.Stend_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 38);
            this.ControlBox = false;
            this.Controls.Add(this.Stend);
            this.Controls.Add(this.tMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TrapChat";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox tMessage;
		private System.Windows.Forms.Button Stend;
	}
}