using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerService
{
	public partial class Layso : Form
	{
		public static Label lb;
		public Layso()
		{
			InitializeComponent();
			this.Size = new Size(600, 490);
			lb = new Label();
			lb.Location = new Point(5, this.Height / 5);
			button1.Location = new Point(this.Width / 2 - button1.Width / 4, this.Height / 6 * 4);

			button2.Location = new Point(this.Width / 2 - button1.Width / 4, this.Height / 6 * 5 + 30);


			lb.Text = (Convert.ToInt32(Function.services.Rows[Function.fmName - 1][2]) + 1) + "";

			lb.Size = new Size(600, 200);
			lb.Font = new Font("Arial", 99, FontStyle.Bold);
			lb.ForeColor = Color.Red;
			lb.TextAlign = ContentAlignment.MiddleCenter;
			lb.FlatStyle = FlatStyle.Flat;

			lb.BackColor = Color.Transparent;
			this.Controls.Add(lb);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//MessageBox.Show(label1.Width+"");

			check_idle_client(Function.fmName, Function.services);
			this.Close();
			Main.bt1.Show();
			Main.bt2.Show();
			Main.bt3.Show();
			Main.bt4.Show();
			Function.fmName = 0;
		}

		private void check_idle_client(int service_id, DataTable dt)
		{
			Function.services.Rows[service_id - 1][2] = Convert.ToInt32(Function.services.Rows[service_id - 1][2]) + 1;
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "update services SET current_cus = @current_cus WHERE id = @id";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = sql;
			cmd.Parameters.Add("@current_cus", MySqlDbType.Int32).Value = Function.services.Rows[service_id - 1][2];
			cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = service_id;
			cmd.ExecuteNonQuery();

			sql = "insert into cus_wait(cus_id,service_id) values(@cus_id,@service_id)";
			cmd = new MySqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = sql;

			cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = Function.services.Rows[service_id - 1][2];
			cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = service_id;
			cmd.ExecuteNonQuery();

		}
		private void button2_Click(object sender, EventArgs e)
		{
			this.Close();
			Main.bt1.Show();
			Main.bt2.Show();
			Main.bt3.Show();
			Main.bt4.Show();
			Function.fmName = 0;
		}

		private void Layso_Load(object sender, EventArgs e)
		{

		}
	}
}
