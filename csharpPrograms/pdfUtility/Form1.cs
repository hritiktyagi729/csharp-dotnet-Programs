using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using Ghostscript.NET.Rasterizer;



namespace DMSUtility
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            rdoIMGwComp.Visible = false;
            rdoIMGCompressed.Visible = false;
            rdoSearchableComp.Visible = false;
            rdoCompressedPDF.Visible = false;
            rdoJPGtoTiff.Visible = false;
            pb1.Visible = false;
            pb2.Visible = false;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void processJPG(string input_path, string output_path)
        {
            string[] imgs = Directory.GetFiles(input_path, "*.jpg");
            for (int i = 0; i < imgs.Length; i++)
            {
                //lblStatus.Text = "Working on- " + (i + 1).ToString() + "/" + imgs.Length.ToString();

                axImgEdit1.ClearDisplay(); Application.DoEvents();
                axImgEdit1.Image = imgs[i];
               axImgEdit1.Display();
                Application.DoEvents();
                //if (!Directory.Exists(textBox2.Text + "\\OUTPUT"))
                //{
                //    Directory.CreateDirectory(textBox2.Text + "\\OUTPUT");
                //    Application.DoEvents();
                //}
                if (axImgEdit1.FileType == 6)
                {
                    if (axImgEdit1.PageType == 6)
                    {
                        axImgEdit1.SaveAs(output_path+"\\" + Path.GetFileNameWithoutExtension(imgs[i]) + ".tif", 6, 3, 6, 2048, false);
                    }
                }
                Application.DoEvents();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (axImgEdit1.PageType == 3)
                {
                    axImgEdit1.SaveAs(output_path+"\\" + Path.GetFileNameWithoutExtension(imgs[i]) + ".tif", 6, 3, 6, 2048, false);
                    //axImgEdit1.SaveAs("c:\\scan\\docc" + Strings.Format(totalscan, "0000") + ".jpg", 6, 3, 6, 1024, false);
                }
                //else if (axImgEdit1.PageType == 6)
                //{
                //    axImgEdit1.SaveAs(textBox2.Text + "\\OUTPUT\\" + ".tif", axImgEdit1.FileType, axImgEdit1.PageType, 9, 32, false);
                //}
                else
                {
                    //axImgEdit1.SaveAs("c:\\scan\\docc" + Strings.Format(totalscan, "0000") + ".jpg", 6, axImgEdit1.PageType, 6, 1024, false);
                    axImgEdit1.SaveAs(output_path+ "\\" + Path.GetFileNameWithoutExtension(imgs[i]) + ".tif", axImgEdit1.FileType, axImgEdit1.PageType, axImgEdit1.CompressionType, axImgEdit1.CompressionInfo, false);
                }
                Application.DoEvents();
            }
            Application.DoEvents();
           // createpdf(textBox1.Text + "\\OUTPUT\\");
            Application.DoEvents();
            //string pdfpath = CreateMultiTiff(Application.StartupPath + "\\PDFTEMP\\", textBox2.Text + "\\" + Path.GetFileName(fold) + ".pdf");
            //pb2.Visible = false;
           // lblStatus.Text = "Done";

        }

        private void processmultiJPG()
        {
            string input_path =textBox1.Text;
            string output_path = "";
            string[] f;
            //f= Directory.GetFiles(textBox1.Text);
            f = Directory.GetDirectories(textBox1.Text);
                               
            for (int i = 0; i < f.Length; i++)
            {
                
                if (!Directory.Exists(textBox2.Text + f[i]))
                {
                    Directory.CreateDirectory(textBox2.Text+ "\\" + Path.GetFileNameWithoutExtension(f[i]));
                    Application.DoEvents();
                }

                input_path = textBox1.Text + "\\" + Path.GetFileNameWithoutExtension(f[i]);
                output_path = textBox2.Text + "\\"+ Path.GetFileNameWithoutExtension(f[i]);
                processJPG(input_path, output_path);
            }
        }

        private void btn_strt_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Plz Select Input Path", "Images", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Plz Select Output Path", "PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //MargeMultiplePDF(textBox1.Text, textBox2.Text + "\\" + Path.GetFileName ( textBox2.Text) + ".pdf");
            ConvertFolder(textBox1.Text.ToString().Trim());

           // MessageBox.Show("Process Completed.........", "Converter", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ConvertFolder(string selectedPath)
        {
            string[] folders = Directory.GetDirectories(selectedPath);
            string[] folders1 = new string[] { selectedPath };
            if (folders.Length == 0)
            {
                folders = folders1;
            }
            pb1.Minimum = 0;
            pb1.Maximum = folders.Length;

            int i = 1;
            foreach (string fold in folders)
            {
                if (fold.ToLower().Contains("done") || fold.ToLower().Contains("delete"))
                    continue;

                if (Directory.GetDirectories(fold.ToString()).Length > 0)
                {
                    ConvertFolder(fold.ToString());
                }

                label3.Text = "Working On :- " + Path.GetFileName(fold);

                //new
                if (rdoIMGwComp.Checked == true)
                {
                    CreatepdfWComp(fold);
                    pb2.Visible = false;
                    label4.Text = "Done";
                }
                else if (rdoIMGCompressed.Checked == true)
                {
                    createpdf(fold);
                    pb2.Visible = false;
                    label4.Text = "Done";
                }
                else if (rdoSearchableComp.Checked == true)
                {
                    Application.DoEvents();
                    if (Directory.Exists(Application.StartupPath + "\\PDFTEMP"))
                    {
                    L:
                        try
                        {
                            Directory.Delete(Application.StartupPath + "\\PDFTEMP", true);
                            Application.DoEvents();

                        }
                        catch (Exception ex)
                        {
                            goto L;
                        }
                    }
                    Directory.CreateDirectory(Application.StartupPath + "\\PDFTEMP");
                    Application.DoEvents();
                    if (Directory.Exists(Application.StartupPath + "\\temp"))
                    {
                    L:
                        try
                        {
                            Directory.Delete(Application.StartupPath + "\\temp", true);
                            Application.DoEvents();
                        }
                        catch (Exception ex)
                        {
                            goto L;
                        }
                    }
                    Directory.CreateDirectory(Application.StartupPath + "\\temp");
                    Application.DoEvents();
                    Application.DoEvents();
                    var filteredFiles = Directory
                                            .EnumerateFiles(fold) //<--- .NET 4.5
                                            .Where(file => file.ToLower().EndsWith("jpg") || file.ToLower().EndsWith("tif") || file.ToLower().EndsWith("pdf"))
                                            .ToList();

                    string[] sub_folder = filteredFiles.ToArray();// System.IO.Directory.GetFiles(fold, "*.jpg;*.tif");
                    Boolean isPDF = false;
                    if (sub_folder.Length > 0)
                    {
                        pb2.Maximum = sub_folder.Length + 1;
                        pb1.Minimum = 0;
                        pb2.Value = 1;

                        for (int m = 0; m < sub_folder.Length; m++)
                        {
                            isPDF = false;
                            if (pb2.Value < pb2.Maximum)
                                pb2.Value = Convert.ToInt32(pb2.Value) + 1;
                            label3.Text = "Working On :- " + sub_folder[m];
                            pb2.Visible = true;
                            label4.Text = "Converting Images to Searchable PDF....... " + (m + 1).ToString() + "/" + sub_folder.Length.ToString();
                            Application.DoEvents();
                            if (Path.GetExtension(sub_folder[m]).ToLower() == ".pdf")
                            {
                                breakIntoImages(sub_folder[m]);
                                string[] imgs = Directory.GetFiles(Application.StartupPath + "\\temp\\", "*.jpg");
                                for (int n = 0; n < imgs.Length; n++)
                                {
                                    convrteSearchablePDF(imgs[n], Application.StartupPath + "\\pdftemp\\");
                                }
                                Application.DoEvents();
                                if (!Directory.Exists(textBox2.Text))
                                {
                                    Directory.CreateDirectory(textBox2.Text);
                                }
                                Application.DoEvents();
                                string pdfpath = CreateMultiTiff(Application.StartupPath + "\\pdftemp\\", textBox2.Text + "\\" + Path.GetFileName(sub_folder[m]));
                                label4.Text = "Compressing and validating the size of PDF, Please wait....";
                                Application.DoEvents();
                                if (pdfpath != "0")
                                {
                                    PdfReader pdfr = new PdfReader(pdfpath);
                                    int pages = pdfr.NumberOfPages;
                                    pdfr.Close();

                                    compressPDF(pdfpath, pages);
                                }

                                isPDF = true;
                            }
                            else
                            {
                                convrteSearchablePDF(sub_folder[m], fold);
                            }

                            Application.DoEvents();
                        }
                        if (isPDF == false)
                        {
                            Application.DoEvents();
                            if (!Directory.Exists(textBox2.Text))
                            {
                                Directory.CreateDirectory(textBox2.Text);
                            }
                            Application.DoEvents();
                            string pdfpath = CreateMultiTiff(Application.StartupPath + "\\PDFTEMP\\", textBox2.Text + "\\" + Path.GetFileName(fold).ToUpper());
                            label4.Text = "Compressing and validating the size of PDF, Please wait....";
                            Application.DoEvents();
                            compressPDF(pdfpath, sub_folder.Length);

                            //int pdfSize = 20;

                            //var fileLength = new FileInfo(pdfpath);
                            //long size = fileLength.Length;
                            //decimal mb_size = (decimal)size / (1024 * 1024);
                            //if (mb_size > pdfSize)
                            //{
                            //    clsSplitPDF.BreakAndSavePDFs(pdfpath, Path.GetDirectoryName(pdfpath),1, pdfSize);
                            //}
                        }


                    }
                    pb2.Visible = false;
                    label4.Text = "Done";
                }
               

                
                i = i + 1;
            }
            if (rdoJPGtoTiff.Checked == true)
            {
                processmultiJPG();
                pb2.Visible = false;
                label4.Text = "Done";
            }
            if (rdoCompressedPDF.Checked == true)
            {
                string[] sub_folders = Directory.GetDirectories(textBox1.Text);
                if (sub_folders.Length > 0)
                {
                    for (int j = 0; j < sub_folders.Length; j++)  // Corrected loop variable
                    {
                        string input_folderPath = sub_folders[j];
                        string[] input_files = Directory.GetFiles(input_folderPath);
                        string output_folderPath = Path.Combine(textBox2.Text, Path.GetFileName(sub_folders[j]));

                        if (!Directory.Exists(output_folderPath))
                        {
                            Directory.CreateDirectory(output_folderPath);
                        }

                        foreach (string input_file in input_files)
                        {
                            string out_pdf = Path.Combine(output_folderPath, Path.GetFileName(input_file));
                            if (Path.GetExtension(out_pdf).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                            {
                                PDFCompressor(input_file, out_pdf);
                            }
                        }
                        // Removed manual garbage collection calls
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }
            if (impdfToSearch.Checked == true)
            {

                string[] sub_folders = Directory.GetFiles(textBox1.Text);
                if (sub_folders.Length > 0)
                {
                    for (int j = 0; j < sub_folders.Length; j++)
                    {
                        //string images_outpath = Path.Combine(Directory.GetCurrentDirectory()+"\\"+ Path.GetFileNameWithoutExtension(textBox1.Text) + "images\\", Path.GetFileNameWithoutExtension(sub_folders[j]).Replace("_",""));
                        string images_outpath = Path.Combine(Directory.GetCurrentDirectory() + "\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "images\\", j.ToString());

                        if (!Directory.Exists(images_outpath))
                        {
                            Directory.CreateDirectory(images_outpath);
                        }

                        label4.Text = "working on pdf: " + Path.GetFileNameWithoutExtension(sub_folders[j]);
                        
                        // convert pdf to images
                        ConvertPDFToImages(sub_folders[j], images_outpath);
                        // public int convrteSearchablePDF(string tiffimage, string filePath)
                        string[] pdfImages = Directory.GetFiles(images_outpath);
                        foreach (string pdfimage in pdfImages)
                        {
                            string a = Path.GetFileNameWithoutExtension(pdfimage).Replace(" ", "");
                            a = a.Replace("-", "");
                            //string pdf_images_outpath = Path.Combine(Directory.GetCurrentDirectory()+"\\"+ Path.GetFileNameWithoutExtension(textBox1.Text) + "searchable\\", (Path.GetFileNameWithoutExtension(sub_folders[j]).Replace("_", "")).Replace(" ", "") );
                            string pdf_images_outpath = Path.Combine(Directory.GetCurrentDirectory() + "\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "searchable\\", j.ToString());

                            if (!Directory.Exists(pdf_images_outpath))
                            {
                                Directory.CreateDirectory(pdf_images_outpath);
                            }
                            string pdf_outpath = pdf_images_outpath + "\\" + (Path.GetFileNameWithoutExtension(pdfimage).Replace("_","")).Replace(" ","") + ".pdf";
                           // string pdf_outpath = pdf_images_outpath + "\\" + j + ".pdf";
                            Application.DoEvents();
                            Application.DoEvents();
                            Application.DoEvents();
                            label4.Text = "working on pdf: " + Path.GetFileNameWithoutExtension(sub_folders[j]);
                            label3.Text = "working on page " + Path.GetFileNameWithoutExtension( pdfimage);
                            convrteSearchablePDF(pdfimage , pdf_outpath);
                            
                            Application.DoEvents();
                            Application.DoEvents();
                            Application.DoEvents();
                            Application.DoEvents();
                        }
                        // merge one folder pdf's into one pdf
                        string pdfFolders = Directory.GetCurrentDirectory()+ "\\satya_pdfssearchable\\";
                       // string[] pdf_folders_list = Directory.GetDirectories(pdfFolders);

                       // string current_Dir = Directory.GetCurrentDirectory();
                        
                        
                            //string in_s_pdf = Directory.GetCurrentDirectory()+ "\\tempsearchable\\" +Path.GetFileNameWithoutExtension( pdf_folder);

                            string out_s_pdf = textBox2.Text +"\\"+ Path.GetFileNameWithoutExtension(sub_folders[j]) + ".pdf";
                            //merge into one pdf function
                            MergePdfs(Directory.GetCurrentDirectory()+"\\pdfsearchable\\"+ j, out_s_pdf);
                        
                        
                        // Removed manual garbage collection calls
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                   //    File.Delete(Directory.GetCurrentDirectory() + "\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "images");
                    //    File.Delete(Directory.GetCurrentDirectory() + "\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "searchable");
                    }
                    //convrteSearchablePDF(images_outpath, );

                }
                label4.Text = "Done";
                label3.Text = "";
                //remove temporary files
                //   File.Delete(Directory.GetCurrentDirectory() + Path.GetFileNameWithoutExtension(textBox1.Text) + "\\searchable\\);
                //   File.Delete(Directory.GetCurrentDirectory() + Path.GetFileNameWithoutExtension(textBox1.Text) + "_images\\");


            }
            string temp_image_path= Directory.GetCurrentDirectory() +"\\" +Path.GetFileNameWithoutExtension(textBox1.Text) + "searchable";
            Directory.Delete(temp_image_path,true);
            Directory.Delete(Directory.GetCurrentDirectory() +"\\" + Path.GetFileNameWithoutExtension(textBox1.Text) + "images",true);
        }

        public static void MergePdfs(string inputDirectory, string outputFile)
        {
            // Get all PDF files in the input directory
            string[] pdfFiles = Directory.GetFiles(inputDirectory, "*.pdf");

            // Create a file stream for the output file
            FileStream stream = new FileStream(outputFile, FileMode.Create);

            // Create a document and a PDF copy writer
            Document document = new Document();
            PdfCopy pdfCopy = new PdfCopy(document, stream);

            try
            {
                document.Open();

                // Iterate through all PDF files
                foreach (var file in pdfFiles)
                {
                    PdfReader reader = new PdfReader(file);
                    try
                    {
                        // Copy each page from the current PDF
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            PdfImportedPage page = pdfCopy.GetImportedPage(reader, i);
                            pdfCopy.AddPage(page);
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            finally
            {
                document.Close();
                stream.Close();
            }
        }




            public  void ConvertPDFToImages(string inputPdfPath, string outputDirectory)
        {
            PdfReader reader = null;
            try
            {
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                reader = new PdfReader(inputPdfPath);
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    label3.Text = "Extracting Images: " + page;
                    var pdfPage = reader.GetPageN(page);
                    var resources = pdfPage.GetAsDict(PdfName.RESOURCES);
                    var xObject = resources.GetAsDict(PdfName.XOBJECT);

                    if (xObject != null)
                    {
                        foreach (PdfName key in xObject.Keys)
                        {
                            var obj = xObject.GetAsIndirectObject(key);
                            if (obj != null && obj.IsIndirect())
                            {
                                var directObject = PdfReader.GetPdfObject(obj) as PdfDictionary;
                                if (directObject == null) continue;

                                var subtype = directObject.Get(PdfName.SUBTYPE) as PdfName;

                                if (subtype != null && subtype.Equals(PdfName.IMAGE))
                                {
                                    var stream = (PdfStream)directObject;
                                    var imageBytes = PdfReader.GetStreamBytesRaw((PRStream)stream);
                                    using (var ms = new MemoryStream(imageBytes))
                                    {
                                        using (var bitmap = new Bitmap(ms))
                                        {
                                            var outputPath = Path.Combine(outputDirectory, $"page_{page}.jpg");
                                            bitmap.Save(outputPath);
                                            GC.Collect();
                                            GC.Collect();


                                            GC.Collect();

                                            GC.Collect();
                                            GC.Collect();
                                            GC.Collect();


                                            GC.Collect();

                                            GC.Collect();
                                            GC.WaitForPendingFinalizers();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Images extracted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting images: {ex.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }




        //public static void ConvertPDFToImages(string inputPdfPath, string outputDirectory)
        //{
        //    try
        //    {
        //        using (var reader = new PdfReader(inputPdfPath))
        //        {
        //            for (int page = 1; page <= reader.NumberOfPages; page++)
        //            {
        //                var pdfPage = reader.GetPageN(page);
        //                var resources = pdfPage.GetAsDict(PdfName.RESOURCES);
        //                var xObject = resources.GetAsDict(PdfName.XOBJECT);

        //                if (xObject != null)
        //                {
        //                    foreach (var key in xObject.Keys)
        //                    {
        //                        var obj = xObject.GetAsIndirectObject(key);
        //                        if (obj is PdfStream stream)
        //                        {
        //                            var subtype = stream.Get(PdfName.SUBTYPE);
        //                            if (subtype != null && subtype.Equals(PdfName.IMAGE))
        //                            {
        //                                var image = iTextSharp.text.Image.GetInstance(stream);
        //                                var outputPath = Path.Combine(outputDirectory, $"page_{page}_image_{key}.png");
        //                                using (var fileStream = new FileStream(outputPath, FileMode.Create))
        //                                {
        //                                    image.Save(fileStream, System.Drawing.Imaging.ImageFormat.Png);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        Console.WriteLine("Images extracted successfully!");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error extracting images: {ex.Message}");
        //    }
        //}
        //public  void ConvertPdfPageToImage(string inputPdfPath, string outputImageDirectory)
        //{
        //    using (var rasterizer = new GhostscriptRasterizer())
        //    {
        //        rasterizer.Open(inputPdfPath);
        //        int dpi = 300; // You can change the DPI for better quality
        //        for (int pageNumber = 1; pageNumber <= rasterizer.PageCount; pageNumber++)
        //        {
        //            var img = rasterizer.GetPage(dpi, pageNumber);

        //            string outputImagePath = Path.Combine(outputImageDirectory, $"page_{pageNumber}.jpg");
        //            img.Save(outputImagePath, ImageFormat.Jpeg);

        //            Console.WriteLine($"Page {pageNumber} converted to image.");
        //        }
        //    }
        //}
        //static void ExtractAllImages(string pdfpath, string imagefolder)
        //{
        //    string path = "";
        //    using (PdfDocument pdf = new PdfDocument(path))
        //    {
        //        for (int i = 0; i < pdf.Images.Count; i++)
        //        {
        //            string imageName = string.Format("image{0}", i);
        //            string imagePath = pdf.Images[i].Save(imageName);
        //        }
        //    }
        //}



        //public static void ExtractImagesFromPDF(string sourcePdf, string outputDirectory)
        //{
        //    if (!Directory.Exists(outputDirectory))
        //    {
        //        Directory.CreateDirectory(outputDirectory);
        //    }

        //    var pdf = new PdfReader(sourcePdf);

        //    try
        //    {
        //        for (int pageNum = 1; pageNum <= pdf.NumberOfPages; pageNum++)
        //        {
        //            PdfDictionary pg = pdf.GetPageN(pageNum);
        //            int imageIndex = 1;
        //            ExtractImagesFromPDF_FindImagesInPDFDictionary(pg, pdf, images =>
        //            {
        //                foreach (var img in images)
        //                {
        //                    string outputImagePath = Path.Combine(outputDirectory, $"page_{pageNum}_image_{imageIndex}.png");
        //                    img.Save(outputImagePath, System.Drawing.Imaging.ImageFormat.Png);
        //                    imageIndex++;
        //                }
        //            });
        //        }
        //    }
        //    finally
        //    {
        //        pdf.Close();
        //    }
        //}

        //private static void ExtractImagesFromPDF_FindImagesInPDFDictionary(PdfDictionary pg, PdfReader pdf, Action<IEnumerable<Image>> foundImages)
        //{
        //    List<System.Drawing.Image> images = new List<System.Drawing.Image>();

        //    foreach (var key in pg.Keys)
        //    {
        //        PdfObject obj = pg.Get(key);

        //        if (obj == null || !obj.IsIndirect())
        //        {
        //            continue;
        //        }

        //        PdfDictionary dictionary = (PdfDictionary)PdfReader.GetPdfObject(obj);

        //        PdfName subType = (PdfName)PdfReader.GetPdfObject(dictionary.Get(PdfName.SUBTYPE));
        //        if (PdfName.IMAGE.Equals(subType))
        //        {
        //            int XrefIndex = Convert.ToInt32(((PRIndirectReference)obj).Number.ToString(CultureInfo.InvariantCulture));
        //            PdfObject pdfObj = pdf.GetPdfObject(XrefIndex);
        //            if (pdfObj is PRStream stream)
        //            {
        //                try
        //                {
        //                    var parser = new PdfImageObject(stream);
        //                    var image = parser.GetDrawingImage();
        //                    images.Add(image);
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"Error extracting image: {ex.Message}");
        //                }
        //            }
        //        }
        //        else if (PdfName.FORM.Equals(subType) || PdfName.GROUP.Equals(subType))
        //        {
        //            ExtractImagesFromPDF_FindImagesInPDFDictionary(dictionary, pdf, foundImages);
        //        }
        //    }

        //    if (images.Count > 0)
        //    {
        //        foundImages(images);
        //    }
        //}



        public static void PDFCompressor(string inputPdfPath, string outputPdfPath)
        {
            FileStream fs = null;
            PdfReader reader = null;
            PdfStamper stamper = null;

            try
            {
                fs = new FileStream(outputPdfPath, FileMode.Create);
                reader = new PdfReader(inputPdfPath);
                stamper = new PdfStamper(reader, fs);

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfDictionary page = reader.GetPageN(i);
                    PdfArray annotations = page.GetAsArray(PdfName.ANNOTS);
                    if (annotations != null)
                    {
                        foreach (PdfObject annotation in annotations.ArrayList)
                        {
                            PdfDictionary annotDict = (PdfDictionary)PdfReader.GetPdfObject(annotation);
                            if (annotDict != null && annotDict.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                            {
                                PdfObject pdfObject = annotDict.Get(PdfName.CONTENTS);
                                if (pdfObject != null && pdfObject.IsStream())
                                {
                                    PRStream stream = (PRStream)pdfObject;

                                    // Get the image bytes
                                    byte[] imgData = PdfReader.GetStreamBytes(stream);

                                    // Create an Image instance
                                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imgData);

                                    // Set image compression level and options
                                    image.CompressionLevel = 6; // Set the compression level (0-9)

                                    // Update the stream with the modified image data
                                    stream.SetData(image.RawData);
                                }
                            }
                        }
                    }
                }

                // Enable full compression
                stamper.Writer.SetFullCompression();

                // Remove unused objects from the PDF
                stamper.Writer.Flush();
                stamper.Writer.CloseStream = false;
                stamper.Writer.FreeReader(reader);

                // Save the modified PDF
                stamper.Close();
            }
            finally
            {
                if (stamper != null)
                {
                    stamper.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        private void breakIntoImages(string pdfName)
        {
            DEL:
            try
            {
                if (Directory.Exists(Application.StartupPath + "\\temp\\"))
                {
                    Directory.Delete (Application.StartupPath + "\\temp\\",true );
                    Application.DoEvents();
                }
                Application.DoEvents();
                Directory.CreateDirectory(Application.StartupPath + "\\temp\\");
                    Application.DoEvents();
            }
            catch (Exception)
            {

                goto DEL;
            }
            
            PdfReader pdfr = new PdfReader(pdfName);
            int pages = pdfr.NumberOfPages;
            pdfr.Close();
            for (int i = 0; i < pages; i++)
            {
                string appPath = '"' + Application.StartupPath + "\\GhostScript\\gswin32c.exe" + '"';
                string Language = "eng";
                string input = '"' + pdfName + '"';
                string oArg = '"' + Application.StartupPath + "\\temp\\" + (i+1).ToString() + ".jpg" +  '"';

                string commandArgs = String.Concat("-dNOPAUSE -q -r300 -sDEVICE=jpeg -dBATCH -dFirstPage=", (i+1).ToString(), " -dLastPage=", (i+1).ToString(), " -sOutputFile=" + oArg + " " + input + " -c quit");

                //MessageBox.Show(GetProgramPath("Tesseract-OCR", "tesseract.exe"));
                ProcessCommand(appPath, commandArgs);

            }
            
            
            //string command = String.Concat("-dNOPAUSE -q -r300 -sDEVICE=bmp256 -dBATCH -dFirstPage=", StartPageNum.ToString(), " -dLastPage=", EndPageNum.ToString(), " -sOutputFile=" + OutPut + " " + PDF + " -c quit");
            //string command = 



            //return new FileInfo(OutPut.Replace('"', ' ').Trim()).FullName;

        }
        public void createpdf(string path)
        {
            string fold_name = "";
            int num = 1;
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileSystemInfo[] allFiles = dirInfo.GetFileSystemInfos();
                var orderedFiles = allFiles.OrderBy(f => f.Name);

                string[] files = Directory.GetFiles(path, "*.*");
                //int[] fls=new int [files.Length]; 
                //for (int k=0;k<files.Length;k++ )
                //{
                //    fls[k] =Convert.ToInt16(Path.GetFileName(files[k]).Replace (".jpg",""));
                //}
                //Array.Sort(fls);
                //for (int k = 0; k < fls.Length; k++)
                //{
                //    files[k] = path + "\\" + fls[k] + ".jpg";
                //}
                int cnt = 0;
                if (!Directory.Exists(textBox2.Text))
                {
                    Directory.CreateDirectory(textBox2.Text);
                }
                foreach (string  fl in files)
                {
                    if (Path.GetExtension(fl) == ".jpg" || Path.GetExtension(fl) == ".tif" || Path.GetExtension(fl) == ".JPG" || Path.GetExtension(fl) == ".TIF")
                    {
                        cnt = cnt + 1;
                    }
                }

                if (cnt == 0)
                {
                    return;
                }
                pb2.Value = 1;
                pb2.Minimum = 1;
                pb2.Maximum = files.Length * 2;
                int cnt_ = 0;
                Document document = new Document();
                iTextSharp.text.Rectangle pageSize = null;
                //RasterCodecs cod = new RasterCodecs();
                //"\\" + Path.GetFileName(path) +
                using (var stream = new FileStream(textBox2.Text + "\\" + Path.GetFileName(path) +  ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))//change by Ashutosh
                {
                    PdfWriter.GetInstance(document, stream);

                    document.Open();

                foreach (string file in files)
                {
                    label4.Text = "Converting Images to PDF...............";
                    pb2.Increment(1);
                    if (Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".tif" || Path.GetExtension(file) == ".JPG" || Path.GetExtension(file) == ".TIF")
                    {

                        cnt_++;
                        using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            Application.DoEvents();
                                Bitmap bitmap = new Bitmap(file);

                                double scale = 72d / bitmap.HorizontalResolution;
                                int width1 = (int)(bitmap.Width * scale);
                                int height1 = (int)(bitmap.Height * scale);

                                Bitmap b0 = new Bitmap(bitmap);

                                scale = 120d / bitmap.HorizontalResolution;
                                int width = (int)(b0.Width * scale);
                                int height = (int)(b0.Height * scale);
                                Bitmap bmpScaled = new Bitmap(b0, width, height);
                                b0.Dispose();
                                //bmpScaled.Dispose();
                                bitmap = new Bitmap(bmpScaled);
                                MemoryStream memstream = new MemoryStream();
                                bitmap.Save(memstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                                

                                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(memstream.ToArray());
                                bitmap.Dispose();
                                bmpScaled.Dispose();
                                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(stream.ToArray());

                                jpg.CompressionLevel = iTextSharp.text.Image.JPEG;
                                //jpg.SetDpi(72, 72);
                                
                                jpg.ScaleAbsoluteHeight(height);
                                jpg.ScaleAbsoluteWidth(width);
                                jpg.SetDpi(72, 72);
                                Application.DoEvents(); 
                                var image = iTextSharp.text.Image.GetInstance(jpg);
                            jpg.Alignment = Element.ALIGN_CENTER;
                                //jpg.SetDpi(300, 300);
                                //jpg.ScalePercent(72f);
                                jpg.ScaleToFit(width1, height1);
                                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(width1, height1);
                                //pageSize = rect.GetRectangle(jpg.Width, jpg.Height);
                                pageSize = new iTextSharp.text.Rectangle(width1, height1);
                                document.SetPageSize(pageSize);
                                document.SetMargins(0, 0, 0, 0);
                            document.NewPage();
                            document.Add(jpg);
                        }

                        //fold_name = path.Substring(path.LastIndexOf(@"\") + 1, path.Length - path.LastIndexOf(@"\") - 1);

                        //Application.DoEvents();
                        //Application.DoEvents();
                        //RasterImage ri = cod.Load(file);
                        //Application.DoEvents();
                        //Application.DoEvents();
                        //RasterImageFormat fi = ri.OriginalFormat;
                        ////cod.Save(ri, path+"\\" + fold_name+".pdf", RasterImageFormat.RasPdfJpeg422, 0, 1, 1, -1, CodecsSavePageMode.Append);
                        //if (num == 1)
                        //{
                        //    cod.Save(ri, path + "\\" + fold_name + ".pdf", RasterImageFormat.RasPdf, 0);
                        //}
                        //else
                        //{
                        //    Application.DoEvents();
                        //    Application.DoEvents();
                        //    cod.Save(ri, path + "\\" + fold_name + ".pdf", RasterImageFormat.RasPdf, 0, 1, 1, -1, CodecsSavePageMode.Append);
                        //}
                        num++;
                        Application.DoEvents();
                        Application.DoEvents();
                        //CreateMultiPageRasterPdfFile("C:\\123.pdf",file);

                    }
                }
                document.Close();
                    //pdfa(path + "\\" + fold_name + ".pdf");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public int convrteSearchablePDF(string tiffimage, string filePath)
        {
            try
            {
                //string pdfpath = Path.GetDirectoryName(filePath);
                string pdfpath = filePath;
                //Path.GetDirectoryName(Path.GetDirectoryName(tiffimage));


                Application.DoEvents();
                //if (Directory.Exists(pdfpath + "\\PDFTEMP"))
                //{
                //    Directory.Delete(pdfpath + "\\PDFTEMP");
                //}
                
                
                //Bitmap bitmap = new Bitmap(tiffimage);

                //MagickReadSettings settings = new MagickReadSettings();

                ////settings.ColorSpace = ImageMagick.ColorSpace.Gray;
                //settings.Density = new Density(300, 300, DensityUnit.PixelsPerInch);
                //using (MagickImage image = new MagickImage())
                //{

                //    // Reads the eps image, the specified settings tell Ghostscript to create an sRGB image
                //    image.Read(tiffimage.ToString());
                //    image.Density =new Density(300, 300, DensityUnit.PixelsPerInch);
                //    // Save image as tiff
                //    image.Write(Application.StartupPath + "\\temp\\" +  Path.GetFileName(tiffimage));
                //    Application.DoEvents();
                //}
                GC.Collect();
                GC.WaitForPendingFinalizers();
                //if (tiffimage != Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage))
                //{
                //    File.Copy(tiffimage, Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage));
                //    Application.DoEvents();
                //}
                //if (File.Exists(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage)))
                //{
                    string appPath = '"' + Application.StartupPath + "\\Tesseract-OCR\\tesseract.exe" + '"';
                    string Language = "eng+hin";
                    string input = '"' + Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage) + '"';
                    string oArg = filePath.Trim() ;//'"' + Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage)+".pdf" + '"';
                    string commandArgs = String.Concat('"'+tiffimage.Trim()+ '"', " ", oArg, " -l " + Language + " --psm 6 pdf ");
                StringBuilder arguments = new StringBuilder();
                arguments.Append($"\"{tiffimage.Replace(" ","")}\" ");
                arguments.Append($"\"{oArg.Replace(" ", "")}\" ");
                arguments.Append($"-l {Language} --psm 6 pdf");
                //MessageBox.Show(GetProgramPath("Tesseract-OCR", "tesseract.exe"));
                // ProcessCommand(appPath, commandArgs);
                ProcessCommand(appPath, arguments.ToString());

                Application.DoEvents();

                    //File.Delete(Application.StartupPath + "\\temp\\" + Path.GetFileName(tiffimage));
                    Application.DoEvents();
                    //if (File.Exists(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + "_1.pdf"))
                    //{
                    //    File.Delete(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + "_1.pdf");
                    //    Application.DoEvents();

                    //}

                    //clsCompressPDF clscmpr = new clsCompressPDF();
                    //Application.DoEvents();

                    //clscmpr.compressPDF(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf");
                    //if (File.Exists(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + "_1.pdf"))
                    //{
                    //    if (File.Exists(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf"))
                    //    {
                    //        File.Delete(Application.StartupPath + "\\PDFTEMP\\" + Path.GetFileNameWithoutExtension(tiffimage) + ".pdf");
                    //    }
                    //    Application.DoEvents();

                    //}
              //  }
                GC.Collect();
                GC.WaitForPendingFinalizers();


                return 1;
            }
            catch (Exception ex)
            {
                //using (StreamWriter wrioter = new StreamWriter(filePath, true))
                //{
                //    wrioter.WriteLine("Tiff-" + tiffimage + "Message :" + ex.Message + "<br/>" + Environment.NewLine);
                return 0;
                //}
            }


        }
        public string CreateMultiTiff(string output_path, string temFileName)
        {
            Application.DoEvents();
            PdfReader reader = null;
            Document sourceDocument = null;
            PdfCopy pdfCopyProvider = null;
            PdfImportedPage importedPage;
            Application.DoEvents();
            string outputPdfPath = temFileName;
            //outputPdfPath = outputPdfPath.Replace(".pdf.pdf", ".pdf");

            if (File.Exists(outputPdfPath))
                File.Delete(outputPdfPath);
            Application.DoEvents();


            var files = Directory.GetFiles(output_path, "*.pdf").OrderBy(d => new FileInfo(d).CreationTime);

            string[] pdf_filelist = Directory.GetFiles(output_path, "*.pdf");
            //MessageBox.Show(pdf_filelist[0]);
            //long[] tmpArr = new long[pdf_filelist.Length];
            //for (int i = 0; i < tmpArr.Length; i++)
            //{
            //    tmpArr[i] = Convert.ToInt64(Path.GetFileNameWithoutExtension(pdf_filelist[i]));
            //}

            //Array.Sort(tmpArr);

            //for (long i = 0; i < tmpArr.Length; i++)
            //{
            //    pdf_filelist[i] = output_path + "\\" + tmpArr[i] + ".pdf";
            //}

            sourceDocument = new Document();
            pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.CreateNew));


            //reader.SetFullCompression();
            //writer.PdfVersion = PdfWriter.VERSION_1_6;
            pb2.Value = 0;
            sourceDocument.Open();
            try
            {
                if (Path.GetFileName(temFileName).ToLower().Contains("@@@$$$$$noting"))
                {
                    for (int f = pdf_filelist.Length-1; f >=0; f--)
                    {
                        if (pb2.Value < pb2.Maximum)
                            pb2.Value = Convert.ToInt32(pb2.Value) + 1;

                        pb2.Visible = true;
                        label4.Text = "Validating PDF....... " + (f + 1).ToString() + "/" + pdf_filelist.Length.ToString();
                        Application.DoEvents();
                        int pages = 1;
                        reader = new PdfReader(pdf_filelist[f]);
                        //reader.RemoveFields();
                        //reader.RemoveUnusedObjects();
                        for (int i = 1; i <= pages; i++)
                        {
                            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                            pdfCopyProvider.AddPage(importedPage);
                            //log.Info("MultiPDFCreation:"+i+"Page was Mergied Sucessfully...!");
                        }
                        reader.Close();
                    }
                }
                else
                {
                    for (int f = 0; f < pdf_filelist.Length; f++)
                    {
                        int pages = 1;
                        reader = new PdfReader(pdf_filelist[f]);
                        //reader.RemoveFields();
                        //reader.RemoveUnusedObjects();
                        for (int i = 1; i <= pages; i++)
                        {
                            importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                            pdfCopyProvider.AddPage(importedPage);
                            //log.Info("MultiPDFCreation:"+i+"Page was Mergied Sucessfully...!");
                        }
                        reader.Close();
                    }
                }

                sourceDocument.Close();

                outputPdfPath = outputPdfPath;

                //log.Info("MultiPDFCreation:" + outputPdfPath + " file was Save Sucessfully...!");
                int pagecount = 0;
                for (int f = 0; f < pdf_filelist.Length; f++)
                {
                    GC.Collect();
                    Application.DoEvents();
                    Application.DoEvents();
                    Application.DoEvents();
                    GC.WaitForPendingFinalizers();
                    if (File.Exists(pdf_filelist[f]))
                        File.Delete(pdf_filelist[f]);

                    pagecount = f + 1;
                }
                //if (Directory.Exists(output_path))
                //{
                //    GC.Collect();
                //    Application.DoEvents();
                //    Application.DoEvents();
                //    Application.DoEvents();
                //    GC.WaitForPendingFinalizers();
                //    Directory.SetCurrentDirectory(output_path);
                //    Directory.Delete(output_path);
                //}
                //log.Info("MultiPDFCreation: " + pdf_filelist.Length + " No of single files was deleted Sucessfully...!");
                return outputPdfPath;

            }
            catch (Exception ex)
            {
                //log.Error("CreateMultiTiff : Error :",ex);
                //throw ex;
                MessageBox.Show(ex.Message);

                return "0";
            }
            finally
            {
                try
                {
                    //reader.Close();
                    sourceDocument.Close();
                }
                catch (Exception)
                {

                    //throw;
                }
            } 
        }
        protected static void ProcessCommand(string FileToExecute, string Arguments)
        {
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = FileToExecute;
            info.UseShellExecute = false;
            info.RedirectStandardError = true;
            info.RedirectStandardOutput = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;
            //-t .75
            info.Arguments = Arguments;
            p.StartInfo = info;
            try
            {
                p.Start();
                p.WaitForExit(900000);
                try
                {
                    p.Kill();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    
                }
                //if (File.Exists())
            }
            catch (Exception x)
            {
                Debug.WriteLine(x.Message);
                throw x;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (textBox1.Text.Trim() != "")
            {
                if (Directory.Exists(textBox1.Text.Trim()))
                {
                    fd.SelectedPath = textBox1.Text.Trim();
                }
            }

            // Show the FolderBrowserDialog once
            if (fd.ShowDialog(this) == DialogResult.OK)
            {
                // Get the selected folder path
                string selectedFolderPath = fd.SelectedPath;
                textBox1.Text = selectedFolderPath; // Update the textbox with the selected path

                // Clear the existing nodes in the TreeView
                treeViewDirectories.Nodes.Clear();

                // Load the directory structure into the TreeView
                LoadDirectoryTree(selectedFolderPath, treeViewDirectories.Nodes);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowDialog(this);
            textBox2.Text = fd.SelectedPath;
        }

        private void rdoSearchableComp_CheckedChanged(object sender, EventArgs e)
        {
            //if (rdoSearchableComp.Checked==true )
            //{
            //    chkSearchable.Checked = true;
            //}
            //else
            //{
            //    chkSearchable.Checked = false;
            //}
        }


        public void CreatepdfWComp(string path)//add by Ashutosh
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                FileSystemInfo[] allFiles = dirInfo.GetFileSystemInfos();
                var orderedFiles = allFiles.OrderBy(f => f.Name);
                iTextSharp.text.Rectangle pageSize = null;

                string[] files = Directory.GetFiles(path, "*.*");
                
                int cnt = 0;
                if (!Directory.Exists(textBox2.Text))
                {
                    Directory.CreateDirectory(textBox2.Text);
                }
                foreach (string fl in files)
                {
                    if (Path.GetExtension(fl) == ".jpg" || Path.GetExtension(fl) == ".tif" || Path.GetExtension(fl) == ".JPG" || Path.GetExtension(fl) == ".TIF")
                    {
                        cnt = cnt + 1;
                    }
                }

                if (cnt == 0)
                {
                    return;
                }
                pb2.Value = 1;
                pb2.Minimum = 1;
                pb2.Maximum = files.Length * 2;
                int cnt_ = 0;
                Document document = new Document();
                Application.DoEvents();
                using (var stream = new FileStream(textBox2.Text + "\\" + Path.GetFileName(path) + ".pdf", FileMode.Create, FileAccess.Write, FileShare.None))//change by Ashutosh
                {
                    PdfWriter.GetInstance(document, stream);

                    document.Open();

                    foreach (string file in files)
                    {
                        label4.Text = "Converting Images to PDF...............";
                        pb2.Increment(1);
                        if (Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".tif" || Path.GetExtension(file) == ".JPG" || Path.GetExtension(file) == ".TIF")
                        {

                            cnt_++;
                            using (var imageStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                Application.DoEvents();
                                Bitmap bitmap = new Bitmap(file);

                                double scale = 72d / bitmap.HorizontalResolution;
                                int width1 = (int)(bitmap.Width * scale);
                                int height1 = (int)(bitmap.Height * scale);

                                Bitmap b0 = new Bitmap(bitmap);

                                scale = 120d / bitmap.HorizontalResolution;
                                int width = (int)(b0.Width * 1);
                                int height = (int)(b0.Height * 1);
                                Bitmap bmpScaled = new Bitmap(b0, width, height);
                                b0.Dispose();
                                //bmpScaled.Dispose();
                                bitmap = new Bitmap(bmpScaled);
                                MemoryStream memstream = new MemoryStream();
                                bitmap.Save(memstream, System.Drawing.Imaging.ImageFormat.Jpeg);

                                //bitmap.Save("D:\\123.tif", System.Drawing.Imaging.ImageFormat.Tiff);


                                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(memstream.ToArray());
                                bitmap.Dispose();
                                bmpScaled.Dispose();
                                //iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(stream.ToArray());

                                jpg.CompressionLevel = iTextSharp.text.Image.JPEG;
                                //jpg.SetDpi(72, 72);

                                jpg.ScaleAbsoluteHeight(height);
                                jpg.ScaleAbsoluteWidth(width);
                                jpg.SetDpi(72, 72);
                                Application.DoEvents();
                                var image = iTextSharp.text.Image.GetInstance(jpg);
                                jpg.Alignment = Element.ALIGN_CENTER;
                                //jpg.SetDpi(300, 300);
                                //jpg.ScalePercent(72f);
                                jpg.ScaleToFit(width1, height1);
                                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(width1, height1);
                                //pageSize = rect.GetRectangle(jpg.Width, jpg.Height);
                                pageSize = new iTextSharp.text.Rectangle(width1, height1);
                                document.SetPageSize(pageSize);
                                document.SetMargins(0, 0, 0, 0);
                                document.NewPage();
                                document.Add(jpg);
                            }

                            Application.DoEvents();
                            Application.DoEvents();
                            
                        }
                    }
                    document.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        public void compressPDF(string pdfname, int totalImages)
        {

            string workingFolder = Application.StartupPath + "\\PDFTEMP";
            //Large image to add to sample PDF
            string largeImage = Path.Combine(workingFolder, "LargeImage.jpg");
            //Name of large PDF to create
            string largePDF = Path.Combine(workingFolder, "Large.pdf");
            //Name of compressed PDF to create
            string smallPDF = Path.Combine(workingFolder, "Small.pdf");

            string newPdfName;
            //Now we're going to open the above PDF and compress things

            //Bind a reader to our large PDF
            
            PdfReader reader = new PdfReader(pdfname);
            //Create our output PDF
            using (FileStream fs = new FileStream(Path.GetFileNameWithoutExtension( pdfname) + "_1.pdf", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                //Bind a stamper to the file and our reader
                /*using (*/
                PdfStamper stamper = new PdfStamper(reader, fs);//)
                                                                //{
                newPdfName = fs.Name;                                              //NOTE: This code only deals with page 1, you'd want to loop more for your code
                                                                //Get page 1
                for (int i = 1; i <= totalImages; i++)
                {
                    PdfDictionary page = reader.GetPageN(i);
                    //Get the xobject structure
                    PdfDictionary resources = (PdfDictionary)PdfReader.GetPdfObject(page.Get(PdfName.RESOURCES));
                    PdfDictionary xobject = (PdfDictionary)PdfReader.GetPdfObject(resources.Get(PdfName.XOBJECT));
                    if (xobject != null)
                    {
                        Application.DoEvents();
                        PdfObject obj;
                        //Loop through each key
                        foreach (PdfName name in xobject.Keys)
                        {
                            obj = xobject.Get(name);
                            if (obj.IsIndirect())
                            {
                                //Get the current key as a PDF object
                                PdfDictionary imgObject = (PdfDictionary)PdfReader.GetPdfObject(obj);
                                //See if its an image
                                if (imgObject.Get(PdfName.SUBTYPE).Equals(PdfName.IMAGE))
                                {
                                    //NOTE: There's a bunch of different types of filters, I'm only handing the simplest one here which is basically raw JPG, you'll have to research others
                                    if (imgObject.Get(PdfName.FILTER).Equals(PdfName.DCTDECODE))
                                    {
                                        //Get the raw bytes of the current image
                                        byte[] oldBytes = PdfReader.GetStreamBytesRaw((PRStream)imgObject);
                                        //Will hold bytes of the compressed image later
                                        byte[] newBytes;
                                        //Wrap a stream around our original image
                                        using (MemoryStream sourceMS = new MemoryStream(oldBytes))
                                        {
                                            //Convert the bytes into a .Net image
                                            using (System.Drawing.Image oldImage = Bitmap.FromStream(sourceMS))
                                            {

                                                using (System.Drawing.Image newImage = ShrinkImage(oldImage, 0.5f))
                                                {
                                                    //Convert the image to bytes using JPG at 85%

                                                    newBytes = ConvertImageToBytes(newImage, 40);

                                                }
                                            }
                                        }
                                        //Create a new iTextSharp image from our bytes
                                        iTextSharp.text.Image compressedImage = iTextSharp.text.Image.GetInstance(newBytes);
                                        //Kill off the old image
                                        PdfReader.KillIndirect(obj);
                                        //Add our image in its place
                                        stamper.Writer.AddDirectImageSimple(compressedImage, (PRIndirectReference)obj);
                                    }
                                }
                            }
                        }
                    }

                }


                stamper.Close();
                //}
            }

            reader.Close();
            
            if (File.Exists(pdfname))
            {
                File.Delete(pdfname);
            }
            if (File.Exists(newPdfName))
            {
                File.Move(newPdfName, Path.GetDirectoryName(pdfname) + "\\" + Path.GetFileNameWithoutExtension (pdfname).ToUpper() + ".pdf");
            }

        }
        public static void MargeMultiplePDF(string inputPath, string OutputFile)
        {
            // Create document object  
            string[] PDFfileNames = Directory.GetFiles(inputPath, "*.pdf");

            iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
            // Create a object of FileStream which will be disposed at the end  
            using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
            {
                // Create a PDFwriter that is listens to the Pdf document  
                iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
                if (PDFwriter == null)
                {
                    return;
                }
                // Open the PDFdocument  
                PDFdoc.Open();
                foreach (string fileName in PDFfileNames)
                {
                    // Create a PDFreader for a certain PDFdocument  
                    iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);
                    PDFreader.ConsolidateNamedDestinations();
                    // Add content  
                    for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                        PDFwriter.AddPage(page);
                    }
                    iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                    if (form != null)
                    {
                        PDFwriter.CopyAcroForm(PDFreader);
                    }
                    // Close PDFreader  
                    PDFreader.Close();
                }
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }
        private static System.Drawing.Image ShrinkImage(System.Drawing.Image sourceImage, float scaleFactor)
        {
            int newWidth = Convert.ToInt32(sourceImage.Width * scaleFactor);
            int newHeight = Convert.ToInt32(sourceImage.Height * scaleFactor);

            var thumbnailBitmap = new Bitmap(newWidth, newHeight);
            using (Graphics g = Graphics.FromImage(thumbnailBitmap))
            {
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.SmoothingMode = SmoothingMode.HighSpeed;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                System.Drawing.Rectangle imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
                g.DrawImage(sourceImage, imageRectangle);
            }
            return thumbnailBitmap;
        }

        private static byte[] ConvertImageToBytes(System.Drawing.Image image, long compressionLevel)
        {
            if (compressionLevel < 0)
            {
                compressionLevel = 0;
            }
            else if (compressionLevel > 100)
            {
                compressionLevel = 100;
            }
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compressionLevel);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, jgpEncoder, myEncoderParameters);
                return ms.ToArray();
            }

        }
        
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // extra added code
        private void LoadDirectoryTree(string dirPath, TreeNodeCollection nodeCollection)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                TreeNode rootNode = new TreeNode(directoryInfo.Name)
                {
                    Tag = directoryInfo
                };
                nodeCollection.Add(rootNode);
                LoadSubDirectories(directoryInfo, rootNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubDirectories(DirectoryInfo dirInfo, TreeNode treeNode)
        {
            try
            {
                foreach (var directory in dirInfo.GetDirectories())
                {
                    TreeNode dirNode = new TreeNode(directory.Name)
                    {
                        Tag = directory
                    };
                    treeNode.Nodes.Add(dirNode);
                    LoadSubDirectories(directory, dirNode);
                }

                foreach (var file in dirInfo.GetFiles())
                {
                    TreeNode fileNode = new TreeNode(file.Name)
                    {
                        Tag = file
                    };
                    treeNode.Nodes.Add(fileNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
