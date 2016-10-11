
using KeepAutomation.Barcode.Bean;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using Root.Reports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace NationalParkServiceSystem.Models
{
    public class createpdf
    {
        public void createpass(useraccount user)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana",32);
            //add pic for nps sign and park name
           
            XPen pen = new XPen(XColors.Navy, Math.PI);
            XRect billingaddress = new XRect(10, 10, 400, 300);
            XRect parkinfo = new XRect(10, 320, 580, 300);
            XBrush brush = XBrushes.Black;
            XStringFormat format = new XStringFormat();
            gfx.DrawRectangle(XPens.Black, billingaddress);
            gfx.DrawRectangle(XPens.Black, parkinfo);
            format.Alignment = XStringAlignment.Center;
            billingaddress.Offset(0, 0);
            gfx.DrawString(user.getaddress().getfirstname(), font, brush, billingaddress, format);
            billingaddress.Offset(0, 50);
            gfx.DrawString(user.getaddress().getlastname(), font, brush, billingaddress, format);
            billingaddress.Offset(0, 50);
            gfx.DrawString(user.getaddress().getaddress1(), font, brush, billingaddress, format);
            billingaddress.Offset(0, 50);
            gfx.DrawString(user.getaddress().getcity() + "," + user.getaddress().getstate(), font, brush, billingaddress, format);
            billingaddress.Offset(0, 50);
            gfx.DrawString(user.getaddress().getzipcode(), font, brush, billingaddress, format);
          

  

            //createbarcode

  
      BarCode barcodes = new BarCode();
      barcodes.Symbology = KeepAutomation.Barcode.Symbology.EAN13;
     barcodes.CodeToEncode = "123456789012";
     barcodes.ChecksumEnabled = true;

     barcodes.Orientation = KeepAutomation.Barcode.Orientation.Degree90;
     barcodes.BarcodeUnit = KeepAutomation.Barcode.BarcodeUnit.Pixel;
     barcodes.DPI= 1024;

     barcodes.ImageFormat = System.Drawing.Imaging.ImageFormat.Gif;
     barcodes.generateBarcodeToImageFile(@"C:\barcode\newtest2.gif");
     XImage image = XImage.FromFile(@"C:\barcode\newtest2.gif");
     gfx.DrawImage(image, 420, 10, 180, 300);

            // Save the document...
            string filename = @"C:\barcode\newtest.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
       
    }
}