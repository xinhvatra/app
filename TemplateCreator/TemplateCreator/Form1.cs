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

namespace QuesttionRender
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			textBox1.Text = "1";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
			Document doc = new Document();

			string filename = "";
			OpenFileDialog openDialog = new OpenFileDialog();
			openDialog.Filter = "Word File |*.doc;*.docx";
			if (openDialog.ShowDialog() == DialogResult.OK)
			{
				string pa = Path.GetDirectoryName(openDialog.FileName);
				//MessageBox.Show(Path.GetDirectoryName( openDialog.FileName));
				string extn = Path.GetExtension(openDialog.FileName).ToLower();
				if (extn.Equals(".doc") || extn.Equals(".docx"))
				{
					// var excelApp = new Microsoft.Office.Interop.Excel.Application();
					// excelApp.Visible = true;
					//  excelApp.Workbooks.Open(Path.GetFullPath(openDialog.FileName));

					filename = Path.GetFileNameWithoutExtension(openDialog.FileName);


					// Define an object to pass to the API for missing parameters
					object missing = System.Type.Missing;
					doc = word.Documents.Open(Path.GetFullPath(openDialog.FileName),
							ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing, ref missing,
							ref missing, ref missing, ref missing);

					//Range r = doc.Content;

					//while (r.Find.Execute("^p"))
					//MessageBox.Show(.Text);
					//r.WholeStory(); ["Câu hỏi "]\d+[".:"]|["Câu "]\d+[".:"]
					string pattern = @"Câu \d+[\.\:]|Câu hỏi \d+[\.\:]|Câu hỏi \d|Câu \d";
					string pt1 = @"^a\. |^a\.|^b\.|^b\. |^c\.|^c\. |^d\. |^d\.|^a\)\.|^a\)|^b\)\.|^b\)|^c\)\.|^c\)|^d\)\.|^d\)";
					int numques = Regex.Matches(doc.Content.Text, pattern).Count;
					label1.Text = "Có tất cả " + numques + " câu hỏi";

					int para;
					try
					{
						para = Int32.Parse(textBox1.Text);
					}
					catch (Exception)
					{
						para = 1;
					}
					if (para > 1)
					{
						string pattern2 = @"Câu hỏi " + para + "|Câu " + para;
						for (int i = 1; i <= doc.Paragraphs.Count; i++)
						{
							//MessageBox.Show(doc.Paragraphs[i].Range.Text);
							if (!Regex.IsMatch(doc.Paragraphs[i].Range.Text, pattern2))
							{
								doc.Paragraphs[i].Range.Delete();
								i--;
							}
							else
							{
								break;
							}
						}

					}
					else
					{
						para = 1;

					}


					int numans = 0;
					//string pt2 = @"[\=\~]";
					doc.Content.ListFormat.RemoveNumbers(ref missing);

					for (int i = 1; i <= doc.Paragraphs.Count; i++)
					{
						double per = (i * 100 / doc.Paragraphs.Count);
						label2.Text = "Đang tạo câu thứ: " + para + "..." + per + "%";
						//MessageBox.Show("Phan tram: " + i+"phan tram: "+ per);
						//MessageBox.Show("i: "+i +" noi dung truoc: " + doc.Paragraphs[i].Range.Text);
						if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, pattern) && numans > 0 && i > 1)
						{
							if (numans >= 4)
							{
								doc.Paragraphs[i].Range.Text = "}\n\n" + doc.Paragraphs[i].Range.Text;
								para++;
								numans = 0;// MessageBox.Show("i: " + i +"cau tra loi: "+ numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
								continue;
							}
							else
							{
								doc.Paragraphs[i].Range.Text = "~\n\n" + doc.Paragraphs[i].Range.Text;
								numans++;
								//i++;
								//MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
								continue;
							}
						}
						if (i % 8 == 1)
						{
							if (doc.Paragraphs[i].Range.Text.Trim().Length <= 1) break;

							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, @"\t", "", RegexOptions.IgnoreCase);
							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, pattern, "", RegexOptions.IgnoreCase) + "{\n";
							doc.Paragraphs[i].Range.Bold = 0;
							doc.Paragraphs[i].Range.Text = "::Câu " + para + "::" + doc.Paragraphs[i].Range.Text;

							i++;// MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
						}
						else if (doc.Paragraphs[i].Range.Font.Bold == -1 && doc.Paragraphs[i].Range.Text.Length > 2)
						{
							//MessageBox.Show("vao bold");
							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, @"\t", "", RegexOptions.IgnoreCase);
							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, pt1, "", RegexOptions.IgnoreCase);
							doc.Paragraphs[i].Range.Text = "=" + doc.Paragraphs[i].Range.Text;
							numans++;// MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
						}
						else if (doc.Paragraphs[i].Range.Text.Trim().Length <= 1)
						{

							if (i % 8 == 6)
							{
								//	MessageBox.Show("vao 6");
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, @"\t", "", RegexOptions.IgnoreCase);
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, pt1, "", RegexOptions.IgnoreCase);
								doc.Paragraphs[i].Range.Text = "~" + doc.Paragraphs[i].Range.Text + "\n\n";
								numans++; //MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
							}
							else if (i % 8 == 7)
							{
								//MessageBox.Show("vao 7");
								doc.Paragraphs[i].Range.Text = "}\n\n";
								para++;
								numans = 0;// MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
							}
							else
							{
								if (numans > 0)
								{
									//MessageBox.Show("vao khac");
									doc.Paragraphs[i].Range.Text = "~" + doc.Paragraphs[i].Range.Text;
									numans++; //MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
								}
							}
						}
						else
						{
							if (i == doc.Paragraphs.Count && numans < 4)
							{
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, @"\t", "", RegexOptions.IgnoreCase);
								doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, pt1, "", RegexOptions.IgnoreCase);
								doc.Paragraphs[i].Range.Bold = 0;
								doc.Paragraphs[i].Range.Text = "~" + "cau tra loi: " + numans + doc.Paragraphs[i].Range.Text + "\n\n";
								numans++;// MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
								continue;
							}
							//MessageBox.Show("vao day: "+ doc.Paragraphs[i].Range.Text);
							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, @"\t", "", RegexOptions.IgnoreCase);
							doc.Paragraphs[i].Range.Text = Regex.Replace(doc.Paragraphs[i].Range.Text, pt1, "", RegexOptions.IgnoreCase);
							doc.Paragraphs[i].Range.Bold = 0;
							doc.Paragraphs[i].Range.Text = "~" + doc.Paragraphs[i].Range.Text;
							numans++;//MessageBox.Show("i: " + i + "cau tra loi: " + numans + " noi dung sau: " + doc.Paragraphs[i].Range.Text);
						}
						//	MessageBox.Show("i: "+i +" noi dung sau: " + doc.Paragraphs[i].Range.Text);
					}


					for (int i = 2; i < doc.Paragraphs.Count; i++)
					{
						double per = (i * 100 / doc.Paragraphs.Count + 1);
						label2.Text = "Đang kiểm tra câu trả lời trùng lặp..." + per + "%";
						if (i == doc.Paragraphs.Count - 1) break;
						if (doc.Paragraphs[i].Range.Text.Contains("{") | doc.Paragraphs[i].Range.Text.Contains("}") | doc.Paragraphs[i].Range.Text == "\n"
							| doc.Paragraphs[i].Range.Text.Contains("::"))
						{
							continue;
						}
						for (int j = i + 1; j <= i + 4; j++)
						{
							if (doc.Paragraphs[j].Range.Text.Contains("}"))
							{
								break;
							}
							if ((Regex.Replace(doc.Paragraphs[i].Range.Text, @"[\=\~]", "", RegexOptions.IgnoreCase)).Trim() ==
								(Regex.Replace(doc.Paragraphs[j].Range.Text, @"[\=\~]", "", RegexOptions.IgnoreCase)).Trim())
							{
								doc.Paragraphs[i].Range.Font.ColorIndex = WdColorIndex.wdRed;
								doc.Paragraphs[j].Range.Font.ColorIndex = WdColorIndex.wdRed;
							}
						}
					}
					MessageBox.Show("OK XONG!");

					((_Document)doc).SaveAs2(@"" + pa + "\\CAU HOI.docx");
					((_Document)doc).Close();
					((_Application)word).Quit();


				}
			}
		}
		string name = "";
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
						string sql = "SELECT * from [DANH SACH$] ";


						using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
						{
							cmd.Fill(myDataSet);
							conn.Close();
						}
						System.Data.DataTable dt = myDataSet.Tables[0];
						//tring str1 = "insert into Nhansu  values(?, ?, ?)";

						
						conn.Close();
						name=(dt.Rows[5][2].ToString());
					}
				}
			}
		}
	}
}
