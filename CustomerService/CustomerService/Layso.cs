using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
			lb = new Label();
			button1.Location = new Point(this.Width / 2-button1.Width/4, this.Height/6*4);
			lb.Location = new Point(5, this.Height / 5);

			button2.Location = new Point(this.Width / 2 - button1.Width / 4, this.Height/6*5+30 );
			
			
			if (Function.fmName == 1)
			{
				lb.Text = Function.sttKetoan + "";
			}
			else if (Function.fmName == 2)
			{
				lb.Text = Function.sttDichvu + "";
			}
			//lb.Text = Function.sttKetoan + "";
			lb.Size = new Size(600,200);
			lb.Font = new Font("Arial", 99, FontStyle.Bold);
			lb.ForeColor = Color.Red;
			lb.TextAlign = ContentAlignment.MiddleCenter;
			this.Controls.Add(lb);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			//MessageBox.Show(label1.Width+"");
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "update services SET current_cus = @current_cus WHERE id = @id";
			MySqlCommand cmd = new MySqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = sql;
			if (Function.fmName == 1)
			{
				cmd.Parameters.Add("@current_cus", MySqlDbType.Int32).Value = Function.sttKetoan;
				cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = 1;
				
			}else if(Function.fmName == 2)
			{
				cmd.Parameters.Add("@current_cus", MySqlDbType.Int32).Value = Function.sttDichvu;
				cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = 2;
				
			}
			cmd.ExecuteNonQuery();

			 sql = "insert into cus_wait(cus_id,service_id) values(@cus_id,@service_id)";
			 cmd = new MySqlCommand();
			cmd.Connection = conn;
			cmd.CommandText = sql;
			if (Function.fmName == 1)
			{
				cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = Function.sttKetoan;
				cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = 1;
				Function.sttKetoan++;
			}
			else if (Function.fmName == 2)
			{
				cmd.Parameters.Add("@cus_id", MySqlDbType.Int32).Value = Function.sttDichvu;
				cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = 2;
				Function.sttDichvu++;
			}
			cmd.ExecuteNonQuery();

			this.Close();
			Main.bt1.Show();
			Main.bt2.Show();
			Main.bt3.Show();
			Main.bt4.Show();
			Function.fmName = 0;
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
	}
}
