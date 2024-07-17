using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
//using PdfSharp.Drawing;
//using PdfSharp.Pdf;
//using Ghostscript.NET.Rasterizer;
//using PdfSharp.Pdf.IO;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using AxAcroPDFLib;
//using iText.Kernel.Font;
//using iText.Kernel.Pdf;
//using iText.Kernel.Pdf.Canvas;
//using iText.Layout;
//using iText.Layout.Element;
//using iText.IO.Image;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
//using PdfiumViewer;
using Ghostscript.NET;
//using iText.IO.Codec;
//using iText.Kernel.Pdf;
//using iTextSharp.text.pdf.parser;



namespace t5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            
        }
        string imagepath = "";
        string pdfpath = "";
        int CurrentPageNumber = 0;
        private void PdfBrowseBtn_Click(object sender, EventArgs e)
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
           // return null;
        }

        //static void CreateSearchablePdf(string inputPdfPath, string outputPdfPath)
        //{
        //    string tessdataPath = "D:\\tessdata";
        //    try
        //    {
        //        // Load the PDF
        //        using (PdfDocument pdfDocument = PdfReader.Open(inputPdfPath, PdfDocumentOpenMode.Import))
        //        {
        //            int pageCount = pdfDocument.PageCount;

        //            using (var renderer = Tesseract.PdfResultRenderer.CreatePdfRenderer(outputPdfPath, tessdataPath, false))
        //            {
        //                using (renderer.BeginDocument("SearchablePDF"))
        //                {
        //                    for (int i = 0; i < pageCount; i++)
        //                    {
        //                        string imagePath = $"page_{i + 1}.jpg";

        //                        // Render the PDF page to an image
        //                        using (var rasterizer = new GhostscriptRasterizer())
        //                        {
        //                            rasterizer.Open(inputPdfPath);
        //                            var img = rasterizer.GetPage(300, i + 1); // 300 DPI
        //                            img.Save(imagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        }

        //                        // Perform OCR on the image
        //                        using (var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
        //                        {
        //                            using (var img = Pix.LoadFromFile(imagePath))
        //                            {
        //                                using (var page = engine.Process(img))
        //                                {
        //                                    renderer.AddPage(page);
        //                                }
        //                            }
        //                        }

        //                        // Cleanup temporary image
        //                        System.IO.File.Delete(imagePath);
        //                        Console.WriteLine($"Page {i + 1} processed.");
        //                    }
        //                }
        //            }

        //            Console.WriteLine($"Searchable PDF created: {outputPdfPath}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}

        //public static void CreateSearchablePdfFromImage(string imagePath, string outputPdfPath)
        //{
        //    string tessdataPath = "D:/tessdata";

        //    try
        //    {
        //        // Initialize Tesseract engine
        //        using (var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
        //        {
        //            // Perform OCR on the image and get text and bounding boxes
        //            using (var img = Pix.LoadFromFile(imagePath))
        //            using (var page = engine.Process(img))
        //            {
        //                var pdfDocument = new Document(new iTextSharp.text.Rectangle(img.Width, img.Height));
        //                using (var pdfStream = new FileStream(outputPdfPath, FileMode.Create))
        //                {
        //                    var writer = PdfWriter.GetInstance(pdfDocument, pdfStream);
        //                    pdfDocument.Open();

        //                    // Add the image to the PDF
        //                    var pdfImage = iTextSharp.text.Image.GetInstance(imagePath);
        //                    pdfImage.SetAbsolutePosition(0, 0);
        //                    pdfDocument.Add(pdfImage);

        //                    // Add searchable text obtained from OCR
        //                    var cb = writer.DirectContent;
        //                    cb.BeginText();
        //                    var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        //                    // Iterate through the text lines to get the bounding boxes
        //                    using (var iter = page.GetIterator())
        //                    {
        //                        iter.Begin();
        //                        do
        //                        {
        //                            if (iter.IsAtBeginningOf(PageIteratorLevel.TextLine))
        //                            {
        //                                var lineText = iter.GetText(PageIteratorLevel.TextLine);
        //                                if (iter.TryGetBoundingBox(PageIteratorLevel.TextLine, out var bbox))
        //                                {
        //                                    float xPosition = bbox.X1;
        //                                    float yPosition = img.Height - bbox.Y2; // Y position is inverted in PDF

        //                                    // Set text position and size
        //                                    float textHeight = bbox.Y2 - bbox.Y1;
        //                                    float fontSize = textHeight * 0.75f; // Adjust font size based on text height
        //                                    cb.SetFontAndSize(baseFont, fontSize);
        //                                    cb.SetTextMatrix(xPosition, yPosition);

        //                                    // Set text rendering mode to invisible
        //                                    cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_INVISIBLE);
        //                                    cb.ShowText(lineText.Trim());
        //                                }
        //                            }
        //                        } while (iter.Next(PageIteratorLevel.TextLine));
        //                    }

        //                    cb.EndText();
        //                    pdfDocument.Close();
        //                }

        //                Console.WriteLine($"Searchable PDF created: {outputPdfPath}");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}

        // string tessdataPath = "D:\\tessdata";
        //public static void CreateSearchablePdfFromImagePdf(string imagePdfPath, string outputPdfPath)
        //{
        //    string tessdataPath = "D:\\tessdata";
        //    try
        //    {
        //        // Initialize Tesseract engine
        //        using (var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
        //        {
        //            // Open the image PDF
        //            using (var reader = new PdfReader(imagePdfPath))
        //            {
        //                using (var fs = new FileStream(outputPdfPath, FileMode.Create))
        //                {
        //                    using (var document = new Document())
        //                    {
        //                        var writer = PdfWriter.GetInstance(document, fs);
        //                        document.Open();

        //                        for (int i = 1; i <= reader.NumberOfPages; i++)
        //                        {
        //                            var page = writer.GetImportedPage(reader, i);
        //                            var pageWidth = page.Width;
        //                            var pageHeight = page.Height;
        //                            document.SetPageSize(new iTextSharp.text.Rectangle(pageWidth, pageHeight));
        //                            document.NewPage();

        //                            var contentByte = writer.DirectContent;
        //                            contentByte.AddTemplate(page, 0, 0);

        //                            // Render the PDF page to an image
        //                            using (var pdfDocument = PdfiumViewer.PdfDocument.Load(imagePdfPath))
        //                            {
        //                                using (var pdfPage = pdfDocument.Render(i - 1, (int)pageWidth, (int)pageHeight, 96, 96, true))
        //                                {
        //                                    using (var imgStream = new MemoryStream())
        //                                    {
        //                                        pdfPage.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);

        //                                        // Perform OCR on the image and get text and bounding boxes
        //                                        using (var imgPix = Pix.LoadFromMemory(imgStream.ToArray()))
        //                                        {
        //                                            using (var pageOcr = engine.Process(imgPix))
        //                                            {
        //                                                var baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        //                                                // Iterate through the text lines to get the bounding boxes
        //                                                using (var iter = pageOcr.GetIterator())
        //                                                {
        //                                                    iter.Begin();
        //                                                    do
        //                                                    {
        //                                                        if (iter.IsAtBeginningOf(PageIteratorLevel.TextLine))
        //                                                        {
        //                                                            var lineText = iter.GetText(PageIteratorLevel.TextLine);
        //                                                            if (iter.TryGetBoundingBox(PageIteratorLevel.TextLine, out var bbox))
        //                                                            {
        //                                                                float xPosition = bbox.X1;
        //                                                                float yPosition = imgPix.Height - bbox.Y2; // Y position is inverted in PDF

        //                                                                // Set text position and size
        //                                                                float textHeight = bbox.Y2 - bbox.Y1;
        //                                                                float fontSize = textHeight * 0.75f; // Adjust font size based on text height
        //                                                                contentByte.SetFontAndSize(baseFont, fontSize);
        //                                                                contentByte.SetTextMatrix(xPosition, yPosition);

        //                                                                // Set text rendering mode to invisible
        //                                                                contentByte.BeginText();
        //                                                                contentByte.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_INVISIBLE);
        //                                                                contentByte.ShowText(lineText.Trim());
        //                                                                contentByte.EndText();
        //                                                            }
        //                                                        }
        //                                                    } while (iter.Next(PageIteratorLevel.TextLine));
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                        document.Close();
        //                    }
        //                }
        //            }
        //            Console.WriteLine($"Searchable PDF created: {outputPdfPath}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}


        //public static void CreateSearchablePdfFromImagePdf(string inputPath, string outputPdfPath)
        //{
        //    // Open the existing PDF
        //    PdfReader pdfReader = new PdfReader(inputPath);
        //    int numberOfPages = pdfReader.NumberOfPages;

        //    // Tesseract OCR setup
        //    string trainingData = "D:\\tessdata";

        //    // Create a new PDF document to hold the searchable content
        //    using (Document document = new Document())
        //    {
        //        using (PdfCopy pdfCopy = new PdfCopy(document, new FileStream(outputPdfPath, FileMode.Create)))
        //        {
        //            document.Open();

        //            for (int i = 1; i <= numberOfPages; i++)
        //            {
        //                // Extract page content as image
        //                PdfDictionary pageDict = pdfReader.GetPageN(i);
        //                PdfObject pdfObject = pageDict.GetDirectObject(PdfName.CONTENTS);
        //                byte[] contentBytes = ContentByteUtils.GetContentBytesFromPage(pdfReader, i);
        //                using (MemoryStream ms = new MemoryStream(contentBytes))
        //                {
        //                    using (var pdfStamper = new PdfStamper(pdfReader, ms))
        //                    {
        //                        PdfImportedPage page = pdfCopy.GetImportedPage(pdfReader, i);
        //                        PdfCopy.PageStamp stamp = pdfCopy.CreatePageStamp(page);

        //                        // Convert the page to an image
        //                        Bitmap bitmap;
        //                        using (var pageContent = new MemoryStream())
        //                        {
        //                            PdfReaderContentParser parser = new PdfReaderContentParser(pdfReader);
        //                            ImageRenderListener listener = new ImageRenderListener();
        //                            parser.ProcessContent(i, listener);
        //                            bitmap = listener.GetImage();
        //                        }

        //                        // Use Tesseract to process the image and get text
        //                        using (TesseractEngine engine = new TesseractEngine(trainingData, "eng", EngineMode.Default))
        //                        {
        //                            using (var img = PixConverter.ToPix(bitmap))
        //                            {
        //                                using (var pageOCR = engine.Process(img))
        //                                {
        //                                    string ocrText = pageOCR.GetText();
        //                                    ColumnText.ShowTextAligned(stamp.GetUnderContent(), Element.ALIGN_LEFT, new Phrase(ocrText), 10, 10, 0);
        //                                }
        //                            }
        //                        }

        //                        stamp.AlterContents();
        //                        pdfCopy.AddPage(page);
        //                    }
        //                }

        //                Console.WriteLine($"Page {i} processed.");
        //            }
        //        }
        //    }
        //}
        private static string PerformOcrOnImage(string imagePath, TesseractEngine engine)
        {
            using (var img = Pix.LoadFromFile(imagePath))
            using (var page = engine.Process(img))
            {
                return page.GetText();
            }
        }




        private void ImgBrowseBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png|All files (*.*)|*.*";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                   pictureBox1.ImageLocation  = openFileDialog.FileName;
                    imagepath = openFileDialog.FileName;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    
                }
            }
            
        }
        //static void CreatePdfFromImage(string imagePath, string outputPdfPath)
        //{
        //    try
        //    {
        //        // Create a new PDF document
        //        PdfDocument document = new PdfDocument();
        //        document.Info.Title = "Created with PDFsharp";

        //        // Create an empty page
        //        PdfPage page = document.AddPage();

        //        // Get the graphics object for the page
        //        XGraphics gfx = XGraphics.FromPdfPage(page);

        //        // Load the image
        //        XImage xImage = XImage.FromFile(imagePath);

        //        // Draw the image at full page size
        //        gfx.DrawImage(xImage, 0, 0, page.Width, page.Height);

        //        // Save the document
        //        document.Save(outputPdfPath);
        //        Console.WriteLine($"PDF created: {outputPdfPath}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //    }
        //}

        [ComImport, Guid("3B813CE7-3C3F-4C78-B657-5648CB5F45E7")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IAcroAXDocShim
        {
            int GetCurrentPage();
            void SetCurrentPage(int n);
            void GoForwardStack();
            void GoBackwardStack();
            void SetViewMode(string viewMode);
        }

        public static string GetPageNumber(IntPtr adobeViewerHandle)
        {
            //get a list of all windows held by parent 
            List<IntPtr> childrenWindows = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(childrenWindows);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(adobeViewerHandle, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }

            //now have a list of the children, look for text boxes with class name "Edit"
            for (int i = 0; i < childrenWindows.Count; i++)
            {
                int nRet;
                // Pre-allocate 256 characters, the maximum class name length.
                StringBuilder className = new StringBuilder(256);
                //Get the window class name
                nRet = GetClassName(childrenWindows.ElementAt(i), className, className.Capacity);

                if (className.ToString().CompareTo("Edit") == 0)
                {
                    IntPtr resultPointer = Marshal.AllocHGlobal(200);
                    StringBuilder text = new StringBuilder(20);
                    SendMessage(childrenWindows.ElementAt(i), 0x000D, text.Capacity, text); //0x000D is WM_GETTEXT message
                    if (text.ToString().Contains("%")) //we don't want the text box for the PDF scale (e.g. 66.7% zoomed etc.)
                    {
                        continue;
                    }
                    else
                    {
                        return text.ToString(); // the only other text box is the page number box
                    }
                }
            }

            //Note I return as a string because PDF supports page labels, "I", "ii", "iv" etc. or even section labels "A", "B". So you're not guaranteed a numerical page number.
            return "0";
        }

        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)                                                                                                           
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");

            list.Add(handle);
            return true;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumChildWindows(IntPtr window,
                                                     EnumWindowProc callback,
                                                     IntPtr i);

        internal delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd,
                                                 StringBuilder lpString,
                                                 int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);

        //Replace Image btn click
        private void Searchable_btn_Click(object sender, EventArgs e)
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

            //  string currentDirectory = System.IO.Directory.GetCurrentDirectory();   // + "\\tempsearchable";
            //  if (!Directory.Exists(currentDirectory))
            //  {
            //      Directory.CreateDirectory(currentDirectory);
            //  }

            string impdf = System.IO.Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileNameWithoutExtension(imagepath) + ".pdf";
           string currentDirectory= System.IO.Directory.GetCurrentDirectory() + "\\" + System.IO.Path.GetFileNameWithoutExtension(imagepath) + "1.pdf";
            imgtopdf(imagepath, impdf);
         //   Class1 ob = new Class1();
         //   ob.CreateSearchablePdfFromImagePdf(impdf, currentDirectory);

            //CreateSearchablePdfFromImagePdf(impdf, currentDirectory);


            // MessageBox.Show("searchable pdf created");
            // Remove impdf
          //  File.Delete(impdf);


            // Directory.Delete(currentDirectory, recursive: true);
            //MessageBox.Show("Directory deleted");
            string pythonExePath = @"C:\Users\Ritik Tyagi\source\repos\t5\t5\bin\Debug\pagereplace.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pythonExePath;
            int PageNo = Convert.ToInt32(GetPageNumber(axAcroPDF1.Handle)) - 1;
            CurrentPageNumber = PageNo + 1;
            string currentPageNo = Convert.ToString(PageNo);
            //startInfo.Arguments = pdfpath + " " + pdfpath + " " + currentPageNo+ " " + currentDirectory ;
            //startInfo.Arguments = $" '{ pdfpath}'   '{pdfpath}'  {currentPageNo}  '{currentDirectory}'  ";
            string new_pdf = Path.GetFileNameWithoutExtension(pdfpath) + "1.pdf";
            startInfo.Arguments = $"\"{pdfpath}\"  \"{new_pdf}\"  {currentPageNo}  \"{currentDirectory}\"";
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
        public static void CreatePdfFromImage(string imagePath, string pdfPath, float maxInchWidth = 8.40f, float maxInchHeight = 13.40f, int dpi = 300)
        {
            // Load the image
            using (var image = new Bitmap(imagePath))
            {
                // Calculate the maximum pixel dimensions based on the desired size and DPI
                float maxPixelWidth = maxInchWidth * dpi;
                float maxPixelHeight = maxInchHeight * dpi;

                // Calculate the new dimensions while preserving the aspect ratio
                float ratioX = maxPixelWidth / image.Width;
                float ratioY = maxPixelHeight / image.Height;
                float ratio = Math.Min(ratioX, ratioY);

                int newWidth = (int)(image.Width * ratio);
                int newHeight = (int)(image.Height * ratio);

                // Create a new bitmap with the new dimensions
                using (var scaledImage = new Bitmap(newWidth, newHeight))
                {
                    using (var graphics = Graphics.FromImage(scaledImage))
                    {
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;

                        graphics.DrawImage(image, 0, 0, newWidth, newHeight);
                    }

                    // Create a PDF document with the desired page size
                    var pageSize = new iTextSharp.text.Rectangle(0, 0, (int)maxPixelWidth, (int)maxPixelHeight);
                    var document = new Document(pageSize);

                    using (var stream = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        PdfWriter.GetInstance(document, stream);
                        document.Open();

                        // Convert the scaled image to iTextSharp Image
                        using (var ms = new MemoryStream())
                        {
                            scaledImage.SetResolution(dpi, dpi);
                            scaledImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            var itextImage = iTextSharp.text.Image.GetInstance(ms.ToArray());

                            // Set the position and size of the image in the PDF
                            itextImage.SetAbsolutePosition(
                                (maxPixelWidth - itextImage.ScaledWidth) / 2,
                                (maxPixelHeight - itextImage.ScaledHeight) / 2
                            );

                            document.Add(itextImage);
                        }

                        document.Close();
                    }
                }
            }
        }
        private void Pg_beforeBtn_Click(object sender, EventArgs e)
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
            string pdfpath1 = "D:\\" + Path.GetFileNameWithoutExtension(imagepath) + "_b.pdf";
            
            imgtopdf(imagepath, pdfpath1);
            // MessageBox.Show($"image created at {pdfpath1}");
            CurrentPageNumber = Convert.ToInt32(GetPageNumber(axAcroPDF1.Handle));


            AddPdfPageBeforePage(pdfpath, pdfpath1, CurrentPageNumber);
        }
        private void AddPdfPageAfterPage(string srcPdfPath, string pagePdfPath, int pageNumber)
        {
            string tempFilePath = Path.GetTempFileName();

            using (var reader = new PdfReader(srcPdfPath))
            {
                using (var pageReader = new PdfReader(pagePdfPath))
                {
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (var document = new Document())
                        {
                            using (var writer = PdfWriter.GetInstance(document, fileStream))
                            {
                                document.Open();
                                var contentByte = writer.DirectContent;
                                var pageCount = reader.NumberOfPages;

                                for (int i = 1; i <= pageCount; i++)
                                {
                                    // Copy the original page content
                                    var pageSizeOriginal = reader.GetPageSizeWithRotation(i);
                                    document.SetPageSize(pageSizeOriginal);
                                    document.NewPage();
                                    var importedPage = writer.GetImportedPage(reader, i);
                                    var rotation = reader.GetPageRotation(i);
                                    if (rotation == 90 || rotation == 270)
                                    {
                                        contentByte.AddTemplate(importedPage, 0, -1f, 1f, 0, 0, pageSizeOriginal.Height);
                                    }
                                    else
                                    {
                                        contentByte.AddTemplate(importedPage, 0, 0);
                                    }

                                    if (i == pageNumber)
                                    {
                                        // Add the page from the other PDF after the specified page number
                                        var pageSize = pageReader.GetPageSizeWithRotation(1);
                                        document.SetPageSize(pageSize);
                                        document.NewPage();
                                        var importedNewPage = writer.GetImportedPage(pageReader, 1);
                                        var newPageRotation = pageReader.GetPageRotation(1);
                                        if (newPageRotation == 90 || newPageRotation == 270)
                                        {
                                            contentByte.AddTemplate(importedNewPage, 0, -1f, 1f, 0, 0, pageSize.Height);
                                        }
                                        else
                                        {
                                            contentByte.AddTemplate(importedNewPage, 0, 0);
                                        }
                                    }
                                }

                                // If the page needs to be added after the last page
                                if (pageNumber >= pageCount)
                                {
                                    var pageSize = pageReader.GetPageSizeWithRotation(1);
                                    document.SetPageSize(pageSize);
                                    document.NewPage();
                                    var importedNewPage = writer.GetImportedPage(pageReader, 1);
                                    var newPageRotation = pageReader.GetPageRotation(1);
                                    if (newPageRotation == 90 || newPageRotation == 270)
                                    {
                                        contentByte.AddTemplate(importedNewPage, 0, -1f, 1f, 0, 0, pageSize.Height);
                                    }
                                    else
                                    {
                                        contentByte.AddTemplate(importedNewPage, 0, 0);
                                    }
                                }

                                document.Close();
                            }
                        }
                    }
                }
            }

            // Replace the original file with the modified temporary file
            File.Delete(srcPdfPath);
            File.Move(tempFilePath, srcPdfPath);
            axAcroPDF1.LoadFile(srcPdfPath);
            axAcroPDF1.setCurrentPage(pageNumber + 1);
            MessageBox.Show("Image added");
        }
        //private void AddImageToPdfBeforePage(string srcPdfPath, string imagePath, int pageNumber)
        //{
        //    string tempFilePath = Path.GetTempFileName();

        //    using (var reader = new PdfReader(srcPdfPath))
        //    {
        //        using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
        //        {
        //            using (var document = new Document())
        //            {
        //                using (var writer = PdfWriter.GetInstance(document, fileStream))
        //                {
        //                    document.Open();
        //                    var contentByte = writer.DirectContent;
        //                    var pageCount = reader.NumberOfPages;

        //                    for (int i = 1; i <= pageCount + 1; i++)
        //                    {
        //                        if (i == pageNumber)
        //                        {
        //                            // Add the image before the specified page number
        //                            var pageSize = reader.GetPageSizeWithRotation(1);
        //                            var image = iTextSharp.text.Image.GetInstance(imagePath);

        //                            // Ensure the image does not exceed the page size
        //                            if (image.Width > pageSize.Width || image.Height > pageSize.Height)
        //                            {
        //                                image.ScaleToFit(pageSize.Width, pageSize.Height);
        //                            }

        //                            // Center the image on the page
        //                            image.SetAbsolutePosition(
        //                                (pageSize.Width - image.ScaledWidth) / 2,
        //                                (pageSize.Height - image.ScaledHeight) / 2
        //                            );

        //                            // Add a new page with the image
        //                            document.SetPageSize(pageSize);
        //                            document.NewPage();
        //                            contentByte.AddImage(image);
        //                        }

        //                        if (i <= pageCount)
        //                        {
        //                            // Copy the original page content
        //                            var pageSizeOriginal = reader.GetPageSizeWithRotation(i);
        //                            document.SetPageSize(pageSizeOriginal);
        //                            document.NewPage();
        //                            var importedPage = writer.GetImportedPage(reader, i);
        //                            var rotation = reader.GetPageRotation(i);
        //                            if (rotation == 90 || rotation == 270)
        //                            {
        //                                contentByte.AddTemplate(importedPage, 0, -1f, 1f, 0, 0, pageSizeOriginal.Height);
        //                            }
        //                            else
        //                            {
        //                                contentByte.AddTemplate(importedPage, 0, 0);
        //                            }
        //                        }
        //                    }

        //                    document.Close();
        //                }
        //            }
        //        }
        //    }

        //    // Replace the original file with the modified temporary file

        //    File.Delete(srcPdfPath);
        //    File.Move(tempFilePath, srcPdfPath);
        //    axAcroPDF1.LoadFile(srcPdfPath);
        //    axAcroPDF1.setCurrentPage(pageNumber);
        //    MessageBox.Show("Image inserted");
        //}


        private int imgtopdf(string tiffimage, string pdfpath)
        {
            if(File.Exists(pdfpath))
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
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfpath , FileMode.CreateNew));

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

        private void InsertafterBtn_Click(object sender, EventArgs e)
        {
            string pdfpath1 = "D:\\" + Path.GetFileNameWithoutExtension(imagepath) + "_b.pdf";

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
            imgtopdf(imagepath, pdfpath1);
           // MessageBox.Show($"image created at {pdfpath1}");
            CurrentPageNumber = Convert.ToInt32(GetPageNumber(axAcroPDF1.Handle));


            AddPdfPageAfterPage(pdfpath, pdfpath1, CurrentPageNumber);
        }

        private void AddPdfPageBeforePage(string srcPdfPath, string pagePdfPath, int pageNumber)
        {
            string tempFilePath = Path.GetTempFileName();

            using (var reader = new PdfReader(srcPdfPath))
            {
                using (var pageReader = new PdfReader(pagePdfPath))
                {
                    using (var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (var document = new Document())
                        {
                            using (var writer = PdfWriter.GetInstance(document, fileStream))
                            {
                                document.Open();
                                var contentByte = writer.DirectContent;
                                var pageCount = reader.NumberOfPages;

                                for (int i = 1; i <= pageCount; i++)
                                {
                                    if (i == pageNumber)
                                    {
                                        // Add the page from the other PDF before the specified page number
                                        var pageSize = pageReader.GetPageSizeWithRotation(1);
                                        document.SetPageSize(pageSize);
                                        document.NewPage();
                                        var importedNewPage = writer.GetImportedPage(pageReader, 1);
                                        var newPageRotation = pageReader.GetPageRotation(1);
                                        if (newPageRotation == 90 || newPageRotation == 270)
                                        {
                                            contentByte.AddTemplate(importedNewPage, 0, -1f, 1f, 0, 0, pageSize.Height);
                                        }
                                        else
                                        {
                                            contentByte.AddTemplate(importedNewPage, 0, 0);
                                        }
                                    }

                                    // Copy the original page content
                                    var pageSizeOriginal = reader.GetPageSizeWithRotation(i);
                                    document.SetPageSize(pageSizeOriginal);
                                    document.NewPage();
                                    var importedPage = writer.GetImportedPage(reader, i);
                                    var rotation = reader.GetPageRotation(i);
                                    if (rotation == 90 || rotation == 270)
                                    {
                                        contentByte.AddTemplate(importedPage, 0, -1f, 1f, 0, 0, pageSizeOriginal.Height);
                                    }
                                    else
                                    {
                                        contentByte.AddTemplate(importedPage, 0, 0);
                                    }
                                }

                                // If the page needs to be added after the last page
                                if (pageNumber > pageCount)
                                {
                                    var pageSize = pageReader.GetPageSizeWithRotation(1);
                                    document.SetPageSize(pageSize);
                                    document.NewPage();
                                    var importedNewPage = writer.GetImportedPage(pageReader, 1);
                                    var newPageRotation = pageReader.GetPageRotation(1);
                                    if (newPageRotation == 90 || newPageRotation == 270)
                                    {
                                        contentByte.AddTemplate(importedNewPage, 0, -1f, 1f, 0, 0, pageSize.Height);
                                    }
                                    else
                                    {
                                        contentByte.AddTemplate(importedNewPage, 0, 0);
                                    }
                                }

                                document.Close();
                            }
                        }
                    }
                }
            }

            // Replace the original file with the modified temporary file
            File.Delete(srcPdfPath);
            File.Move(tempFilePath, srcPdfPath);
            axAcroPDF1.LoadFile(srcPdfPath);
            axAcroPDF1.setCurrentPage(pageNumber);
        }



        private void replacesearchablepdf()
        {
            string currentDirectory = System.IO.Directory.GetCurrentDirectory() + "\\tempsearchable";
            if (!Directory.Exists(currentDirectory))
            {
                Directory.CreateDirectory(currentDirectory);
            }

            currentDirectory = currentDirectory + "\\" + System.IO.Path.GetFileNameWithoutExtension(imagepath) + ".pdf";
            //CreateSearchablePdfFromImage(imagepath, currentDirectory);
            MessageBox.Show("searchable pdf created");

            // Directory.Delete(currentDirectory, recursive: true);
            //MessageBox.Show("Directory deleted");
            string pythonExePath = @"C:\Users\Ritik Tyagi\source\repos\t5\t5\bin\Debug\pagereplace.exe";
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = pythonExePath;
            int PageNo = Convert.ToInt32(GetPageNumber(axAcroPDF1.Handle)) - 1;
            CurrentPageNumber = PageNo + 1;
            string currentPageNo = Convert.ToString(PageNo);
            //startInfo.Arguments = pdfpath + " " + pdfpath + " " + currentPageNo+ " " + currentDirectory ;
            //startInfo.Arguments = $" '{ pdfpath}'   '{pdfpath}'  {currentPageNo}  '{currentDirectory}'  ";
            string new_pdf = Path.GetFileNameWithoutExtension(pdfpath) + "1.pdf";
            startInfo.Arguments = $"\"{pdfpath}\"  \"{new_pdf}\"  {currentPageNo}  \"{currentDirectory}\"";
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
    }

   
   
}
