using System;
using System.Collections;
using System.Collections.Generic;
using System.util.collections;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
//using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using WpfMath;

namespace Math_Paper_Generater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[] rand = new int[50];

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Generate_Click(object sender, RoutedEventArgs e)
        {
            if (textbox.Text == "")
                return;
 
            pdfWebViewer.Navigate("about:blank");
            this.IsEnabled = false;
            await Task.Delay(500);
            GeneratePDF();
            this.IsEnabled = true;
        }

        private async void RandomGenerate_Click(object sender, RoutedEventArgs e)
        {
            pdfWebViewer.Navigate("about:blank");
            Random rnd = new Random();
            int r = rnd.Next(100000000, 999999999);
            textbox.Text = r.ToString();
            this.IsEnabled = false;
            await Task.Delay(500);
            GeneratePDF();
            this.IsEnabled = true;
        }

        private void HandleElementNavigating(object sender, NavigatingCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void GeneratePDF()
        {
            rand[0] = (Convert.ToInt32(textbox.Text) * 1103515245) & 0x7fffffff;
            for (int i = 0; i < 49; i++)
            {
                rand[i + 1] = (rand[i] * 1103515245) & 0x7fffffff;
            }

            var paper1 = new Document(PageSize.A4, 60, 60, 80, 50);
            PdfWriter pdf = PdfWriter.GetInstance(paper1, new FileStream("paper1.pdf", FileMode.Create));
            PageEventHelper pageEventHelper = new PageEventHelper();
            pdf.PageEvent = pageEventHelper;
            paper1.Open();
            Font roman = new Font(Font.FontFamily.TIMES_ROMAN, 10f);

            PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 480f;
            table.LockedWidth = true;

            var p = new Paragraph();
            p.Add(new Phrase("SECTION A(1) (35 marks)\n\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 10f, Font.BOLD)));

            //Question 1
            p.Add(new Phrase("\n1.       Simplify  ", roman));
            string strquestion1 = GenerateQuestion(1);
            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(new Uri(GenerateImage(strquestion1, "img1.png")));
            label.Content = strquestion1;
            png.ScalePercent(10f);
            p.Add(new Chunk(png, 0, -9f));
            p.Add(new Phrase("  and express your answer with positive indices.", roman));
            //p.TabSettings = new TabSettings();
            //p.Add(Chunk.TABBING);
            p.Add(new Chunk(new VerticalPositionMark()));
            p.Add(new Phrase("(3 marks)", roman));
            p.Add(new Phrase("_\n\n\n", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new iTextSharp.text.BaseColor(255, 255, 255))));
            for (int i = 0; i < 11; i++)
                p.Add(new Phrase("        ____________________________________________________________________\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 13f)));

            //Question 2
            p.Add(new Phrase("\n2.       Make  ", roman));
            string strquestion2 = GenerateQuestion(2);
            png = iTextSharp.text.Image.GetInstance(new Uri(GenerateImage(strquestion2, "img2.png")));
            png.ScalePercent(10f);
            if (strquestion2.Contains("y")) p.Add(new Chunk(png, 0, -2f));
            else p.Add(new Chunk(png, 0, 0));
            p.Add(new Phrase("  the subject of the formula   ", roman));
            string strquestion3 = GenerateQuestion(3);
            png = iTextSharp.text.Image.GetInstance(new Uri(GenerateImage(strquestion3, "img3.png")));
            png.ScalePercent(10f);
            if (strquestion3.Contains("\\frac{")) p.Add(new Chunk(png, 0, -7f));
            else p.Add(new Chunk(png, 0, -2f));
            p.Add(new Phrase("  .", roman));
            p.Add(new Chunk(new VerticalPositionMark()));
            p.Add(new Phrase("(3 marks)", roman));
            p.Add(new Phrase("_\n\n\n", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new iTextSharp.text.BaseColor(255, 255, 255))));
            for (int i = 0; i < 11; i++)
                p.Add(new Phrase("        ____________________________________________________________________\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 13f)));

            PdfPCell content = new PdfPCell(new Phrase(p));
            //content.Colspan = 4;
            table.AddCell(content);
            paper1.Add(table);

            Paragraph para = new Paragraph(new Phrase("Answers written in the margins will not be marked.", roman)); 
            para.Add(new Chunk(new VerticalPositionMark()));
            para.Add(new Phrase("Seed: " + textbox.Text.ToString(), roman));
            paper1.Add(para);
            //Paragraph password = new Paragraph(new Phrase("Password: " + rand[0].ToString(), new Font(Font.FontFamily.TIMES_ROMAN, 10f)));
            //paper1.Add(password);
            
            //Next page
            paper1.NewPage();

            table = new PdfPTable(1);
            table.TotalWidth = 480f;
            table.LockedWidth = true;

            p = new Paragraph();
            //Question 3
            p.Add(new Phrase("3.       Factorize\n\n          (a)     ", roman));
            string strquestion4 = GenerateQuestion(4);
            png = iTextSharp.text.Image.GetInstance(new Uri(GenerateImage(strquestion4, "img4.png")));
            png.ScalePercent(10f);
            p.Add(new Chunk(png, 0, -2f));
            p.Add(new Phrase("  ,\n\n          (b)     ", roman));
            string strquestion5 = GenerateQuestion(5);
            png = iTextSharp.text.Image.GetInstance(new Uri(GenerateImage(strquestion5, "img5.png")));
            png.ScalePercent(10f);
            p.Add(new Chunk(png, 0, -2f));
            p.Add(new Phrase("  .", roman));
            //p.TabSettings = new TabSettings();
            //p.Add(Chunk.TABBING);
            p.Add(new Chunk(new VerticalPositionMark()));
            p.Add(new Phrase("(3 marks)", roman));
            p.Add(new Phrase("_\n\n\n", FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new iTextSharp.text.BaseColor(255, 255, 255))));
            for (int i = 0; i < 11; i++)
                p.Add(new Phrase("        ____________________________________________________________________\n\n", new Font(Font.FontFamily.TIMES_ROMAN, 13f)));



            content = new PdfPCell(new Phrase(p));
            //content.Colspan = 4;
            table.AddCell(content);
            paper1.Add(table);

            paper1.Add(para);

            paper1.Close();

            pdfWebViewer.Navigate(System.AppDomain.CurrentDomain.BaseDirectory + "paper1.pdf");
        }

        public class PageEventHelper : PdfPageEventHelper
        {
            // write on top of document
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                base.OnOpenDocument(writer, document);
            }

            // write on start of each page
            public override void OnStartPage(PdfWriter writer, Document document)
            {
                base.OnStartPage(writer, document);
            }

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                base.OnEndPage(writer, document);
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
            }
        }

        private string GenerateImage(string latex, string filename)
        {
            string fileName = System.AppDomain.CurrentDomain.BaseDirectory;
            fileName += filename;
            var parser = new TexFormulaParser();
            var formula = parser.Parse(latex);
            var pngBytes = formula.RenderToPng(100.0, 0.0, 0.0, "Times New Roman");
            File.WriteAllBytes(fileName, pngBytes);
            return fileName;
        }

        private string GenerateQuestion(int num)
        {
            string char1 = "x", char2 = "y", char3 = "k", char4 = "m", char5 = "n", strquestion = "";

            int[] r = new int[8];
            switch (num)
            {
                //Indices
                case (1):
                    {
                        for (int i = 0; i < 7; i++)
                            r[i] = (int)(rand[num].ToString()[i]) - 48;

                        strquestion += "\\frac{";

                        //Random set (
                        if (r[0] > 5) strquestion += "(";

                        //First char
                        strquestion += char1;
                        if (r[1] != 0 && r[1] != 1) // != 1 then add ^
                        {
                            strquestion += "^";
                            if (r[2] < 5) // < 5 then add -
                            {
                                strquestion += "{-" + r[1].ToString() + "}";
                            }
                            else strquestion += r[1].ToString();
                        }

                        //Second char
                        if (r[3] < 5) // < 5 then add char2
                        {
                            strquestion += char2;
                            if (r[4] != 0 && r[4] != 1) // != 1 then add ^
                            {
                                strquestion += "^";
                                if (r[5] < 5) // < 5 then add -
                                {
                                    strquestion += "{-" + r[4].ToString() + "}";
                                }
                                else strquestion += r[4].ToString();
                            }
                        }

                        //Random set )^
                        if (r[0] > 5) // > 5 then add )^
                        {
                            strquestion += ")^";
                            if (r[6] < 5) // < 5 then add -
                            {
                                strquestion += "{-" + r[0].ToString() + "}";
                            }
                            else strquestion += r[0].ToString();
                        }

                        //Upper done, downward
                        strquestion += "}{";

                        //Validate
                        strquestion = strquestion.Replace("(x)", "x").Replace("(y)", "y");

                        //Generate new r[] values
                        for (int i = 0; i < 7; i++)
                            r[i] = (int)(rand[num+1].ToString()[i]) - 48;

                        //Random set ( or before not contain ()
                        if (!strquestion.Contains(")^")) strquestion += "(";

                        //First char
                        strquestion += char1;
                        if (r[1] != 0 && r[1] != 1) // != 1 then add ^
                        {
                            strquestion += "^";
                            if (r[2] < 5) // < 5 then add -
                            {
                                strquestion += "{-" + r[1].ToString() + "}";
                            }
                            else strquestion += r[1].ToString();
                        }

                        //Second char
                        if (r[3] < 5 || !strquestion.Contains(char2) || !strquestion.Contains("-")) // < 5 then add char2
                        {
                            strquestion += char2;
                            if ((r[4] != 0 && r[4] != 1) || !strquestion.Contains("-")) // != 1 then add ^
                            {
                                if (r[4] == 0 || r[4] == 1) r[4] = 2;
                                strquestion += "^";
                                if (r[5] < 5 || !strquestion.Contains("-")) // < 5 then add -
                                {
                                    strquestion += "{-" + r[4].ToString() + "}";
                                }
                                else strquestion += r[4].ToString();
                            }
                        }

                        //Random set )^
                        if (!strquestion.Contains(")^")) // > 5 then add )^
                        {
                            if (r[0] == 1)  r[0] = 2;
                            strquestion += ")^";
                            if (r[6] < 5) // < 5 then add -
                            {
                                strquestion += "{-" + r[0].ToString() + "}";
                            }
                            else strquestion += r[0].ToString();
                        }
                        strquestion += "}";

                        //Validate
                        strquestion = strquestion.Replace("(x)", "(x^3)").Replace("(y)", "(y^3)");
                    }
                    break;
                //Change subject
                case (2):
                    {
                        for (int i = 0; i < 7; i++)
                            r[i] = (int)(rand[3].ToString()[i]) - 48;

                        if (r[0] < 3)       strquestion += char1;
                        else if (r[0] < 6)  strquestion += char2;
                        else                strquestion += char3;
                    }
                    break;
                case (3):
                    {
                        for (int i = 0; i < 7; i++)
                            r[i] = (int)(rand[3].ToString()[i]) - 48;

                        //first unknown
                        if (r[1] > 1)       strquestion += r[1].ToString();
                        if (r[0] < 3)       strquestion += char1;
                        else if (r[0] < 6)  strquestion += char2;
                        else                strquestion += char3;

                        //Add equal sign
                        strquestion += "=";

                        //Random add fraction or (
                        if (r[2] < 5)       strquestion += "\\frac{";
                        else                strquestion += "(";

                        //Second unknown
                        if (r[3] > 1)       strquestion += r[3].ToString();
                        if (r[0] < 3)       strquestion += char2;
                        else if (r[0] < 6)  strquestion += char3;
                        else                strquestion += char3;

                        //Random add + or -
                        if (r[4] < 5)       strquestion += "+";
                        else                strquestion += "-";

                        //Third unknown
                        if (r[5] > 1)       strquestion += r[5].ToString();
                        if (r[0] < 3)       strquestion += char1;
                        else if (r[0] < 6)  strquestion += char2;
                        else                strquestion += char1;

                        //Random add back fraction or (
                        if (r[2] < 5)       strquestion += "}{";
                        else
                        {
                            strquestion += ")";
                        }

                        //Forth unknown
                        if (r[0] < 3)       strquestion += char3;
                        else if (r[0] < 6)  strquestion += char1;
                        else                strquestion += char2;

                        if (r[2] < 5) strquestion += "}";
                    }
                    break;

                //Factorize
                case (4):
                case (5):
                    {
                        for (int i = 0; i < 7; i++)
                            r[i] = (int)(rand[4].ToString()[i]) - 48;

                        if (r[1] < 1) r[1] = 1;
                        if (r[2] < 1) r[2] = 1;
                        if (r[3] < 1) r[3] = 1;

                        string str1 = "-", str2 = "-";
                        if (r[4] < 5)
                        {
                            str1 = "+";
                            r[3] *= -1;
                        }
                        else
                        {
                            str2 = "+";
                            r[1] *= -1;
                        }

                        int temp1 = r[0] * r[2];
                        int temp2 = (r[0] * r[3]) + (r[1] * r[2]);
                        int temp3 = r[1] * r[3];
                        int div = Math.Abs(Gcd(Gcd(temp1, temp2), temp3));
                        temp1 /= div;
                        temp2 /= div;
                        temp3 /= div;

                        //First
                        if (temp1 != 0 && temp1 != 1 && temp1 != -1) strquestion += temp1.ToString();
                        strquestion += char4 + "^2";

                        //Second
                        if (temp2 >= 0) strquestion += "+";
                        if (temp2 == -1) strquestion += "-";
                        if (temp2 != 0 && temp2 != 1 && temp2 != -1) strquestion += temp2.ToString();
                        strquestion += char4 + char5;

                        //Third
                        if (temp3 >= 0) strquestion += "+";
                        if (temp3 == -1) strquestion += "-";
                        if (temp3 != 0 && temp3 != 1 && temp3 != -1) strquestion += temp3.ToString();
                        strquestion += char5 + "^2";

                        if (num == 5)
                        {
                            if (r[6] < 2) r[6] = 2;
                            if (r[6] >= 5) r[6] *= -1;
                            if (r[5] < 5)
                            {
                                r[0] *= r[6];
                                r[1] *= r[6];
                                if (r[0] >= 0) strquestion += "+";
                                strquestion += r[0].ToString() + char4;
                                if (r[1] >= 0) strquestion += "+";
                                strquestion += r[1].ToString() + char5;
                            }
                            else
                            {
                                r[2] *= r[6];
                                r[3] *= r[6];
                                if (r[2] >= 0) strquestion += "+";
                                strquestion += r[2].ToString() + char4;
                                if (r[3] >= 0) strquestion += "+";
                                strquestion += r[3].ToString() + char5;
                            }
                        }
                    }
                    break;

            }
            return strquestion;
        }

        public static int Gcd(int a, int b) 
        {
            return b == 0 ? a : Gcd(b, a % b);
        }

        private void Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int iValue = -1;

            if (Int32.TryParse(textBox.Text, out iValue) == false)
            {
                TextChange textChange = e.Changes.ElementAt<TextChange>(0);
                int iAddedLength = textChange.AddedLength;
                int iOffset = textChange.Offset;

                textBox.Text = textBox.Text.Remove(iOffset, iAddedLength);
            }
        }
    }
}
