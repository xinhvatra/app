using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	public partial class Main : Form
	{
		public static Button bt1;
		public static Button bt2;
		public static Button bt3;
		public static Button bt4;
		public const int MAX_CONNECTION = 2;
		public const int PORT_NUMBER = 9999;
		public static TcpListener listener;
		public static Dictionary<int, int> clients;
		public Main()
		{
			InitializeComponent();
			SocketRun.fm = this;
			//this.TopMost = true;


			bt1 = new Button();
			bt2 = new Button();
			bt3 = new Button();
			bt4 = new Button();

			bt1.Click += new EventHandler(bt1_click);
			bt1.MouseHover += new EventHandler(bt1_hover);
			bt1.MouseLeave += new EventHandler(bt1_leave);

			bt2.Click += new EventHandler(bt2_click);
			bt2.MouseHover += new EventHandler(bt2_hover);
			bt2.MouseLeave += new EventHandler(bt2_leave);

			bt1.Location = new Point(5, 5);
			bt1.Size = new Size(300, 300);
			bt1.BackgroundImage = CustomerService.Properties.Resources.guitien;
			bt1.BackgroundImageLayout = ImageLayout.Zoom;
			bt1.Text = "Tiền gửi";
			bt1.Font = new Font("Timesnewroman", 30, FontStyle.Italic);
			bt1.ForeColor = Color.Purple;
			bt1.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt1);

			bt2.Location = new Point(310, 5);
			bt2.Size = new Size(300, 300);
			bt2.BackgroundImage = CustomerService.Properties.Resources.dichvu;
			bt2.BackgroundImageLayout = ImageLayout.Zoom;
			bt2.Text = "Mở tài khoản";
			bt2.Font = new Font("Timesnewroman", 30, FontStyle.Italic);
			bt2.ForeColor = Color.Purple;
			bt2.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt2);

			bt3.Location = new Point(5, 310);
			bt3.Size = new Size(300, 300);
			this.Controls.Add(bt3);

			bt4.Location = new Point(310, 310);
			bt4.Size = new Size(300, 300);
			this.Controls.Add(bt4);
		}
		private void bt1_hover(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Blue;
		}
		private void bt1_leave(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Purple;
		}
		private void bt2_hover(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Blue;
		}
		private void bt2_leave(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Purple;
		}
		private void bt1_click(object sender, EventArgs e)
		{
			Function.fmName = 1;
			Layso layso = new Layso();

			layso.MdiParent = this;
			layso.Text = Function.service_customer.Rows[0][1] + "";

			layso.Dock = DockStyle.Fill;
			layso.ControlBox = false;
			layso.AutoSize = false;

			layso.Show();

			bt1.Hide();
			bt2.Hide();
			bt3.Hide();
			bt4.Hide();
			//this.Hide();
		}

		private void bt2_click(object sender, EventArgs e)
		{
			Function.fmName = 2;
			Layso layso = new Layso();

			layso.MdiParent = this;
			layso.Text = Function.service_customer.Rows[1][1] + "";

			layso.Dock = DockStyle.Fill;
			layso.ControlBox = false;
			layso.AutoSize = false;

			layso.Show();

			bt1.Hide();
			bt2.Hide();
			bt3.Hide();
			bt4.Hide();
		}

		private void Main_Load(object sender, EventArgs e)
		{


			SocketRun.SocketCreate();
			clients = new Dictionary<int, int>();
			Thread t = new Thread(sound);
			t.Start();
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "select * from services";
			MySqlCommand cm = new MySqlCommand(sql, conn);
			using (DbDataReader reader = cm.ExecuteReader())
			{
				if (reader.HasRows)
				{
					Function.service_customer = new DataTable();
					Function.service_customer.Load(reader);
				}
			}


		}
		//public static async void process(String st)
		//{			
		//	//await processData(st);
		//	var myTask = Task.Run(() => processData(st));
		//	await myTask;			
		//}
		public static bool isProcess = false;
		static Semaphore sm = new Semaphore(1, 1);
		public static void processData(string st)
		{

			//try
			{
				isProcess = true;

				//try
				//{
				String[] arrRs = st.Split('|');
				if (arrRs[0] == "login")
				{
					MySqlConnection conn = Function.GetConnection();
					conn.Open();
					string sql = "select * from client where id = " + arrRs[1];
					MySqlCommand cm = new MySqlCommand(sql, conn);
					using (DbDataReader reader = cm.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{
								SocketRun.sendData("login", (int)reader.GetValue(0), (int)reader.GetValue(1), (string)reader.GetValue(2), (int)reader.GetValue(3), 0);
							}
						}
					}
				}

				else if (arrRs[0] == "data")
				{
					//sm.WaitOne();
					//MessageBox.Show("xu ly tin nhan tu client: " + st);
					bool isAdd = false;
					int cus_id = 0;
					MySqlConnection conn = Function.GetConnection();
					conn.Open();
					string sql = "SELECT * FROM cus_wait AS c INNER JOIN `client` AS cc ON c.service_id=cc.service_id AND active=1 AND cc.id=" + arrRs[1] + " LIMIT 1";
					MySqlCommand cm = new MySqlCommand(sql, conn);
					using (DbDataReader reader = cm.ExecuteReader())
					{
						if (reader.HasRows)
						{
							while (reader.Read())
							{	
								clients.Add((int)reader.GetValue(0), (int)reader.GetValue(5));
								cus_id = (int)reader.GetValue(0);
								isAdd = true;
								//sound((int)reader.GetValue(0), (int)reader.GetValue(5));
								SocketRun.sendData("data", (int)reader.GetValue(0), (int)reader.GetValue(1), reader.GetValue(2).ToString(), (int)reader.GetValue(3), 0);
							}
							//MySqlConnection conn = Function.GetConnection();
							//conn.Open();							
						}
					}
					if (isAdd)
					{
						 sql = "Delete FROM cus_wait WHERE cus_id = " + cus_id;
						MySqlCommand cmd = new MySqlCommand(sql, conn);						
						cmd.ExecuteNonQuery();
						conn.Close();
					}
				}

				//}
				//catch
				//{
				//	//MessageBox.Show("catch");				
				//}
			}
			//finally
			//{
			//	mutex.ReleaseMutex();
			//}
		}
		public static void sound()
		{	while (true)
			{
				if (clients.Count > 0)
				{
					try
					{
						foreach (KeyValuePair<int, int> entry in clients)
						{
							//MessageBox.Show("chạy sound thông báo mời khách: "+ customer_id);
							//sm.WaitOne();
							int num = entry.Key;
							int ra = entry.Value;
							string donvi = num.ToString().Substring(3, 1);
							string chuc = num.ToString().Substring(2, 1);
							string tram = num.ToString().Substring(1, 1);
							string nghin = num.ToString().Substring(0, 1);

							//Random r = new Random();
							//int ra = r.Next(1, 10);

							WMPLib.WindowsMediaPlayer mp = new WMPLib.WindowsMediaPlayer();
							WMPLib.IWMPPlaylist playlist = mp.playlistCollection.newPlaylist("customerCall");
							WMPLib.IWMPMedia media, media1, media2, media3, media4, media5, media6;

							string moi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\moi.mp3";
							//mp.URL = moi;
							//mp.controls.play();			
							media = mp.newMedia(moi);

							//Thread.Sleep(1600);

							//WMPLib.WindowsMediaPlayer mp1 = new WMPLib.WindowsMediaPlayer();
							string sokhachnghin = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + nghin + ".mp3";
							//mp1.URL = sokhachnghin;
							//mp1.controls.play();
							//Thread.Sleep(700);
							media1 = mp.newMedia(sokhachnghin);

							//WMPLib.WindowsMediaPlayer mp11 = new WMPLib.WindowsMediaPlayer();
							string sokhachtram = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + tram + ".mp3";
							//mp11.URL = sokhachtram;
							//mp11.controls.play();
							//Thread.Sleep(700);
							media2 = mp.newMedia(sokhachtram);

							//WMPLib.WindowsMediaPlayer mp12 = new WMPLib.WindowsMediaPlayer();
							string sokhachchuc = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + chuc + ".mp3";
							//mp12.URL = sokhachchuc;
							//mp12.controls.play();
							//Thread.Sleep(700);
							media3 = mp.newMedia(sokhachchuc);

							//WMPLib.WindowsMediaPlayer mp13 = new WMPLib.WindowsMediaPlayer();
							string sokhachdonvi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + donvi + ".mp3";
							//mp13.URL = sokhachdonvi;
							//mp13.controls.play();
							//Thread.Sleep(700);
							media4 = mp.newMedia(sokhachdonvi);

							//WMPLib.WindowsMediaPlayer mp2 = new WMPLib.WindowsMediaPlayer();
							string vaocua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\cuaso.mp3";
							//mp2.URL = vaocua;
							//mp2.controls.play();
							//Thread.Sleep(1100);
							media5 = mp.newMedia(vaocua);

							//WMPLib.WindowsMediaPlayer mp3 = new WMPLib.WindowsMediaPlayer();
							string socua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + ra + ".mp3";
							media6 = mp.newMedia(socua);
							//mp3.URL = socua;
							//mp3.controls.play();
							//MessageBox.Show("ok done");

							playlist.appendItem(media);
							playlist.appendItem(media1);
							playlist.appendItem(media2);
							playlist.appendItem(media3);
							playlist.appendItem(media4);
							playlist.appendItem(media5);
							playlist.appendItem(media6);
							mp.currentPlaylist = playlist;
							mp.controls.play();
							clients.Remove(num);

						}
					}
					catch { }
				}
				Thread.Sleep(10000);
			}
		}
	}
}
