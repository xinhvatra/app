using Microsoft.Office.Interop.Word;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DataTable = System.Data.DataTable;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;

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
		public static String questionList;
		public Main()
		{
			InitializeComponent();
			SocketRun.fm = this;
			this.TopMost = true;

			this.Size = new Size(1280, 1024);
			//this.Location = new System.Drawing.Point(-10, 0);
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

			bt1.Location = new System.Drawing.Point(3, 30);
			bt1.Size = new Size(625, 510);
			bt1.BackgroundImage = CustomerService.Properties.Resources.guitien;
			bt1.BackgroundImageLayout = ImageLayout.Zoom;
			bt1.Text = "Tiền gửi";
			bt1.Font = new Font("Timesnewroman", 30, FontStyle.Italic);
			bt1.ForeColor = Color.Blue;
			bt1.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt1);

			bt2.Location = new Point(635, 30);
			bt2.Size = new Size(625, 510);
			bt2.BackgroundImage = CustomerService.Properties.Resources.dichvu1;
			bt2.BackgroundImageLayout = ImageLayout.Zoom;
			bt2.Text = "Mở tài khoản";
			bt2.Font = new System.Drawing.Font("Timesnewroman", 30, FontStyle.Italic);
			bt2.ForeColor = Color.Blue;
			bt2.TextAlign = ContentAlignment.BottomCenter;
			this.Controls.Add(bt2);

			bt3.Location = new Point(3, 545);
			bt3.Size = new Size(625, 512);
			this.Controls.Add(bt3);

			bt4.Location = new Point(635, 545);
			bt4.Size = new Size(625, 512);
			this.Controls.Add(bt4);

			this.KeyPreview = true;
			this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
		}
		private void MainWindow_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.E)
			{
				Control ed = new Control();
				ed.Show();
			}else if(e.Control && e.KeyCode == Keys.Q) {
				System.Environment.Exit(1);
			}
		}

		private void bt1_hover(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Orange;
		}
		private void bt1_leave(object sender, EventArgs e)
		{
			bt1.ForeColor = Color.Blue;
		}
		private void bt2_hover(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Orange;
		}
		private void bt2_leave(object sender, EventArgs e)
		{
			bt2.ForeColor = Color.Blue;
		}
		public static void print(string service,string num)
		{
			if (!printTicket) return;
			string pa = Directory.GetCurrentDirectory();
					
			Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
			Document doc = new Document();
			//string filename = "";
			object missing = System.Type.Missing;
			doc = word.Documents.Open(Path.GetFullPath(@"" + pa + "\\ticket.docx"),
					ref missing, ref missing, ref missing, ref missing,
					ref missing, ref missing, ref missing, ref missing,
					ref missing, ref missing, ref missing, ref missing,
					ref missing, ref missing, ref missing);
			for (int i = 1; i <= doc.Paragraphs.Count; i++)
			{
				
				if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "{giaodich}"))
				{
					//MessageBox.Show(doc.Paragraphs[i].Range.Text);
					doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "{giaodich}", service, RegexOptions.IgnoreCase);

				}
				else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "{sothutu}"))
				{
					//MessageBox.Show(doc.Paragraphs[i].Range.Text);
					doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "{sothutu}", num, RegexOptions.IgnoreCase);

				}

			}
			((_Document)doc).SaveAs2(@"" + pa + "\\tk.docx");
			((_Document)doc).Close();
			((_Application)word).Quit();

			Process p = new Process();
			p.StartInfo.FileName = @"" + pa + "\\tk.docx";
			p.StartInfo.UseShellExecute = true;
			p.StartInfo.Verb = "printto";
			p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			p.Start();
			
		}
	
		private void bt1_click(object sender, EventArgs e)
		{
			Function.fmName = 1;
			Layso layso = new Layso();

			layso.MdiParent = this;
			layso.Text = Function.services.Rows[0][1] + "";

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
			layso.Text = Function.services.Rows[1][1] + "";

			layso.Dock = DockStyle.Fill;
			layso.ControlBox = false;
			layso.AutoSize = false;

			layso.Show();

			bt1.Hide();
			bt2.Hide();
			bt3.Hide();
			bt4.Hide();
		}
		public static void loadConfig()
		{
			XmlDocument xd = new XmlDocument();
			xd.Load("config.xml");

			XmlNodeList nodelist = xd.SelectNodes("/config");

			foreach (XmlNode node in nodelist)
			{
				try
				{
					Function.host = (node.SelectSingleNode("host").InnerText);
				}
				catch (Exception) { }
				try
				{
					Function.username = node.SelectSingleNode("user").InnerText;
				}
				catch (Exception) { }
				try
				{
					Function.password = (node.SelectSingleNode("pass").InnerText);
					//MessageBox.Show(ipAndroid);
				}
				catch (Exception) { }
				try
				{
					Function.database = (node.SelectSingleNode("db").InnerText);
					//MessageBox.Show(ipAndroid);
				}
				catch (Exception) { }
				try
				{
					printTicket = (node.SelectSingleNode("printBill").InnerText)=="1"?true:false;
					//MessageBox.Show(ipAndroid);
				}
				catch (Exception) { }

			}
		}
		private static bool printTicket = false;
		[Obsolete]
		private void Main_Load(object sender, EventArgs e)
		{
			loadConfig();			
			
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "select * from services";
			MySqlCommand cm = new MySqlCommand(sql, conn);
			using (DbDataReader reader = cm.ExecuteReader())
			{
				if (reader.HasRows)
				{
					Function.services = new DataTable();
					Function.services.Load(reader);
				}
			}
			Function.data_services = "";
			foreach (DataRow dtrow in Function.services.Rows)
			{
				Function.data_services += "|" + dtrow[1];
			}

			sql = "SELECT TIME FROM cus_deal ORDER BY TIME DESC LIMIT 1";
			cm = new MySqlCommand(sql, conn);
			DateTime time=DateTime.Now;
			using (DbDataReader reader = cm.ExecuteReader())
			{
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						time = (DateTime)reader.GetValue(0);						
					}
				}
			}
			if (!time.ToString("dd/MM/yyyy").Equals(DateTime.Now.ToString("dd/MM/yyyy"))){
				sql = "update services set current_cus =id*1000";
				cm = new MySqlCommand(sql, conn);
				cm.ExecuteNonQuery();

				sql = "truncate table cus_wait";
				cm = new MySqlCommand(sql, conn);
				cm.ExecuteNonQuery();
			}
			conn.Close();			
			clients = new Dictionary<int, int>();
			Thread t = new Thread(sound);
			t.Start();
			SocketRun.SocketCreate();

		}
		private static void loadQuestion()
		{
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT question.id, question, question_attr.id,question_attr.`votes`" +
				" FROM question INNER JOIN question_attr ON question.id = question_attr.`question_id` " +
				"AND active=1";
			MySqlCommand cm = new MySqlCommand(sql, conn);			
			
			int id = 0;
			using (DbDataReader reader = cm.ExecuteReader())
			{
				if (reader.HasRows)
				{
					//Function.services = new DataTable();
					//Function.services.Load(reader);
					while (reader.Read())
					{
						if (id != (Int32)reader.GetValue(0))
						{	
							id = (Int32)reader.GetValue(0);								
							questionList += reader.GetValue(1).ToString() + "{{" + reader.GetValue(3).ToString();
							
						}
						else
						{
							questionList +=  "[[" + (string)reader.GetValue(3);
						}
						
					}
				}
			}

			
		}
		public static bool isProcess = false;
		public static void processData(string st)
		{

			isProcess = true;

			//try
			//{
			String[] arrRs = st.Split('|');
			if (arrRs[0] == "login")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				string sql = "select * from client where lower(ipcas) like '" + arrRs[1] + "'";
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							SocketRun.sendData("login", (int)reader.GetValue(0), (int)reader.GetValue(2), (string)reader.GetValue(3), (int)reader.GetValue(4), 0, Function.data_services);
						}
					}
				}
				sql = "UPDATE client SET idle=1, active=1, ip_address ='" + SocketRun.ip.Address + "' WHERE  ipcas like '" + arrRs[1] + "'";
				cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			else if (arrRs[0] == "logout")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				String sql = "UPDATE client SET idle=0, active=0 WHERE id=" + arrRs[1];
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			else if (arrRs[0] == "question")
			{
				loadQuestion();
				SocketRun.sendData("question",0,0,"android",0,0,questionList );
				
			}
			else if (arrRs[0] == "idle")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				String sql = "UPDATE client SET idle=1 WHERE id=" + arrRs[1];
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				conn.Close();
				SocketRun.sendData("idle", Int32.Parse(arrRs[1]), 0, "", 0, 0, "");
			}
			else if (arrRs[0] == "notidle")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				String sql = "UPDATE client SET idle=0 WHERE id=" + arrRs[1];
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();
				conn.Close();
				SocketRun.sendData("notidle", Int32.Parse(arrRs[1]), 0, "", 0, 0, "");
			}

			else if (arrRs[0] == "data")
			{

				//MessageBox.Show("xu ly tin nhan tu client: " + st);
				bool isAdd = false;
				int cus_id = 0;
				int client_id = 0, service_id = 0, gate = 0;
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				string sql = "SELECT * FROM cus_wait AS cus  INNER JOIN `client` AS cl  ON cus.service_id=cl.service_id AND cus.`receive_client_id`=0 AND cl.active=1 AND cl.id= " + arrRs[1] + " ORDER BY cus.`priority` DESC, cus.cus_id ASC LIMIT 1";
				MySqlCommand cm = new MySqlCommand(sql, conn);
				using (DbDataReader reader = cm.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							if (!clients.ContainsValue((int)reader.GetValue(8))) //nếu khách đang chờ thì để gọi vào cổng thì không nhận nữa
							{
								clients.Add((int)reader.GetValue(0), (int)reader.GetValue(8));
								cus_id = (int)reader.GetValue(0);
								client_id = (int)reader.GetValue(4);
								service_id = (int)reader.GetValue(6);
								gate = (int)reader.GetValue(8);
								isAdd = true;
								SocketRun.sendData("data", (int)reader.GetValue(4), (int)reader.GetValue(6), reader.GetValue(7).ToString(), (int)reader.GetValue(8), (int)reader.GetValue(0), "");
							}
						}
					}
				}
				//cập nhật trạng thái tiếp khách client					
				sql = "UPDATE client SET idle=0 WHERE id=" + arrRs[1];
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();

				if (isAdd)//xóa khách khỏi bảng chờ
				{
					sql = "Delete FROM cus_wait WHERE cus_id = " + cus_id;
					cmd = new MySqlCommand(sql, conn);
					cmd.ExecuteNonQuery();

					//ghi lịch sử giao dịch
					sql = "insert into cus_deal(cus_id,client_id,service_id,gate,question) values(@cus_id,@client_id,@service_id,@gate,@question)";
					cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;

					cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = cus_id;
					cmd.Parameters.Add("@client_id", MySqlDbType.Int32).Value = client_id;
					cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = service_id;
					cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = gate;
					cmd.Parameters.Add("@question", MySqlDbType.Int32).Value = 1;
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}
			else if (arrRs[0] == "switch")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				string sql = "select * from client where service_id = " + arrRs[2] + " AND active = 1 AND id NOT IN (SELECT id FROM CLIENT WHERE id = " + arrRs[1] + ")";
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				string data = "";
				using (DbDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
					{

						while (reader.Read())
						{
							data += "|" + reader.GetValue(0).ToString() + "_" + reader.GetValue(3).ToString() + "_" + reader.GetValue(4) + "_" + reader.GetValue(5);
						}

					}
					SocketRun.sendData("switch", Int32.Parse(arrRs[1]), 0, "", 0, 0, data);
				}
				conn.Close();
			}
			else if (arrRs[0] == "pass")
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();

				string sql = "insert into cus_wait(cus_id,service_id,receive_client_id,priority) values(@cus_id,@service_id,@receive_client_id,@priority)";
				MySqlCommand cmd = new MySqlCommand();
				cmd = new MySqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = sql;

				cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = arrRs[4];
				cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = arrRs[2];
				cmd.Parameters.Add("@receive_client_id", MySqlDbType.Int32).Value = arrRs[3];
				cmd.Parameters.Add("@priority", MySqlDbType.Int32).Value = 1;
				cmd.ExecuteNonQuery();
				SocketRun.sendData("pass", Int32.Parse(arrRs[1]), 0, "", 0, 0, "");
				//cập nhật trạng thái tiếp khách client					
				sql = "UPDATE client SET idle=1 WHERE id=" + arrRs[1];
				cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();

				sql = "select name,ip_address from client where id = " + arrRs[1] + " or id = " + arrRs[3];
				MySqlDataAdapter adap = new MySqlDataAdapter(sql, conn);
				DataTable dt = new DataTable();
				adap.Fill(dt);
				if (dt.Rows.Count > 1)
				{
					using (adap)
					{
						string send_name, receive_ip;
						receive_ip = dt.Rows[0][1].ToString();
						send_name = dt.Rows[1][0].ToString();
						SocketRun.connectClient(receive_ip, send_name, Int32.Parse(arrRs[4]));
					}
				}

				conn.Close();
				//SocketRun.connectClient("tétts", Int32.Parse(arrRs[4]));
			}
			else if (arrRs[0] == "forcecustomer")
			{

				bool isAdd = false;
				int cus_id = 0;
				int client_id = 0, service_id = 0, gate = 0;
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				string sql = "SELECT * FROM cus_wait AS cus  INNER JOIN `client` AS cl  ON cus.service_id=cl.service_id AND cus.`receive_client_id`= " + arrRs[1] + " AND cl.active=1 AND cl.id= " + arrRs[1] + " ORDER BY cus.`priority` DESC, cus.cus_id ASC LIMIT 1";
				MySqlCommand cm = new MySqlCommand(sql, conn);
				using (DbDataReader reader = cm.ExecuteReader())
				{
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							if (!clients.ContainsValue((int)reader.GetValue(8))) //nếu khách đang chờ thì để gọi vào cổng thì không nhận nữa
							{
								clients.Add((int)reader.GetValue(0), (int)reader.GetValue(8));
								cus_id = (int)reader.GetValue(0);
								client_id = (int)reader.GetValue(4);
								service_id = (int)reader.GetValue(6);
								gate = (int)reader.GetValue(8);
								isAdd = true;
								SocketRun.sendData("data", client_id, service_id, reader.GetValue(7).ToString(), gate, cus_id, "");
							}
						}
					}
				}

				//cập nhật trạng thái tiếp khách client					
				sql = "UPDATE client SET idle=0 WHERE id=" + arrRs[1];
				MySqlCommand cmd = new MySqlCommand(sql, conn);
				cmd.ExecuteNonQuery();

				if (isAdd)//xóa khách khỏi bảng chờ
				{
					sql = "Delete FROM cus_wait WHERE cus_id = " + cus_id;
					cmd = new MySqlCommand(sql, conn);
					cmd.ExecuteNonQuery();

					//ghi lịch sử giao dịch
					sql = "insert into cus_deal(cus_id,client_id,service_id,gate,question) values(@cus_id,@client_id,@service_id,@gate,@question)";
					cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;

					cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = cus_id;
					cmd.Parameters.Add("@client_id", MySqlDbType.Int32).Value = client_id;
					cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = service_id;
					cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = gate;
					cmd.Parameters.Add("@question", MySqlDbType.Int32).Value = 1;
					cmd.ExecuteNonQuery();

					//ghi đánh giá của khách hàng
					sql = "insert into cus_vote_data(cus_id,question_id,question_attr_id,data) values(@cus_id,@question_id,@question_attr_id,@data)";
					cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;

					//cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = cus_id;
					//cmd.Parameters.Add("@question_id", MySqlDbType.Int32).Value = question_id;
					//cmd.Parameters.Add("@question_attr_id", MySqlDbType.Int32).Value = question_attr_id;
					//cmd.Parameters.Add("@data", MySqlDbType.Int32).Value = data;
					cmd.ExecuteNonQuery();
				}
				conn.Close();
			}

		}
		public static void sound()
		{
			while (true)
			{
				if (clients.Count > 0)
				{
					try
					{
						foreach (KeyValuePair<int, int> entry in clients)
						{
							int num = entry.Key;
							int ra = entry.Value;
							string donvi = num.ToString().Substring(3, 1);
							string chuc = num.ToString().Substring(2, 1);
							string tram = num.ToString().Substring(1, 1);
							string nghin = num.ToString().Substring(0, 1);

							WMPLib.WindowsMediaPlayer mp = new WMPLib.WindowsMediaPlayer();
							WMPLib.IWMPPlaylist playlist = mp.playlistCollection.newPlaylist("customerCall");
							WMPLib.IWMPMedia media, media1, media2, media3, media4, media5, media6;

							string moi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\moi.mp3";
							media = mp.newMedia(moi);

							string sokhachnghin = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + nghin + ".mp3";
							media1 = mp.newMedia(sokhachnghin);

							string sokhachtram = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + tram + ".mp3";
							media2 = mp.newMedia(sokhachtram);

							string sokhachchuc = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + chuc + ".mp3";
							media3 = mp.newMedia(sokhachchuc);

							string sokhachdonvi = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + donvi + ".mp3";
							media4 = mp.newMedia(sokhachdonvi);

							string vaocua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\cuaso.mp3";
							media5 = mp.newMedia(vaocua);

							string socua = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\")) + "Resources\\" + ra + ".mp3";
							media6 = mp.newMedia(socua);

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
							Thread.Sleep(7000); //sleep 10s đợi phát âm thanh gọi khách trước xong.
						}
					}
					catch { }
				}


			}
		}

		private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{

		}
	}
}
