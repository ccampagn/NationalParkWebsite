
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
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);
            XRect rect = new XRect(0, 0, 250, 140);
            XStringFormat format = new XStringFormat();


            // Draw the text
            gfx.DrawString(user.getaddress().getfirstname()+" "+user.getaddress().getlastname(), font, XBrushes.Black,
              0,30);
            gfx.DrawString(user.getaddress().getaddress1(), font, XBrushes.Black,
           50,60);
            gfx.DrawString(user.getaddress().getcity()+","+user.getaddress().getstate()+" "+user.getaddress().getzipcode(), font, XBrushes.Black,
              90,90);
           

            // Save the document...
            string filename = @"C:\barcode\test.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
       
    }
}