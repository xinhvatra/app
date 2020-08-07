using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace QuesttionRender
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			textBox1.Text = "1";
			button1.Visible = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			for (int j = 0; j < dtName.Rows.Count; j++) // chạy bảng khách hàng đã phân công theo cán bộ
			{
				Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
				Document doc = new Document();
				string filename = "";
				string pa = System.Environment.CurrentDirectory;

				filename = (pa + "\\TBB DN.docx");
				if (j > 14) filename = (pa + "\\TBB CN.docx");
				//MessageBox.Show(filename);
				object missing = System.Type.Missing;
				doc = word.Documents.Open(Path.GetFullPath(filename),
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing, ref missing,
						ref missing, ref missing, ref missing);
				for (int i = 1; i <= doc.Paragraphs.Count; i++)
				{
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "canbotindung"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString())) //so sánh mã KH ở bảng phân công cán bộ với dữ liệu lấy từ ipcas
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "canbotindung", dtData.Rows[k][32].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "mucdichvay"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "mucdichvay", dtData.Rows[k][61].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "soducaptindung"))
					{
						long sum = 0;
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{

								//MessageBox.Show(dtData.Rows[k][32].ToString());
								sum = long.Parse(dtData.Rows[k][7].ToString());
							}
						}
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "soducaptindung",Convert.ToInt64(sum).ToString("N0"), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "duno"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "duno", Convert.ToInt64(dtName.Rows[j][3].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "sohopdongtindung"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "sohopdongtindung", dtData.Rows[k][3].ToString(), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "sotiengiaingan"))
					{
						for (int k = 0; k < dtData.Rows.Count; k++)
						{
							if (Regex.IsMatch(dtName.Rows[j][1].ToString(), dtData.Rows[k][0].ToString()))
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "sotiengiaingan", Convert.ToInt64(dtData.Rows[k][13].ToString()).ToString("N0"), RegexOptions.IgnoreCase);
								//MessageBox.Show(dtData.Rows[k][32].ToString());
								break;
							}
						}
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "tenkhachhang"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "tenkhachhang", dtName.Rows[j][2].ToString(), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "canbokiemtra"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "canbokiemtra", dtName.Rows[j][5].ToString(), RegexOptions.IgnoreCase);
					}
					else
					if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "makhachhang"))
					{
						//MessageBox.Show(dtName.Rows[j][2].ToString());
						doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, "makhachhang", dtName.Rows[j][1].ToString(), RegexOptions.IgnoreCase);
					}
				}
				//MessageBox.Show("OK XONG!");
				if(j>=13) ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\CN\\" + dtName.Rows[j][2].ToString() + "_" + dtName.Rows[j][1].ToString() + "_" + dtName.Rows[j][5].ToString() + ".docx");
				else ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\DN\\" + dtName.Rows[j][2].ToString() + "_" + dtName.Rows[j][1].ToString() + "_" + dtName.Rows[j][5].ToString() + ".docx");
				((_Document)doc).Close();
				((_Application)word).Quit();
			}
			MessageBox.Show("OK XONG!");
		}

		DataTable dtData, dtName;
		private void button2_Click(object sender, EventArgs e)
		{
			DataSet myDataSet = new DataSet();
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "Excell File |*.xlsx;*.xls";
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				string extn = Path.GetExtension(openDialog.FileName).ToLower();
				if (extn.Equals(".xls") || extn.Equals(".xlsx"))
				{
					//filename = Path.GetFileNameWithoutExtension(openDialog.FileName);

					string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Path.GetFullPath(openDialog.FileName) + ";Extended Properties=Excel 12.0";

					OleDbConnection conn = new OleDbConnection(connString);
					conn.Open();
					System.Data.DataTable dbSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
					if (dbSchema != null && dbSchema.Rows.Count >= 1)
					{
						//string firstSheetName = dbSchema.Rows[0]["TABLE_NAME"].ToString();
						string sql = "SELECT * from [Sheet2$]";
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtData = myDataSet.Tables[0];
						myDataSet = new DataSet();
						sql = "SELECT * from [Sheet1$]";
						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
						}
						dtName = myDataSet.Tables[0];
						//tring str1 = "insert into Nhansu  values(?, ?, ?)";						
						conn.Close();

					}
					button1.Visible = true;
				}
			}
		}
	}
}
