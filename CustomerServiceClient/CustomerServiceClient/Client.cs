using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public static Label lbTop,lbCenter;
		public static Button bt1,bt2,btOnoff;
		static bool inGate = false,online=true;
		static int customer;
		//public static string stringTop;
		public Client()
		{
			InitializeComponent();
			this.FormClosing += Client_FormClosing;
			Graphics g = this.CreateGraphics();
			var px = SystemInformation.VirtualScreen.Width / g.DpiX * 72;
			var py = SystemInformation.VirtualScreen.Height / g.DpiY * 72;
			this.Location = new Point(Convert.ToInt32(px) - 10, Convert.ToInt32(py) - 170);
			this.Size = new Size(310, 300);
			g.Dispose();
			//groupBox3.BackgroundImage = Properties.Resources.maunen1;

			
			bt1 = new Button();
			bt1.Font = new Font("Arial", 5);
			bt1.Text = "Nhận khách";
			bt1.Size = new Size(this.groupBox3.Height + 10, this.groupBox3.Height - 30);
			bt1.Location = new Point(20, 17);
			bt1.Click += new EventHandler(button1_Click);
			bt1.TabStop = false;
			this.groupBox3.Controls.Add(bt1);

			bt2 = new Button();
			bt2.Font = new Font("Arial", 5);
			bt2.Text = "Chuyển khách";
			bt2.Size = new Size(this.groupBox3.Height + 10, this.groupBox3.Height - 30);
			bt2.Location = new Point(bt1.Width+40, 17);
			bt2.Click += new EventHandler(button2_Click);
			bt2.TabStop = false;
			this.groupBox3.Controls.Add(bt2);


			btOnoff = new Button();				
			btOnoff.Size = new Size(this.groupBox3.Height +10, this.groupBox3.Height - 30);
			btOnoff.Location = new Point(this.groupBox3.Width-btOnoff.Width-20, 17);
			btOnoff.Click += new EventHandler(button3_Click);			
			btOnoff.BackgroundImage = Properties.Resources.off;
			btOnoff.FlatStyle = FlatStyle.Flat;
			btOnoff.BackgroundImage = Properties.Resources.off;
			btOnoff.BackgroundImageLayout = ImageLayout.Zoom;
			btOnoff.BackColor = Color.Transparent;
			btOnoff.FlatAppearance.BorderSize = 0;	
			
			this.groupBox3.Controls.Add(btOnoff);

			lbTop = new Label();
			lbTop.Size = new Size(this.Width, 20);
			lbTop.Location = new Point(3, 1);
			lbTop.TextAlign = ContentAlignment.MiddleLeft;
			lbTop.Text = "1000";
			lbTop.Font = new Font("Timesnewroman", 10, FontStyle.Bold);
			lbTop.ForeColor = Color.Blue;
			this.groupBox1.Controls.Add(lbTop);


			lbCenter = new Label();
			lbCenter.AutoSize = true;
			lbCenter.Location = new Point(13, 10);
			lbCenter.TextAlign = ContentAlignment.MiddleCenter;
			//lbCenter.Text = "1000";
			lbCenter.Font = new Font("Timesnewroman", 70, FontStyle.Bold);
			lbCenter.ForeColor = Color.Red;
			//lbCenter.Left = groupBox2.Left + (groupBox2.Width - lbCenter.Width) / 2;
			//lbCenter.Top = groupBox2.Top - 20;
			this.groupBox2.Controls.Add(lbCenter);
			

		}
		private void btOnoff_hover(object sender, EventArgs e)
		{
		
		}
		private void btOnoff_leave(object sender, EventArgs e)
		{			
		}
		private void Client_FormClosing(object sender, FormClosingEventArgs e)
		{
			SocketRun.SocketClose();
			System.Environment.Exit(1);
		}
		private void Client_Load(object sender, EventArgs e)
		{
			//button3.BackgroundImage = Properties.Resources.on;
			SocketRun.fm = this;
			SocketRun.SocketCreate();
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (inGate)
			{
				bt1.Text = "Nhận khách";
				inGate = false;
				if (SocketRun.android == 1)
				{
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("data");
			}
		}
		public static void processData(string st)
		{

			//try
			//{
			String[] arrRs = st.Split('|');
			if (arrRs[0] == "login")
			{
				lbTop.Text = arrRs[3] + " - Cổng số " + arrRs[4];
				if (SocketRun.android == 1)
				{
					SocketRun.gate = arrRs[4];
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "data")
			{
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = arrRs[5];
				inGate = true;
				if (SocketRun.android == 1)
				{
					Thread t = new Thread((obj) =>
					{						
						SocketRun.sendDataAndroid(SocketRun.gate + "," + arrRs[5]);
					});
					t.Start();


				}
			}
			else if (arrRs[0] == "call")
			{
				//MessageBox.Show(st);
				customer = Convert.ToInt32(arrRs[5]);
				//bt1.Text = arrRs[5];
				//inGate = true;
				if (SocketRun.android == 1)
				{
					Thread t = new Thread((obj) =>
					{
						Thread.Sleep(30000);
						SocketRun.sendDataAndroid(SocketRun.gate + "," + arrRs[5]);
					});
					t.Start();


				}
			}
			//}
			//catch { }

		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (inGate)
			{
				bt1.Text = "Nhận khách";
				inGate = false;
				if (SocketRun.android == 1)
				{
					SocketRun.sendDataAndroid(SocketRun.gate + ",0");
				}
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("data");
			}
		}
	
		private void button3_Click(object sender, EventArgs e)
		{
			if (online)
			{
				btOnoff.BackgroundImage = Properties.Resources.on;
				online = false;
				bt1.Visible = false;
				bt2.Visible = false;
			}
			else
			{
				btOnoff.BackgroundImage = Properties.Resources.off;
				online = true;
				bt1.Visible = true;
				bt2.Visible = true;
			}
		}
	}
}
