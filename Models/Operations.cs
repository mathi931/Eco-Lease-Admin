using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;


namespace EcoLease_Admin.Models
{
    public static class Operations
    {
        //converts a string to pdf
        public static PdfDocument toPDF(this String document){
            
            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.PrintOptions.Footer.FontFamily = "Arial";
            htmlToPdf.PrintOptions.Footer.FontSize = 11;
            htmlToPdf.PrintOptions.Footer.LeftText = "Eco Lease";
            htmlToPdf.PrintOptions.Footer.RightText = "{page}";

            var pdfDoc = htmlToPdf.RenderHtmlAsPdf(document);

            return pdfDoc;
        }

        //generates a filename


        //counts the full cost of the lease
        public static int toFullCost(this Reservation reservation)
        {
            int fullCost = 0;

            return fullCost;
        }


    }
}
