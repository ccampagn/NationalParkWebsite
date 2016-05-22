
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
        public void helloworld()
        {
            const string text =
  "Facin exeraessisit la consenim iureet dignibh eu facilluptat vercil dunt autpat. " +
  "Ecte magna faccum dolor sequisc iliquat, quat, quipiss equipit accummy niate magna " +
  "facil iure eraesequis am velit, quat atis dolore dolent luptat nulla adio odipissectet " +
  "lan venis do essequatio conulla facillandrem zzriusci bla ad minim inis nim velit eugait " +
  "aut aut lor at ilit ut nulla ate te eugait alit augiamet ad magnim iurem il eu feuissi.\n" +
  "Guer sequis duis eu feugait luptat lum adiamet, si tate dolore mod eu facidunt adignisl in " +
  "henim dolorem nulla faccum vel inis dolutpatum iusto od min ex euis adio exer sed del " +
  "dolor ing enit veniamcon vullutat praestrud molenis ciduisim doloborem ipit nulla consequisi.\n" +
  "Nos adit pratetu eriurem delestie del ut lumsandreet nis exerilisit wis nos alit venit praestrud " +
  "dolor sum volore facidui blaor erillaortis ad ea augue corem dunt nis  iustinciduis euisi.\n" +
  "Ut ulputate volore min ut nulpute dolobor sequism olorperilit autatie modit wisl illuptat dolore " +
  "min ut in ute doloboreet ip ex et am dunt at.";

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Times New Roman", 10, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(gfx);

            XRect rect = new XRect(40, 100, 100, 100);
            gfx.DrawRectangle(XBrushes.SeaShell, rect);
            //tf.Alignment = ParagraphAlignment.Left;
            tf.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);

           
            const string filename = @"C:\barcode\test.pdf";
            document.Save(filename);
            // ...and start a viewer.
            Process.Start(filename);
        }
    }
}