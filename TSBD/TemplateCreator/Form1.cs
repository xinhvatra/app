﻿using Microsoft.Office.Interop.Word;
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

namespace TSBD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Visible = false;
        }


        int count = 0;
        int lav = 0;
        int run = 0;
        string LAVtemp = "";
        int dunohientai, sotiengiaingan;
        int tongts = 0, tongbaodam = 0;
        string[] LDS, giatri, giatritt, mucdichvay, LAV, ngayvay;
        string[] diachits, dientich, loaidat, sothua, tobando, soqsdđ, noicap, ngaycapsdđ,
            sovaoso, tsganlien, dientichxaydung, dientichsudung, giatritsbđ, phamvibđ;
        private void button1_Click(object sender, EventArgs e)
        {

            for (int j = 0; j <= dtDuno.Rows.Count - 1; j++) // chạy bảng khách hàng đã phân công theo cán bộ
            {
                LDS = new string[5] { "", "", "", "", "" };
                giatri = new string[5] { "", "", "", "", "" };
                mucdichvay = new string[5] { "", "", "", "", "" };
                ngayvay = new string[5] { "", "", "", "", "" };
                giatritt = new string[5] { "", "", "", "", "" };
                LAV = new string[5] { "", "", "", "", "" };

                diachits = new string[5] { "", "", "", "", "" };
                dientich = new string[5] { "", "", "", "", "" };
                loaidat = new string[5] { "", "", "", "", "" };
                sothua = new string[5] { "", "", "", "", "" };
                tobando = new string[5] { "", "", "", "", "" };
                soqsdđ = new string[5] { "", "", "", "", "" };
                noicap = new string[5] { "", "", "", "", "" };
                ngaycapsdđ = new string[5] { "", "", "", "", "" };
                sovaoso = new string[5] { "", "", "", "", "" };
                tsganlien = new string[5] { "", "", "", "", "" };
                dientichxaydung = new string[5] { "", "", "", "", "" };
                dientichsudung = new string[5] { "", "", "", "", "" };
                giatritsbđ = new string[5] { "", "", "", "", "" };
                phamvibđ = new string[5] { "", "", "", "", "" };
                dunohientai = 0;
                sotiengiaingan = 0;
                string maKH = dtDuno.Rows[j][0].ToString();

                if (dtDuno.Rows[j][17].ToString().Trim() == "") break;
                dunohientai[0] = Int32.Parse(dtDuno.Rows[j][17].ToString());
                LDS[0] = dtDuno.Rows[j][8].ToString();
                giatri[0] = dtDuno.Rows[j][13].ToString();
                mucdichvay[0] = dtDuno.Rows[j][58].ToString();
                giatritt[0] = dtDuno.Rows[j][17].ToString();
                LAV[0] = dtDuno.Rows[j][3].ToString();
                sotiengiaingan[lav] = Int32.Parse(dtDuno.Rows[j][7].ToString());
                label1.Text = "";
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                Document doc = new Document();
                string filename = "";
                string pa = System.Environment.CurrentDirectory;
                object missing = System.Type.Missing;
                filename = (pa + "\\12A.docx");
                doc = word.Documents.Open(Path.GetFullPath(filename),
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing, ref missing,
                        ref missing, ref missing, ref missing);
                count = 0;

                for (int i = 1; i <= doc.Paragraphs.Count; i++)
                {             


                    for (int z = j + 1; z <= dtDuno.Rows.Count - 1; z++)// chạy bảng để so sánh lav
                    {
                        if (dtDuno.Rows[z][0].ToString().Trim() == "") break;
                        if (dtDuno.Rows[j][3].ToString() == dtDuno.Rows[z][3].ToString())
                        {
                            count++;
                            dunohientai += Int32.Parse(dtDuno.Rows[j][17].ToString());
                            giatri[count] = dtDuno.Rows[z][13].ToString();
                            mucdichvay[count] = dtDuno.Rows[z][58].ToString();
                            giatritt[count] = dtDuno.Rows[z][17].ToString();
                            LDS[count] = dtDuno.Rows[z][8].ToString();
                            label2.Text = dtDuno.Rows[j][2].ToString() + " - LAV: " + dtDuno.Rows[j][3].ToString() + " - " + " LDS: " + LDS[count] + " - giatritt[0]: " + giatritt[0] + "\n";

                            int dem = j;
                            //label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "tenkhachhang"))
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("tenkhachhang", dtDuno.Rows[j][2].ToString());
                            }

                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "diachi"))
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("diachi", dtDuno.Rows[j][52].ToString() + ", " + dtDuno.Rows[j][50].ToString() + ", " + dtDuno.Rows[j][48].ToString());
                            }
                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "LAV"))
                            {

                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("LAV", LAV[lav].ToString());
                            }
                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "ngayvay"))
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("ngayvay", dtDuno.Rows[j][4].ToString());

                            }
                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "dunohientai"))
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("dunohientai", dunohientai[lav].ToString("N0"));

                            }
                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "sotiengiaingan"))
                            {
                                int dnhientai = Int32.Parse(dtDuno.Rows[j][7].ToString());
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("sotiengiaingan", dnhientai.ToString("N0"));
                            }
                            else
                            if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "mucdichvay"))
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("mucdichvay", dtDuno.Rows[j][58].ToString());
                            }
                            else
                            {
                                for (int k = 0; k < 5; k++)
                                {

                                    if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "LDS" + (k + 1).ToString()))
                                    {
                                        label1.Text += "LDS" + (k + 1).ToString() + ": " + LDS[k].ToString();
                                        doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("LDS" + (k + 1).ToString(), LDS[k].ToString());
                                    }
                                    else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Mucdichvay" + (k + 1).ToString()))
                                    {

                                        label1.Text += "Mucdichvay" + (k + 1).ToString() + ": " + mucdichvay[k].ToString();
                                        doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Mucdichvay" + (k + 1).ToString(), mucdichvay[k].ToString());
                                    }
                                    else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Giatri" + (k + 1).ToString()))
                                    {
                                        if (giatri[k].ToString() == "")
                                        {
                                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Giatri" + (k + 1).ToString(), giatri[k].ToString());
                                            continue;
                                        }
                                        label1.Text += "Giatri" + (k + 1).ToString() + ": " + giatri[k].ToString();
                                        doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Giatri" + (k + 1).ToString(), Int32.Parse(giatri[k].ToString()).ToString("N0"));
                                    }
                                    else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Dno" + (k + 1).ToString()))
                                    {
                                        if (giatritt[k].ToString() == "")
                                        {
                                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dno" + (k + 1).ToString(), giatritt[k].ToString());
                                            continue;

                                        }
                                        label1.Text += "Du no " + (k + 1).ToString() + ": " + giatritt[k].ToString() + "\n";
                                        doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dno" + (k + 1).ToString(), Int32.Parse(giatritt[k].ToString()).ToString("N0"));
                                    }
                                }


                            }
                        }
                        ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\12A\\" + dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString() + ".docx");
                        ((_Document)doc).Close();
                        ((_Application)word).Quit();
                      

                        lav++;
                        LAV[lav] = dtDuno.Rows[j][3].ToString();
                        dunohientai[lav] = Int32.Parse(dtDuno.Rows[j][17].ToString());
                        ngayvay[lav] = dtDuno.Rows[j][4].ToString();
                        mucdichvay[lav] = dtDuno.Rows[j][58].ToString();
                        sotiengiaingan[lav] = Int32.Parse(dtDuno.Rows[z][7].ToString());

                    }
                    //label2.Text += dtDuno.Rows[j][2].ToString() + " - LAV: " + LAV[lav].ToString() + " - " + " mucdichvay0: " + mucdichvay[0] + " - giatritt[0]: " + giatritt[0];








                }

                j = j + run;
                if (j >= dtDuno.Rows.Count - 1) break;


            }

           



            MessageBox.Show("OK XONG!");


            int count1 = 0;
            for (int y = 0; y < dtTaisan.Rows.Count - 1; y++) // chạy bảng tài sản
            {
                if (maKH == dtTaisan.Rows[y][2].ToString())
                {
                    if (dtTaisan.Rows[y][7].ToString() == "1")
                    {
                        dientich[count1] = dtTaisan.Rows[y][24].ToString();
                        //diachits[count1] = dtTaisan.Rows[y][23].ToString();
                        soqsdđ[count1] = dtTaisan.Rows[y][59].ToString();
                        //	giatritsbđ[count1]= dtTaisan.Rows[y][5].ToString();
                        count1++;
                    }

                }
            }

            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Document doc = new Document();
            string filename = "";
            string pa = System.Environment.CurrentDirectory;
            object missing = System.Type.Missing;

            filename = (pa + "\\12B.docx");

            missing = System.Type.Missing;
            doc = word.Documents.Open(Path.GetFullPath(filename),
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);
            for (int i = 1; i <= doc.Paragraphs.Count; i++)
            {
                int dem = j;
                //label2.Text = dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString();
                if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "tenkhachhang"))
                {
                    doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("tenkhachhang", dtDuno.Rows[j][2].ToString());
                }

                else
                if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "diachi"))
                {
                    doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("diachi", dtDuno.Rows[j][52].ToString() + ", " + dtDuno.Rows[j][50].ToString() + ", " + dtDuno.Rows[j][48].ToString());
                }
                else
                {
                    for (int k = 0; k < 5; k++)
                    {
                        if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "LAV" + (k + 1).ToString()))
                        {
                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("LAV", LAV[k].ToString());
                        }
                        else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Mucdichvay" + (k + 1).ToString()))
                        {
                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Mucdichvay" + (k + 1).ToString(), mucdichvay[k].ToString());
                        }
                        else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Sotiengiaingan" + (k + 1).ToString()))
                        {
                            if (sotiengiaingan[k].ToString() == "0")
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Sotiengiaingan" + (k + 1).ToString(), "");
                                continue;
                            }
                            //
                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Sotiengiaingan" + (k + 1).ToString(), sotiengiaingan[k].ToString("N0"));
                        }
                        else if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Dunohientai" + (k + 1).ToString()))
                        {
                            if (dunohientai[k].ToString() == "0")
                            {
                                doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dunohientai" + (k + 1).ToString(), "");
                                continue;

                            }

                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Dunohientai" + (k + 1).ToString(), dunohientai[k].ToString("N0"));
                        }
                        if (Regex.IsMatch(doc.Paragraphs[i].Range.Text, "Ngayvay"))
                        {
                            doc.Paragraphs[i].Range.Text = doc.Paragraphs[i].Range.Text.Replace("Ngayvay", ngayvay[k].ToString());

                        }

                    }


                            ((_Document)doc).SaveAs2(@"" + pa + "\\maubieu\\12B\\" + dtDuno.Rows[j][2].ToString() + "_" + dtDuno.Rows[j][3].ToString() + "_" + dtDuno.Rows[j][0].ToString() + ".docx");
                    ((_Document)doc).Close();
                    ((_Application)word).Quit();
                }
            }
        }
        DataTable dtDuno, dtTaisan;
        //lấy dư nợ ở sheet1, tài sản ở sheet2
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
                        string sql = "SELECT * from [Sheet1$]";
                        using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
                        {
                            cmd.Fill(myDataSet);
                        }
                        dtDuno = myDataSet.Tables[0];
                        myDataSet = new DataSet();
                        sql = "SELECT * from [Sheet2$]";
                        using (OleDbDataAdapter cmd = new OleDbDataAdapter(sql, conn))
                        {
                            cmd.Fill(myDataSet);
                        }
                        dtTaisan = myDataSet.Tables[0];
                        conn.Close();

                    }
                    button1.Visible = true;
                }
            }
        }
    }
}
