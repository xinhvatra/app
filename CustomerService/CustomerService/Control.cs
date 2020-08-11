﻿using MySql.Data.MySqlClient;
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
			if (isQuestion)
			{
				string Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();

				// Set the control's size to the string's size.
				textBox2.Size = TextRenderer.MeasureText(Text, textBox2.Font).Height;
				textBox2.Text = Text;

				textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				//textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
				//textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
			}
			else
			{
				if (e.RowIndex != -1)
				{
					if (radioButton1.Checked)
					{
						radioButton4.Checked = true;
						textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
						comboBox1.SelectedItem = (String)dataGridView1.Rows[e.RowIndex].Cells[2].Value;
						textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
						textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
						client_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
						textBox1.SelectAll();
						textBox1.Focus();

					}
					else if (radioButton2.Checked)
					{
						radioButton4.Checked = true;
						textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
						client_id = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
						textBox1.SelectAll();
						textBox1.Focus();
					}
				}
			}
		}
		private void Edit_Load(object sender, EventArgs e)
		{
			dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dataGridView1.AutoSize = true;
			radioButton1.Checked = true;
			radioButton2.Checked = false;
			radioButton3.Checked = true;
			radioButton4.Checked = false;
			radioButton5.Checked = true;

			textBox2.Visible = false;
			textBox2.Multiline = true;
			textBox5.Visible = false;
			textBox6.Visible = false;
			textBox7.Visible = false;
			textBox8.Visible = false;
			label5.Visible = false;
			isQuestion = false;
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
			if (radioButton1.Checked)
			{
				if (radioButton3.Checked)
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
				else if (radioButton4.Checked)
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
			else if (radioButton2.Checked)
			{
				if (radioButton3.Checked)
				{
					MySqlConnection conn = Function.GetConnection();
					conn.Open();
					string sql = "INSERT into services (name) values(@name)";
					MySqlCommand cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;
					cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox1.Text;
					cmd.ExecuteNonQuery();

					long id = cmd.LastInsertedId;
					sql = "update services  set current_cus = @current_cus where id =@id";
					cmd = new MySqlCommand();
					cmd.Connection = conn;
					cmd.CommandText = sql;
					cmd.Parameters.Add("@current_cus", MySqlDbType.Int32).Value = id * 1000;
					cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
					cmd.ExecuteNonQuery();
					conn.Close();
				}
				else if (radioButton4.Checked)
				{
					try
					{
						MySqlConnection conn = Function.GetConnection();
						conn.Open();
						string sql = sql = "update services  set name = @name where id =@id";
						MySqlCommand cmd = new MySqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = sql;
						cmd.Parameters.Add("@name", MySqlDbType.String).Value = textBox1.Text;
						cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = client_id;
						cmd.ExecuteNonQuery();
						conn.Close();
					}
					catch (Exception)
					{
						MessageBox.Show("Không tìm thấy thông tin người sử dụng");
					}

				}

				getService();
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			//radioButton2.Checked = false;
			radioButton1.ForeColor = Color.Blue;
			radioButton2.ForeColor = Color.Black;
			label1.Text = "Mã ipcas";
			textBox1.Clear();
			textBox3.Clear();
			textBox4.Clear();
			comboBox1.SelectedIndex = -1;
			label2.Visible = true;
			label3.Visible = true;
			label4.Visible = true;
			textBox3.Visible = true;
			textBox4.Visible = true;
			comboBox1.Visible = true;
			getData();
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			radioButton2.ForeColor = Color.Blue;
			radioButton1.ForeColor = Color.Black;
			label1.Text = "Nghiệp vụ";
			textBox1.Clear();
			textBox3.Clear();
			textBox4.Clear();
			comboBox1.SelectedIndex = -1;
			label2.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			textBox3.Visible = false;
			textBox4.Visible = false;
			comboBox1.Visible = false;
			getService();
		}
		private void getService()
		{
			dataGridView1.DataSource = null;
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT services.id,services.`name` AS `Nghiệp vụ` from services";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}

		private void radioButton3_CheckedChanged(object sender, EventArgs e)
		{
			textBox1.Clear();
			textBox3.Clear();
			textBox4.Clear();
			comboBox1.SelectedIndex = -1;
			textBox1.Focus();
		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{

		}
		Boolean isQuestion = true;
		private void radioButton6_CheckedChanged(object sender, EventArgs e)
		{
			isQuestion = true;
			radioButton1.Visible = false;
			radioButton2.Visible = false;
			radioButton6.ForeColor = Color.Red;
			radioButton5.ForeColor = Color.Black;

			textBox2.Visible = true;
			textBox5.Visible = true;
			textBox6.Visible = true;
			textBox7.Visible = true;
			textBox8.Visible = true;
			label5.Visible = true;

			textBox1.Visible = false;
			textBox3.Visible = false;
			textBox4.Visible = false;
			label1.Text = "Câu hỏi";
			label2.Text = "Đánh giá 1";
			label3.Text = "Đánh giá 2";
			label4.Text = "Đánh giá 3";

			// hiển thị câu hỏi trên data gridview
			dataGridView1.DataSource = null;
			MySqlConnection conn = Function.GetConnection();
			conn.Open();
			string sql = "SELECT question.id as 'Mã câu hỏi', question as 'Câu hỏi', question_attr.id as 'Mã đánh giá',question_attr.`votes` as 'Đánh giá'" +
				" FROM question INNER JOIN question_attr ON question.id = question_attr.`question_id` " +
				"AND active=1";
			MySqlDataAdapter cmd = new MySqlDataAdapter(sql, conn);
			DataTable dt = new DataTable();
			cmd.Fill(dt);
			dataGridView1.DataSource = dt;
			conn.Close();
		}

		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{
			radioButton1.Visible = true;
			radioButton2.Visible = true;
			radioButton5.ForeColor = Color.Red;
			radioButton6.ForeColor = Color.Black;

			textBox2.Visible = false;
			textBox5.Visible = false;
			textBox6.Visible = false;
			textBox7.Visible = false;
			textBox8.Visible = false;
			label5.Visible = false;

			textBox1.Visible = true;
			textBox3.Visible = true;
			textBox4.Visible = true;
			label1.Text = "Mã ipcas";
			label2.Text = "Nghiệp vụ";
			label3.Text = "Tên";
			label4.Text = "Cửa số";
			isQuestion = false;
		}
	}
}
