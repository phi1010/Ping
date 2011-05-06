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
		readonly int Count = 5;
		readonly int PauseTime = 100;
		readonly string URL;

		bool running = false;
		List<int> states = new List<int>();
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
			Text = URL;
			Width = TextRenderer.MeasureText(Text, Font).Width + 150;
			MaximumSize = Size;
			UpdateIcon();
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

				UpdateIcon();
				running = false;
			}
		}

		private void UpdateIcon()
		{
			Icon icon = null;
			Icon icon0 = Properties.Resources._0;// X
			Icon icon1 = Properties.Resources._1;// !
			Icon icon2 = Properties.Resources._2;// ?
			Icon icon3 = Properties.Resources._3;// i
			Icon icon4 = Properties.Resources._4;// +



			//pictureBox1.BackgroundImage = icon.ToBitmap();
			Icon = icon;
		}
	}
}
