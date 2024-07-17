// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using System.Drawing;

public class ImageToPdfConverter
{
    public static void ConvertImagesToPdf(string imagesDirectory, string outputPdfPath)
    {
        string[] imageFiles = Directory.GetFiles(imagesDirectory, "*.jpg");

        using (FileStream fs = new FileStream(outputPdfPath, FileMode.Create))
        {
            using (Document doc = new Document())
            {
                using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                {
                    doc.Open();

                    foreach (string imageFile in imageFiles)
                    {
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageFile);
                        doc.SetPageSize(new iTextSharp.text.Rectangle(image.Width+20, image.Height));
                        doc.NewPage();
                        doc.Add(image);
                    }

                    doc.Close();
                }
            }
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        //string imagesDirectory = @"C:\Users\ritik\Desktop\TestInputfolder2"; // Replace with the path to the directory containing images
        //string outputPdfPath = @"C:\Users\ritik\Desktop\testfolderoutputpdf\Output.pdf"; // Replace with the desired output PDF path

        string imagesDirectory = @"D:\ImgToWord\img"; // Replace with the path to the directory containing images
        string outputPdfPath = @"D:\ImgToWord\Output.pdf"; // Replace with the desired output PDF path

        ImageToPdfConverter.ConvertImagesToPdf(imagesDirectory, outputPdfPath);

        Console.WriteLine("Images converted to PDF successfully!");

    }
}



