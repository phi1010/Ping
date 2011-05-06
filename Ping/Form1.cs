using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Threading;

namespace Ping
{
	public partial class Form1 : Form
	{
		const int Count = 10;
		string URL;
		const int PauseTime = 50;
		const int ValueSuccess = 8;
		const int ValueError = 1;
		const int Timeout = 250;
		Thread thread;

		bool running = true;
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
			textBox1.Text = URL;
			thread = new Thread(Run);
			thread.Start();
		}

		public int CheckInternetConnection()
		{
			System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

			try
			{
				PingReply reply = ping.Send(URL, Timeout);
				return reply.Status == IPStatus.Success ? ValueSuccess : 0;
			}
			catch (Exception e)
			{
				return ValueError;
			}
		}

		private void Run()
		{
			while (running)
			{
				states.Add(CheckInternetConnection());
				if (states.Count > Count)
					states.RemoveAt(0);
				UpdateIcon();
				Thread.Sleep(PauseTime);
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
			if (states.Count == 0)
			{
				icon = icon2;
			}
			else
			{
				double percent = states.Sum() * 1d / (states.Count * ValueSuccess);
				if (percent < 0.20)
					icon = icon0;
				else if (percent < 0.40)
					icon = icon1;
				else if (percent < 0.60)
					icon = icon2;
				else if (percent < 0.80)
					icon = icon3;
				else
					icon = icon4;
			}

			//pictureBox1.BackgroundImage = icon.ToBitmap();
			if (running)
				try
				{
					Invoke(new Action(() => { Icon = icon; }));
				}
				catch { }
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			running = false;
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
