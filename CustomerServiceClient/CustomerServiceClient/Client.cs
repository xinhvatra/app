using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerServiceClient
{
	public partial class Client : Form
	{
		public static Label lbTop, lbCenter;
		public static Button bt1, bt2, btOnoff;
		public static String[] dt_service;
		static bool inGate = false, online = false, inforce = false;
		public static int customer, force_customer;
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
			bt1.Font = new Font("Timesnewroman", 8, FontStyle.Bold);
			bt1.Text = "Nhận khách";
			bt1.Size = new Size(this.groupBox3.Height + 15, this.groupBox3.Height - 30);
			bt1.Location = new Point(15, 17);
			bt1.Click += new EventHandler(button1_Click);
			bt1.TabStop = false;
			bt1.Visible = false;
			this.groupBox3.Controls.Add(bt1);


			bt2 = new Button();
			bt2.Font = new Font("Timesnewroman", 8, FontStyle.Bold);
			bt2.Text = "Chuyển khách";
			bt2.Size = new Size(this.groupBox3.Height + 15, this.groupBox3.Height - 30);
			bt2.Location = new Point(bt1.Width + 30, 17);
			bt2.Click += new EventHandler(button2_Click);
			bt2.TabStop = false;
			bt2.Visible = false;
			this.groupBox3.Controls.Add(bt2);


			btOnoff = new Button();
			btOnoff.Size = new Size(this.groupBox3.Height - 10, this.groupBox3.Height - 30);
			btOnoff.Location = new Point(this.groupBox3.Width - btOnoff.Width - 15, 17);
			btOnoff.Click += new EventHandler(button3_Click);
			btOnoff.BackgroundImage = Properties.Resources.on;
			btOnoff.FlatStyle = FlatStyle.Flat;
			btOnoff.BackgroundImageLayout = ImageLayout.Zoom;
			btOnoff.BackColor = Color.Transparent;
			btOnoff.FlatAppearance.BorderSize = 0;

			this.groupBox3.Controls.Add(btOnoff);

			lbTop = new Label();
			lbTop.Size = new Size(this.Width, 20);
			lbTop.Location = new Point(3, 1);
			lbTop.TextAlign = ContentAlignment.MiddleLeft;
			lbTop.Text = "AGRIBANK TỈNH THÁI NGUYÊN";
			lbTop.Font = new Font("Timesnewroman", 10, FontStyle.Bold);
			lbTop.ForeColor = Color.Green;
			this.groupBox1.Controls.Add(lbTop);


			lbCenter = new Label();
			lbCenter.AutoSize = true;
			lbCenter.Location = new Point(13, 10);
			lbCenter.TextAlign = ContentAlignment.MiddleCenter;
			lbCenter.Text = "";
			lbCenter.Font = new Font("Timesnewroman", 70, FontStyle.Bold);
			lbCenter.ForeColor = Color.Red;
			//lbCenter.Left = groupBox2.Left + (groupBox2.Width - lbCenter.Width) / 2;
			//lbCenter.Top = groupBox2.Top - 20;
			this.groupBox2.Controls.Add(lbCenter);
			online = true;

		}
		private void Client_FormClosing(object sender, FormClosingEventArgs e)
		{
			SocketRun.connect();
			SocketRun.sendData("logout", 0);
			SocketRun.SocketClose();
			if (SocketRun.android == 1)

			{
				SocketRun.androidConnect(SocketRun.gate + ",0");
			}
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
			if (inforce)
			{
				SocketRun.connect();
				SocketRun.sendData("forcecustomer", force_customer);
			}
			else if (inGate)
			{
				SocketRun.connect();
				SocketRun.sendData("idle", 0);
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("data", 0);
			}
		}
		public static async void processDataAsync(string st)
		{

			//try
			//{
			String[] arrRs = st.Split('|');
			if (arrRs[0] == "login")
			{
				SocketRun.client_id = Int32.Parse(arrRs[1]);
				lbTop.Text = arrRs[3] + " - Cổng số " + arrRs[4];
				bt1.Visible = true;
				lbCenter.Text = "";
				dt_service = new string[arrRs.Length - 6];
				for (int i = 6; i < arrRs.Length; i++)
				{
					dt_service[i - 6] = arrRs[i];
				}

				btOnoff.BackgroundImage = Properties.Resources.off;
				if (SocketRun.android == 1)
				{
					SocketRun.gate = arrRs[4];
					SocketRun.androidConnect(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "data")
			{
				changeColor = false;
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = arrRs[5];
				inGate = true;
				inforce = false;
				bt1.Text = "Xong việc";
				bt1.BackColor = Color.Red;
				bt1.ForeColor = Color.White;
				bt2.Visible = true;
				btOnoff.Visible = false;
				if (SocketRun.android == 1)
				{
					SocketRun.androidConnect(SocketRun.gate + "," + arrRs[5]);

				}
			}
			else if (arrRs[0] == "idle")
			{
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = "";
				bt1.ForeColor = Color.Black;
				inGate = false;
				online = true;
				bt1.Text = "Nhận khách";
				bt1.BackColor = Color.Empty;
				bt1.Visible = true;
				btOnoff.Visible = true;
				btOnoff.BackgroundImage = Properties.Resources.off;
				if (SocketRun.android == 1)
				{
					SocketRun.androidConnect(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "notidle")
			{
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = arrRs[5];
				bt1.ForeColor = Color.Black;
				online = false;
				bt1.Visible = false;
				bt2.Visible = false;
				btOnoff.BackgroundImage = Properties.Resources.on;
				if (SocketRun.android == 1)
				{
					SocketRun.androidConnect(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "switch")
			{
				Switch.dtgrid.DataSource = null;
				try
				{
					Switch.dtgrid.Columns.RemoveAt(0);
				}
				catch (Exception)
				{

				}
				Switch.data = new DataTable();
				Switch.data.Columns.Add("Mã GDV");
				Switch.data.Columns.Add("Giao dịch viên");
				Switch.data.Columns.Add("Cửa số");
				Switch.data.Columns.Add("Tình trạng");


				for (int i = 6; i < arrRs.Length; i++)
				{
					String[] arr = arrRs[i].Split('_');
					//MessageBox.Show(arr[1].ToString().Trim());
					if (arr[3].ToString().Trim().Equals("True"))
					{
						arr[3] = "Rảnh";
					}
					else
					{
						arr[3] = "Bận";
					}

					Switch.data.Rows.Add(arr);
				}
				Switch.dtgrid.DataSource = Switch.data;

				//MessageBox.Show(Switch.dtgrid.Rows.Count+"");
				foreach (DataGridViewRow dgvr in Switch.dtgrid.Rows)
				{
					//MessageBox.Show(dgvr.Cells[1].Value+"");
					string vl = dgvr.Cells[3].Value + "";
					if (vl.Equals("Bận"))
					{
						dgvr.DefaultCellStyle.ForeColor = Color.Red;
					}
					else
					{
						dgvr.DefaultCellStyle.ForeColor = Color.Blue;
					}
				}
				DataGridViewCheckBoxColumn check = new DataGridViewCheckBoxColumn();
				check.HeaderText = "Chọn";
				Switch.dtgrid.Columns.Add(check);
				Switch.dtgrid.Columns[0].Visible = false;
			}
			else if (arrRs[0] == "pass")
			{
				MessageBox.Show("Bạn đã chuyển tiếp khách hàng số " + customer + " cho GVD " + Switch.gdv + " tại cửa số " + Switch.gate);
				swi.Close();
				customer = Convert.ToInt32(arrRs[5]);
				lbCenter.Text = "";
				bt1.ForeColor = Color.Black;
				inGate = false;
				bt1.Text = "Nhận khách";
				bt1.BackColor = Color.Empty;
				bt2.Visible = false;
				if (SocketRun.android == 1)
				{
					SocketRun.androidConnect(SocketRun.gate + ",0");
				}
			}
			else if (arrRs[0] == "forcecustomer")
			{
				MessageBox.Show(new Form { TopMost = true }, "Bạn có khách hàng chuyển tiếp từ GVD " + arrRs[1], "Chuyển tiếp khách hàng!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				inforce = true;
				bt1.Text = "Nhận khách chuyển tiếp";
				force_customer = Int32.Parse(arrRs[2]);
				changeColor = true;
				await ChangeColor(bt1);
				if (SocketRun.android == 1)
				{
					SocketRun.androidConnect(SocketRun.gate + "," + arrRs[2]);

				}
			}

			//}
			//catch { }

		}
		static bool changeColor = false;
		private static async Task ChangeColor(Button bt)
		{
			while (changeColor)
			{
				bt.BackColor = Color.Red;
				bt.ForeColor = Color.White;
				await Task.Delay(TimeSpan.FromSeconds(1));

				bt.BackColor = Color.Orange;
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}

		public static Switch swi;
		private void button2_Click(object sender, EventArgs e)
		{
			swi = new Switch();
			//swi.Parent = this;
			swi.Show();

		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (online)
			{
				SocketRun.connect();
				SocketRun.sendData("notidle", 0);
				//btOnoff.BackgroundImage = Properties.Resources.on;
				//online = false;
				//bt1.Visible = false;
				//bt2.Visible = false;
			}
			else
			{
				SocketRun.connect();
				SocketRun.sendData("idle", 0);
				//btOnoff.BackgroundImage = Properties.Resources.off;
				//online = true;
				//bt1.Visible = true;
			}
		}
	}
}
