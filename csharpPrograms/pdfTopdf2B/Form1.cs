using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.xmp;
using iTextSharp.text.xml.xmp;
using Microsoft.VisualBasic.FileIO;
using iTextSharp.text.html.simpleparser;
using Microsoft.VisualBasic.FileIO;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using System.Text.RegularExpressions;
namespace xmpMetadata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inputFilePath = @"D:\image00009_b.pdf";
                string outputFilePath = "D://image00009_b_1.pdf";
                string xmlFilePath = "D:/image00009_b.xml";
                string csvFilePath = "D://0135.csv";

                DataTable dt = LoadCsvToDataTable(csvFilePath);
                customMetaData(inputFilePath, outputFilePath, dt, xmlFilePath);
                dt.TableName = "image00009_b"; // ("image00009_b");
                //Path.GetFileNameWithoutExtension(inputFilePath)
                dt.WriteXml(xmlFilePath);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception ex)
            {

            }

        }

        private void customMetaData(string inputFilePath, string outputFilePath, DataTable dt, string xmlFilePath)
        {
            try
            {
                string iccProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "APTEC_PC10_CardBoard_2023_v1.icc");

                // Read the existing PDF
                using (PdfReader reader = new PdfReader(inputFilePath))
                {
                    using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                    {
                        using (PdfStamper stamper = new PdfStamper(reader, fs))
                        {
                            stamper.Writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);

                            // Add MarkInfo dictionary
                            PdfDictionary markInfo = new PdfDictionary(PdfName.MARKINFO);
                            markInfo.Put(PdfName.MARKED, PdfBoolean.PDFTRUE);
                            stamper.Writer.ExtraCatalog.Put(PdfName.MARKINFO, markInfo);

                            // Output Intent
                            ICC_Profile icc = ICC_Profile.GetInstance(File.ReadAllBytes(iccProfilePath));
                            PdfICCBased pdfIcc = new PdfICCBased(icc);
                            pdfIcc.Remove(PdfName.ALTERNATE);
                            PdfIndirectObject iccRef = stamper.Writer.AddToBody(pdfIcc);

                            PdfDictionary outputIntent = new PdfDictionary(PdfName.OUTPUTINTENT);
                            outputIntent.Put(PdfName.TYPE, PdfName.OUTPUTINTENT);
                            outputIntent.Put(PdfName.S, PdfName.GTS_PDFA1);
                            outputIntent.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("sRGB IEC61966-2.1"));
                            outputIntent.Put(PdfName.INFO, new PdfString("sRGB IEC61966-2.1"));
                            outputIntent.Put(PdfName.DESTOUTPUTPROFILE, iccRef.IndirectReference);

                            PdfArray outputIntents = new PdfArray();
                            outputIntents.Add(outputIntent);
                            stamper.Writer.ExtraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);

                            PdfDictionary defaultRgb = new PdfDictionary();
                            defaultRgb.Put(PdfName.DEVICERGB, iccRef.IndirectReference);
                            PdfDictionary resources = stamper.Writer.ExtraCatalog.GetAsDict(PdfName.RESOURCES) ?? new PdfDictionary();
                            resources.Put(PdfName.DEFAULT, defaultRgb);
                            stamper.Writer.ExtraCatalog.Put(PdfName.RESOURCES, resources);

                            // Create XMP Metadata
                            stamper.CreateXmpMetadata();
                            XmpWriter xmpWriter = stamper.XmpWriter;
                            xmpWriter.XmpMeta.SetProperty(XmpConst.NS_PDFA_ID, "pdfaid:part", "2");
                            xmpWriter.XmpMeta.SetProperty(XmpConst.NS_PDFA_ID, "pdfaid:conformance", "B");

                            // Add custom metadata from DataTable
                            if (dt.Rows.Count > 0)
                            {
                                DataRow row = dt.Rows[0]; // Assuming you only process the first row
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    string key = SanitizeXmlName(dt.Columns[j].ToString());
                                    string value = row[j].ToString();
                                    //  xmpWriter.XmpMeta.SetProperty(XmpConst.NS_DC, key, value);
                                    AddCustomMetadata(stamper, key, value);
                                }
                            }

                            // Embed Font
                            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "arial.ttf");
                            EmbedFont(reader, stamper, fontPath);

                            stamper.FormFlattening = true;
                        }
                    }
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Console.WriteLine("PDF/A compliant file created successfully.");
                MessageBox.Show("Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Error occurred");
            }
        }

        //private void EmbedFont(PdfReader reader, PdfStamper stamper, string fontPath)
        //{
        //    BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //    PdfContentByte cb = stamper.GetOverContent(1);
        //    cb.BeginText();
        //    cb.SetFontAndSize(bf, 12);
        //    cb.ShowTextAligned(Element.ALIGN_LEFT, "Embedded Font Example", 36, 750, 0);
        //    cb.EndText();
        //}

        private string SanitizeXmlName(string name)
        {
            // Replace spaces and invalid characters with underscores or other valid characters
            return Regex.Replace(name, @"[^a-zA-Z0-9_]", "_");
        }




        //almost right code
        //private void customMetaData(string inputFilePath, string outputFilePath, DataTable dt, string xmlFilePath)
        //{
        //    try
        //    {
        //        string iccProfilePath = Path.Combine(Directory.GetCurrentDirectory(), "APTEC_PC10_CardBoard_2023_v1.icc");

        //        // Read the existing PDF
        //        using (PdfReader reader = new PdfReader(inputFilePath))
        //        {
        //            using (FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
        //            {
        //                using (PdfStamper stamper = new PdfStamper(reader, fs))
        //                {
        //                    stamper.Writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);

        //                    // Add MarkInfo dictionary
        //                    PdfDictionary markInfo = new PdfDictionary(PdfName.MARKINFO);
        //                    markInfo.Put(PdfName.MARKED, PdfBoolean.PDFTRUE);
        //                    markInfo.Put(PdfName.TITLE, new PdfString("PDF/A-2B Compliant Document"));
        //                    markInfo.Put(PdfName.AUTHOR, new PdfString("Author"));
        //                    markInfo.Put(PdfName.SUBJECT, new PdfString("Subject"));
        //                    markInfo.Put(PdfName.KEYWORDS, new PdfString("Keywords"));
        //                    markInfo.Put(PdfName.CREATOR, new PdfString("Creator"));
        //                    markInfo.Put(PdfName.PRODUCER, new PdfString("Producer"));
        //                    markInfo.Put(PdfName.TRAPPED, new PdfName("False"));
        //                    stamper.Writer.ExtraCatalog.Put(PdfName.MARKINFO, markInfo);

        //                    // Output Intent
        //                    ICC_Profile icc = ICC_Profile.GetInstance(File.ReadAllBytes(iccProfilePath));
        //                    PdfICCBased pdfIcc = new PdfICCBased(icc);
        //                    pdfIcc.Remove(PdfName.ALTERNATE);
        //                    PdfIndirectObject iccRef = stamper.Writer.AddToBody(pdfIcc);

        //                    PdfDictionary outputIntent = new PdfDictionary(PdfName.OUTPUTINTENT);
        //                    outputIntent.Put(PdfName.TYPE, new PdfName("OutputIntent"));
        //                    outputIntent.Put(PdfName.S, PdfName.GTS_PDFA1);
        //                    outputIntent.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("sRGB IEC61966-2.1"));
        //                    outputIntent.Put(PdfName.INFO, new PdfString("sRGB IEC61966-2.1"));
        //                    outputIntent.Put(PdfName.DESTOUTPUTPROFILE, iccRef.IndirectReference);

        //                    PdfArray outputIntents = new PdfArray();
        //                    outputIntents.Add(outputIntent);
        //                    stamper.Writer.ExtraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);

        //                    PdfDictionary defaultRgb = new PdfDictionary();
        //                    defaultRgb.Put(PdfName.DEVICERGB, iccRef.IndirectReference);
        //                    PdfDictionary resources = stamper.Writer.ExtraCatalog.GetAsDict(PdfName.RESOURCES) ?? new PdfDictionary();
        //                    resources.Put(PdfName.DEFAULT, defaultRgb);
        //                    stamper.Writer.ExtraCatalog.Put(PdfName.RESOURCES, resources);

        //                    // Create XMP Metadata
        //                    stamper.CreateXmpMetadata();
        //                    XmpWriter xmpWriter = stamper.XmpWriter;
        //                    xmpWriter.XmpMeta.SetProperty(XmpConst.NS_PDFA_ID, "pdfaid:part", "2");
        //                    xmpWriter.XmpMeta.SetProperty(XmpConst.NS_PDFA_ID, "pdfaid:conformance", "B");

        //                    // Add custom metadata from DataTable
        //                    if (dt.Rows.Count > 0)
        //                    {
        //                        DataRow row = dt.Rows[0]; // Assuming you only process the first row
        //                        for (int j = 0; j < dt.Columns.Count; j++)
        //                        {
        //                            string key = dt.Columns[j].ToString();
        //                            string value = row[j].ToString();
        //                            //   xmpWriter.XmpMeta.SetProperty(XmpConst.NS_DC, key, value);
        //                            AddCustomMetadata(stamper, key, value);
        //                        }
        //                    }

        //                    // Embed Font
        //                    string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "arial.ttf");
        //                    EmbedFont(reader, stamper, fontPath);
        //                    stamper.FormFlattening = true;
        //                }
        //            }
        //        }

        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();

        //        Console.WriteLine("PDF/A compliant file created successfully.");
        //        MessageBox.Show("Done");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        MessageBox.Show("Error occurred");
        //    }
        //}






        //private void customMetaData(string inputFilePath, string outputFilePath, DataTable dt, string xmlFilePath)
        //{
        //    try
        //    {
        //        string iccProfilePath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "APTEC_PC10_CardBoard_2023_v1.icc");

        //        // Read the existing PDF
        //        PdfReader reader = new PdfReader(inputFilePath);

        //        FileStream fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write);

        //        PdfStamper stamper = new PdfStamper(reader, fs);
        //        {

        //            stamper.Writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_7);

        //            // Add MarkInfo dictionary
        //            PdfDictionary markInfo = new PdfDictionary(PdfName.MARKINFO);
        //            markInfo.Put(PdfName.MARKED, PdfBoolean.PDFTRUE);
        //            //            CompleteInfoDictionary(markInfo);
        //            markInfo.Put(PdfName.TITLE, new PdfString("PDF/A-2B Compliant Document"));
        //            markInfo.Put(PdfName.AUTHOR, new PdfString("Author"));
        //            markInfo.Put(PdfName.SUBJECT, new PdfString("Subject"));
        //            markInfo.Put(PdfName.KEYWORDS, new PdfString("Keywords"));
        //            markInfo.Put(PdfName.CREATOR, new PdfString("Creator"));
        //            markInfo.Put(PdfName.PRODUCER, new PdfString("Producer"));
        //            markInfo.Put(PdfName.TRAPPED, new PdfName("False"));



        //            stamper.Writer.ExtraCatalog.Put(PdfName.MARKINFO, markInfo);
        //            //changes here 5 july evening
        //            //  PdfDictionary extraCatalog = reader.Catalog;
        //            //            CompleteExtraCatalog(stamper.Writer.ExtraCatalog);
        //            PdfDictionary outp = new PdfDictionary(PdfName.OUTPUTINTENT);
        //            outp.Put(PdfName.OUTPUTCONDITION, new PdfString("SWOP CGATS TR 001-1995"));
        //            outp.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("CGATS TR 001"));
        //            outp.Put(PdfName.REGISTRYNAME, new PdfString("http://www.color.org"));
        //            outp.Put(PdfName.INFO, new PdfString(""));
        //            outp.Put(PdfName.S, PdfName.GTS_PDFX);

        //            PdfArray outputIntents = stamper.Writer.ExtraCatalog.GetAsArray(PdfName.OUTPUTINTENTS);
        //            if (outputIntents == null)
        //            {
        //                outputIntents = new PdfArray();
        //            }
        //            //forcing to write whether it is null or not
        //            outputIntents = new PdfArray();
        //            outputIntents.Add(outp);
        //            stamper.Writer.ExtraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);



        //            //it might be commented out

        //            stamper.Writer.ExtraCatalog.Put(PdfName.EXTEND, stamper.Writer.ExtraCatalog);
        //            // Make PDF/A compliant
        //            //ref to be added here
        //            //             MakePdfA(stamper, iccProfilePath);
        //            ICC_Profile icc = ICC_Profile.GetInstance(File.ReadAllBytes(iccProfilePath));
        //            PdfICCBased pdfIcc = new PdfICCBased(icc);
        //            pdfIcc.Remove(PdfName.ALTERNATE);
        //            PdfIndirectObject iccRef = stamper.Writer.AddToBody(pdfIcc);

        //            PdfDictionary outputIntent = new PdfDictionary();
        //            outputIntent.Put(PdfName.TYPE, new PdfName("OutputIntent"));
        //            outputIntent.Put(PdfName.S, new PdfName("GTS_PDFA1"));
        //            outputIntent.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("sRGB IEC61966-2.1"));
        //            outputIntent.Put(PdfName.INFO, new PdfString("sRGB IEC61966-2.1"));
        //            outputIntent.Put(PdfName.DESTOUTPUTPROFILE, iccRef.IndirectReference);

        //          //  PdfArray outputIntents = new PdfArray();
        //            outputIntents.Add(outputIntent);
        //            stamper.Writer.ExtraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);

        //            PdfDictionary defaultRgb = new PdfDictionary();
        //            defaultRgb.Put(PdfName.DEVICERGB, iccRef.IndirectReference);
        //            PdfDictionary resources = stamper.Writer.ExtraCatalog.GetAsDict(PdfName.RESOURCES) ?? new PdfDictionary();
        //            resources.Put(PdfName.DEFAULT, defaultRgb);
        //            stamper.Writer.ExtraCatalog.Put(PdfName.RESOURCES, resources);

        //            stamper.CreateXmpMetadata();

        //            // Add PDF/A identification to XMP metadata
        //            //              AddPdfAIdentificationToXmp(stamper, reader, fs);
        //            // Create a new AcroFields instance(assuming no existing fields)
        //            AcroFields fields = stamper.AcroFields;
        //            PdfAConformanceLevel conformanceLevel = PdfAConformanceLevel.PDF_A_2B;

        //            // Set custom metadata fields for PDF/A identification
        //            fields.SetFieldProperty("pdfaid:part", "1", false, null); // Change value for different conformance levels
        //            fields.SetFieldProperty("pdfaid:conformance", "https://www.loc.gov/preservation/digital/formats/fdd/fdd000318.shtml", false, null);

        //            // Add custom metadata from DataTable
        //            if (dt.Rows.Count > 0)
        //            {
        //                DataRow row = dt.Rows[0]; // Assuming you only process the first row
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {
        //                    //string key = dt.Columns[j].ToString();
        //                    string value = row[j].ToString();
        //                    AddCustomMetadata(stamper, dt.Columns[j].ToString(), value);
        //                }
        //            }


        //            // Create XMP metadata
        //            //stamper.CreateXmpMetadata();


        //            string fontPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "arial.ttf");
        //            EmbedFont(reader, stamper, fontPath);
        //            stamper.FormFlattening = true;
        //        }
        //       // stamper.FormFlattening = true; // Flatten form fields (optional)
        //        //stamper.Close();


        //        // HTMLWorker worker = new HTMLWorker(document);
        //        //register font used in html
        //        //FontFactory.Register(Environment.GetEnvironmentVariable("SystemRoot") + "\\Fonts\\ARIALUNI.TTF", "arial unicode ms");

        //        ////adding custom style attributes to html specific tasks. Can be used instead of css
        //        ////this one is a must fopr display of utf8 language specific characters (čćžđpš)
        //        //iTextSharp.text.html.simpleparser.StyleSheet ST = new iTextSharp.text.html.simpleparser.StyleSheet();
        //        //ST.LoadTagStyle("body", "encoding", "Identity-H");
        //        //  worker.SetStyleSheet(ST);
        //        //stamper.Close();
        //        //stamper.Dispose();
        //        //reader.Dispose();
        //        //fs.Dispose();


        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();

        //        //stamper.Dispose();

        //        //fs.Dispose();
        //        fs.Close();
        //       //  fs.Dispose();
        //        reader.Dispose();  

        //        Console.WriteLine("PDF/A compliant file created successfully.");
        //        MessageBox.Show("Done");
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine($"An error occurred: {ex.Message}");
        //        MessageBox.Show("Error occurred");
        //    }
        //}
        //public static void CreateSyncfusionPdf(string inputFilePath, string outputFilePath)
        //{
        //    //Load an existing PDF document.
        //    PdfLoadedDocument loadedDocument = new PdfLoadedDocument(inputFilePath);

        //    //Ensure the document meets PDF/A-2b standard.
        //    loadedDocument.Conformance = PdfConformanceLevel.Pdf_A2B;

        //    //Add a new page to the loaded document.
        //    PdfLoadedPage loadedPage = loadedDocument.Pages.Add() as PdfLoadedPage;

        //    //Create PDF graphics for the page.
        //    PdfGraphics graphics = loadedPage.Graphics;

        //    //Load the TrueType font from the local file.
        //    FileStream fontStream = new FileStream(Path.GetFullPath("../../../Arial.ttf"), FileMode.Open, FileAccess.Read);

        //    //Create the PDF font file. 
        //    Syncfusion.Pdf.Graphics.PdfFont font = new PdfTrueTypeFont(fontStream, 14);

        //    //Draw the text.
        //    graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new Syncfusion.Drawing.PointF(0, 0));

        //    //Create file stream for the output PDF.
        //    using (FileStream outputFileStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.ReadWrite))
        //    {
        //        //Save the modified PDF document to file stream.
        //        loadedDocument.Save(outputFileStream);
        //    }

        //    //Close the document.
        //    loadedDocument.Close(true);
        //}

        public static void EmbedFont(PdfReader reader,PdfStamper stamper, string fontPath)
        {
            try
            {
                // Load the input PDF document
              //  PdfReader reader = new PdfReader(inputPdfPath);
                //using (FileStream fs = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                //{
                //    PdfStamper stamper = new PdfStamper(reader, fs);

                    // Load the font
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.WINANSI, BaseFont.EMBEDDED);

                    // Iterate through all the pages and embed the font
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        PdfContentByte content = stamper.GetOverContent(i);
                        content.BeginText();
                        content.SetFontAndSize(baseFont, 12);
                        content.ShowTextAligned(Element.ALIGN_LEFT, "Sample Text with Embedded Font", 100, 100, 0);
                        content.EndText();
                    }

                  //  stamper.Close();
              //  }

             //   reader.Close();
            //    Console.WriteLine("Font embedded successfully.");
            //    MessageBox.Show("Font embedded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                MessageBox.Show("Error occurred while embedding font.");
            }
        }



        public static void CompleteExtraCatalog(PdfDictionary extraCatalog)
        {
            PdfDictionary outp = new PdfDictionary(PdfName.OUTPUTINTENT);
            outp.Put(PdfName.OUTPUTCONDITION, new PdfString("SWOP CGATS TR 001-1995"));
            outp.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("CGATS TR 001"));
            outp.Put(PdfName.REGISTRYNAME, new PdfString("http://www.color.org"));
            outp.Put(PdfName.INFO, new PdfString(""));
            outp.Put(PdfName.S, PdfName.GTS_PDFX);

            PdfArray outputIntents = extraCatalog.GetAsArray(PdfName.OUTPUTINTENTS);
            if (outputIntents == null)
            {
                outputIntents = new PdfArray();
            }
            outputIntents.Add(outp);
            extraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);
        }

        //public bool IsPdfX32002()
        //{
        //    return pdfxConformance == PdfWriter.PDFX32002;
        //}

        //public bool IsPdfX1A2001()
        //{
        //    return pdfxConformance == PdfWriter.PDFX1A2001;
        //}
        public static void CompleteInfoDictionary( PdfDictionary info)
        {
            //if (info.Get(PdfName.GTS_PDFXVERSION) == null)
            //{
            //    info.Put(PdfName.GTS_PDFXVERSION, new PdfString("PDF/X-1:2001"));
            //    info.Put(new PdfName("GTS_PDFXConformance"), new PdfString("PDF/X-1a:2001"));
            //}

            //if (info.Get(PdfName.TITLE) == null)
            //{
            //    info.Put(PdfName.TITLE, new PdfString("Pdf document"));
            //}
            //if (info.Get(PdfName.CREATOR) == null)
            //{
            //    info.Put(PdfName.CREATOR, new PdfString("Unknown"));
            //}
            //if (info.Get(PdfName.TRAPPED) == null)
            //{
            //    info.Put(PdfName.TRAPPED, new PdfName("False"));
            //}
            info.Put(PdfName.TITLE, new PdfString("PDF/A-2B Compliant Document"));
            info.Put(PdfName.AUTHOR, new PdfString("Author"));
            info.Put(PdfName.SUBJECT, new PdfString("Subject"));
            info.Put(PdfName.KEYWORDS, new PdfString("Keywords"));
            info.Put(PdfName.CREATOR, new PdfString("Creator"));
            info.Put(PdfName.PRODUCER, new PdfString("Producer"));
            info.Put(PdfName.TRAPPED, new PdfName("False"));


        }




        static void AddCustomMetadata(PdfStamper stamper, string key, string value)
        {
            var info = stamper.MoreInfo ?? new Dictionary<string, string>();
            info[key] = value;
            stamper.MoreInfo = info;
        }

        static void MakePdfA(PdfStamper stamper, string iccProfilePath)
        {
            ICC_Profile icc = ICC_Profile.GetInstance(File.ReadAllBytes(iccProfilePath));
            PdfICCBased pdfIcc = new PdfICCBased(icc);
            pdfIcc.Remove(PdfName.ALTERNATE);
            PdfIndirectObject iccRef = stamper.Writer.AddToBody(pdfIcc);

            PdfDictionary outputIntent = new PdfDictionary();
            outputIntent.Put(PdfName.TYPE, new PdfName("OutputIntent"));
            outputIntent.Put(PdfName.S, new PdfName("GTS_PDFA1"));
            outputIntent.Put(PdfName.OUTPUTCONDITIONIDENTIFIER, new PdfString("sRGB IEC61966-2.1"));
            outputIntent.Put(PdfName.INFO, new PdfString("sRGB IEC61966-2.1"));
            outputIntent.Put(PdfName.DESTOUTPUTPROFILE, iccRef.IndirectReference);

            PdfArray outputIntents = new PdfArray();
            outputIntents.Add(outputIntent);
            stamper.Writer.ExtraCatalog.Put(PdfName.OUTPUTINTENTS, outputIntents);

            PdfDictionary defaultRgb = new PdfDictionary();
            defaultRgb.Put(PdfName.DEVICERGB, iccRef.IndirectReference);
            PdfDictionary resources = stamper.Writer.ExtraCatalog.GetAsDict(PdfName.RESOURCES) ?? new PdfDictionary();
            resources.Put(PdfName.DEFAULT, defaultRgb);
            stamper.Writer.ExtraCatalog.Put(PdfName.RESOURCES, resources);

            stamper.CreateXmpMetadata();
        }

        static void AddPdfAIdentificationToXmp(PdfStamper stamper, PdfReader reader, FileStream fs)
        {
          
            try
            {
               
                // Create a new AcroFields instance (assuming no existing fields)
                AcroFields fields = stamper.AcroFields;
                PdfAConformanceLevel conformanceLevel = PdfAConformanceLevel.PDF_A_2B;

                // Set custom metadata fields for PDF/A identification
                fields.SetFieldProperty("pdfaid:part", "1", false, null); // Change value for different conformance levels
                fields.SetFieldProperty("pdfaid:conformance", "https://www.loc.gov/preservation/digital/formats/fdd/fdd000318.shtml", false, null);

                // Close the stamper and reader
                //stamper.FormFlattening = true; // Flatten form fields (optional)
                //stamper.Close();
                //reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding PDF/A identification: {ex.Message}");
                
            }
        }
      
        //public class PdfA1Schema : XmpSchema
        //{
        //    public PdfA1Schema() : base("http://www.aiim.org/pdfa/ns/id/")
        //    {
        //        // Initialize or configure schema properties if necessary
        //    }
        //}

        public static DataTable LoadCsvToDataTable(string csvFilePath)
        {
            DataTable dt = new DataTable();
            try
            {
                using (TextFieldParser parser = new TextFieldParser(csvFilePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");

                    // Read the column headers
                    string[] headers = parser.ReadFields();
                    if (headers != null)
                    {
                        foreach (string header in headers)
                        {
                            dt.Columns.Add(header);
                        }

                        // Read the data rows
                        while (!parser.EndOfData)
                        {
                            string[] fields = parser.ReadFields();
                            if (fields != null)
                            {
                                DataRow row = dt.NewRow();
                                for (int i = 0; i < headers.Length; i++)
                                {
                                    row[i] = fields[i];
                                }
                                dt.Rows.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return dt;
        }
    }
}
