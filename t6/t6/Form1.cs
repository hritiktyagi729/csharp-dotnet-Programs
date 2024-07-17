using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace t6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string pdfpath = "";
        string imagepath = "";
        private void BrowsePdfBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.pdf)|*.pdf";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filename = openFileDialog.FileName;
                    pdfpath = filename;
                    axAcroPDF1.LoadFile(filename);
                    axAcroPDF1.setView("FitH");
                    

                    //return newImagePath;
                }
            }
        }

        private void BrowseImgBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.ImageLocation = openFileDialog.FileName;
                    imagepath = openFileDialog.FileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;


                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (imagepath == "")
            {
                MessageBox.Show("Please Select Image");
                return;
            }
            if (pdfpath == "")
            {
                MessageBox.Show("Please select pdf");
                return;
            }

           

            string impdf = System.IO.Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileNameWithoutExtension(imagepath) + ".pdf";
            string currentDirectory = System.IO.Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileNameWithoutExtension(imagepath) + "1.pdf";
            imgtopdf(imagepath, impdf);
           
            string pythonExePath = @"D:\visualstudioRepo\t6\t6\bin\Debug\pagereplace.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pythonExePath;
            // int PageNo = Convert.ToInt32(GetPageNumber(axAcroPDF1.Handle)) - 1;
            //CurrentPageNumber = PageNo + 1;
            int PageNo = 1;
            string currentPageNo = Convert.ToString(PageNo);
            //startInfo.Arguments = pdfpath + " " + pdfpath + " " + currentPageNo+ " " + currentDirectory ;
            //startInfo.Arguments = $" '{ pdfpath}'   '{pdfpath}'  {currentPageNo}  '{currentDirectory}'  ";
            string new_pdf = Path.Combine(Directory.GetCurrentDirectory(), Path.GetFileNameWithoutExtension(pdfpath) + "1.pdf");
            startInfo.Arguments = $"\"{pdfpath}\"  \"{new_pdf}\"  \"{currentPageNo}\"  \"{impdf}\"";
            // axAcroPDF1.g 

            // Optional: if you want to redirect standard output and error
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            try
            {
                // Start the process
                using (Process process = Process.Start(startInfo))
                {
                    // Read the output (if redirected)
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    // Wait for the process to exit
                    process.WaitForExit();

                    // Display the output (if any)
                    // MessageBox.Show("Output: " + output + "\nError: " + error);
                    MessageBox.Show("Image Replaced Succesfully");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            File.Delete(pdfpath);
            File.Move(new_pdf, pdfpath);
            axAcroPDF1.LoadFile(pdfpath);
            axAcroPDF1.setCurrentPage(PageNo + 1);
        }
        private int imgtopdf(string tiffimage, string pdfpath)
        {
            if (File.Exists(pdfpath))
            {
                return 1;
            }
            try
            {
                //string pdfpath = Path.GetDirectoryName(filePath);
                // string pdfpath =Application.StartupPath + "\\PDFTEMP";

                //Path.GetDirectoryName(Path.GetDirectoryName(tiffimage));
                try
                {

                    Bitmap bitmap1 = new Bitmap(tiffimage);
                }
                catch (Exception ex1)
                {
                    if (File.Exists(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage)))
                    {
                        File.Delete(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage));
                    }
                    Application.DoEvents();
                    // axImgEdit1.ClearDisplay();
                    Application.DoEvents();
                    // axImgEdit1.Image = tiffimage;
                    //  axImgEdit1.Display();
                    //  axImgEdit1.SaveAs(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage), axImgEdit1.FileType, axImgEdit1.PageType, 9, 32, false);
                    Application.DoEvents();
                    Application.DoEvents();
                    //   axImgEdit1.ClearDisplay();
                    Application.DoEvents();
                    if (File.Exists(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage)))
                    {
                        File.Copy(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage), tiffimage, true);
                    }
                    Application.DoEvents();
                    Application.DoEvents();
                    //axImgEdit1.Save();
                    //fileListBox1.Refresh();
                    //fileListBox1.SelectedIndex = intindex + 1;

                    //MagickReadSettings settings = new MagickReadSettings();
                    //settings.Compression = CompressionMethod.LZW;
                    //using (MagickImage image = new MagickImage(tiffimage, settings))
                    //{

                    //    image.Read(tiffimage.ToString());
                    //    string curroptPath = Path.GetDirectoryName(tiffimage);
                    //    string curroptfilename = Path.GetFileName(tiffimage);
                    //    image.Write(curroptPath + "\\" + curroptfilename + ".tif", ImageMagick.MagickFormat.Tiff);

                    //    Application.DoEvents();
                    //}
                }
                Bitmap bitmap = new Bitmap(tiffimage);

                Application.DoEvents();
                //if (!Directory.Exists(pdfpath + ""))
                //{
                //    Directory.CreateDirectory(pdfpath + "");
                //}
                Application.DoEvents();
                float scale1 = 72f / bitmap.HorizontalResolution;

                int width1 = (int)(bitmap.Width * scale1);
                int height1 = (int)(bitmap.Height * scale1);

                var pgSize = new iTextSharp.text.Rectangle(width1, height1);

                Document document = new Document(pgSize, 50, 50, 50, 50);
                //if (File.Exists(pdfpath + "\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf"))
                //{
                //    File.Delete(pdfpath + "\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf");
                //}
                Application.DoEvents();

                //PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfpath + "\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf", FileMode.CreateNew));
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfpath, FileMode.CreateNew));

                //writer.SetFullCompression();
                //writer.CompressionLevel = PdfStream.BEST_COMPRESSION;
                writer.PdfVersion = PdfWriter.VERSION_1_6;
                document.Open();
                Application.DoEvents();
                int numberOfPages = bitmap.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
                Application.DoEvents();
                PdfContentByte cb = writer.DirectContent;

                for (int page = 0; page < numberOfPages; page++)
                {
                    try
                    {
                        Application.DoEvents();
                        bitmap.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, page);
                        //bitmap.Save(@"d:\12.tif", System.Drawing.Imaging.ImageFormat.Tiff);
                        //Bitmap bmp = (Bitmap)System.Drawing.Image.FromFile(@"d:\12.tif");
                        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
                        ImageCodecInfo ici = null;
                        EncoderParameters ep = new EncoderParameters();
                        System.IO.MemoryStream stream = new System.IO.MemoryStream();
                        if (bitmap.PixelFormat.ToString() == "Format1bppIndexed")
                        {
                            foreach (ImageCodecInfo codec in codecs)
                            {
                                if (codec.MimeType == "image/png")
                                    ici = codec;
                            }
                            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)50);
                        }
                        else
                        {
                            foreach (ImageCodecInfo codec in codecs)
                            {
                                Application.DoEvents();

                                if (codec.MimeType == "image/jpeg")
                                {
                                    ici = codec;
                                }
                                //else  if (codec.MimeType == "image/tiff")
                                //  {
                                //      ici = codec;
                                //  }
                                //else if (codec.MimeType == "image/jpg")
                                //{
                                //    ici = codec;
                                //}
                                //else if (codec.MimeType == "image/bmp")
                                //{
                                //    ici = codec;
                                //}
                            }
                            Application.DoEvents();
                            ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)50);
                        }


                        //bitmap.SetResolution(200, 200);
                        //GC.Collect();
                        //GC.WaitForPendingFinalizers();
                        //bitmap.SaveAdd(bitmap, ep);
                        //bitmap.Save(@"d:\123.tif", ici, ep);
                        //bitmap.Save(@"d:\123.jpg", ici, ep);
                        bitmap.Save(stream, ici, ep);



                        //bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png );

                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(stream.ToArray());
                        //writer.SetFullCompression();

                        stream.Close();
                        Application.DoEvents();
                        Application.DoEvents();
                        Application.DoEvents();
                        //img.ScalePercent(72f);
                        //iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(img.Width, img.Height);
                        //pageSize = rect.GetRectangle(jpg.Width, jpg.Height);
                        float scale = 72f / 300f;// bitmap.HorizontalResolution;

                        int width = (int)(img.Width * scale);
                        int height = (int)(img.Height * scale);

                        iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(width1, height1);

                        document.SetPageSize(pageSize);
                        document.SetMargins(0, 0, 0, 0);
                        img.SetAbsolutePosition(0, 0);
                        img.ScaleToFit(document.PageSize.Width, document.PageSize.Height);


                        document.NewPage();
                        cb.AddImage(img);
                        document.Close();
                        bitmap.Dispose();
                        //bmp.Dispose();
                    }
                    catch (Exception ex)
                    {
                        // msg = ex.Message.ToString();
                        //using (StreamWriter wrioter = new StreamWriter(filePath, true))
                        //{
                        //    wrioter.WriteLine("Tiff-" + tiffimage + "Message :" + ex.Message + "<br/>" + Environment.NewLine);
                        return 0;
                        //}
                    }
                    return 1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                //using (StreamWriter wrioter = new StreamWriter(filePath, true))
                //{
                //    wrioter.WriteLine(ex.Message + "<br/>" + Environment.NewLine);
                return 0;
                //}
            }

        }
    }
}
