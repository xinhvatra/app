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
	public partial class Control : Form
	{
		int client_id = 0;
		public Control()
		{
			InitializeComponent();

			foreach (DataRow dtrow in Function.services.Rows)
			{
				comboBox1.Items.Add(dtrow[1]);
			}
			dataGridView1.CellClick += new DataGridViewCellEventHandler(dtgrid_OnCellValueChanged);
		}
		private void dtgrid_OnCellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1)
			{
				textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
				comboBox1.SelectedItem = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;
				textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
				client_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
			}
		}
		private void Edit_Load(object sender, EventArgs e)
		{
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView1.AutoSize = true;
			getData();

		}
		private void getData()
		{
			dataGridView1.DataSource = null;
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT client.id,client.ipcas,services.`name` AS `Nghiệp vụ`,client.`name` `Tên` ,client.gate AS `Cửa` FROM CLIENT INNER JOIN services WHERE client.`service_id`=services.`id`";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}
		private void button1_Click(object sender, EventArgs e)
		{
			if (checkBox1.Checked)
			{
				MySqlConnection conn = Function.GetConnection();
				conn.Open();
				string sql = "INSERT into client (ipcas,service_id,name,gate,idle,active,ip_address) values(@ipcas,@service_id,@name,@gate,@idle,@active,@ip_address)";
				MySqlCommand cmd = new MySqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = sql;

				cmd.Parameters.Add("@ipcas", MySqlDbType.String).Value = textBox1.Text;
				cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = comboBox1.SelectedIndex + 1;
				cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox3.Text;
				cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = textBox4.Text;
				cmd.Parameters.Add("@idle", MySqlDbType.Int32).Value = 0;
				cmd.Parameters.Add("@active", MySqlDbType.Int32).Value = 0;
				cmd.Parameters.Add("@ip_address", MySqlDbType.String).Value = "0.0.0.0";
				cmd.ExecuteNonQuery();
				conn.Close();
			}
			else
			{
				try
				{
					MySqlConnection conn = Function.GetConnection();
					conn.Open();
					string sql = "UPDATE client SET  ipcas=@ipcas,service_id=@service_id,name=@name,gate=@gate WHERE  id=@id";
					MySqlCommand cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;

					cmd.Parameters.Add("@ipcas", MySqlDbType.String).Value = textBox1.Text;
					cmd.Parameters.Add("@service_id", MySqlDbType.Int32).Value = comboBox1.SelectedIndex + 1;
					cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox3.Text;
					cmd.Parameters.Add("@gate", MySqlDbType.Int32).Value = textBox4.Text;
					cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = client_id;
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				catch (Exception)
				{
					MessageBox.Show("Không tìm thấy thông tin người sử dụng");
				}

			}
			getData();
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
