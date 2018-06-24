using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SabrineClient
{
	public partial class Form1 : Form
	{
		private Func<string, int> callback;

		public Form1(Func<string,int> callback)
		{
			InitializeComponent();
			this.callback = callback;
		}

		private void Stend_Click(object sender, EventArgs e)
		{
			callback(tMessage.Text);
			tMessage.Text = "";
		}

		private void tMessage_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) { callback(tMessage.Text); tMessage.Text = ""; }
		}
	}
}
