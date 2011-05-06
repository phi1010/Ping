using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace Ping
{
	public partial class Form1 : Form
	{
		string URL;

		bool running = false;
		bool? laststate = null;
		//List<int> states = new List<int>();
		public Form1()
		{
			InitializeComponent();
			URL = "www.google.com";
		}
		public Form1(string url)
		{
			InitializeComponent();
			URL = url;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			textBox1.Text = URL;
			UpdateIcon(null);
		}

		public bool? CheckInternetConnection()
		{
			System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

			try
			{
				PingReply reply = ping.Send(URL, 250);
				return reply.Status == IPStatus.Success;
			}
			catch
			{
				return null;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (running == false)
			{
				running = true;
				bool? state = CheckInternetConnection();

				UpdateIcon(state);
				laststate = state;
				running = false;
			}
		}

		private void UpdateIcon(bool? state)
		{
			Icon icon;

			if (laststate == null)
			{
				if (state == null)
					icon = Properties.Resources.x;
				else if (state == true)
					icon = Properties.Resources._1;
				else//false
					icon = Properties.Resources._0;
			}
			else if (laststate == true)
			{
				if (state == null)
					icon = Properties.Resources._1;
				else if (state == true)
					icon = Properties.Resources._1;
				else//false
					icon = Properties.Resources._10;
			}
			else //false
			{
				if (state == null)
					icon = Properties.Resources._0;
				else if (state == true)
					icon = Properties.Resources._10;
				else//false
					icon = Properties.Resources._0;
			}

			//pictureBox1.BackgroundImage = icon.ToBitmap();
			Icon = icon;
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			MaximumSize = new Size(0, 0);
			MinimumSize = new Size(0, 0);
			URL = textBox1.Text;
			Text = URL;
			Width = TextRenderer.MeasureText(Text, Font).Width + 150;
			MaximumSize = Size;
			MinimumSize = Size;
		}
	}
}
