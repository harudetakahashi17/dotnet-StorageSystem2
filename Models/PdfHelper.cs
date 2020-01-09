using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace StorageSystem.Models
{
    public class PdfHelper
    {
        private static readonly Font font = new Font(Font.FontFamily.HELVETICA, 8, 1);
        public string ExportToPdf(string fileName, DataTable dataTable)
        {
            var pdfDocument = new Document(PageSize.A4, 25f, 25f, 25f, 25f);
            var pdfFile = string.Format("{0}{1}{2}.pdf", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName, DateTime.Now.ToString("ddMMyyyyhhmmss"));
            if (File.Exists(pdfFile))
            {
                File.Delete(pdfFile);
            }
            var pdfWriter = PdfWriter.GetInstance(pdfDocument, new FileStream(pdfFile, FileMode.Create));
            pdfDocument.Open();

            // Header Part as Table
            var headerDoc = new PdfPTable(1);
            headerDoc.WidthPercentage = 100;
            var headerDocCell = new PdfPCell(new Phrase("Data Barang", new Font(Font.FontFamily.HELVETICA, 16, 1)));
            headerDocCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            headerDocCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
            headerDocCell.Border = 0;
            headerDoc.AddCell(headerDocCell);

            var pdfPTable = new PdfPTable(dataTable.Columns.Count);
            float[] colWidth = new[] { 2.5f, 2.5f, 8f, 6f, 5f, 3f, 8f, 8f };
            pdfPTable.SetWidths(colWidth);
            for(int i = 0; i < dataTable.Columns.Count; i++)
            {
                var pdfPCell = new PdfPCell(new Phrase(dataTable.Columns[i].ColumnName, new Font(Font.FontFamily.HELVETICA, 8, 1)));
                pdfPCell.Padding = 2;
                pdfPCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                pdfPCell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                pdfPCell.BackgroundColor = new BaseColor(200, 200, 200);
                pdfPTable.AddCell(pdfPCell);
            }
            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                for (int column = 0; column < dataTable.Columns.Count; column++)
                {
                    var cell = new PdfPCell(new Phrase(dataTable.Rows[row][column].ToString(), new Font(Font.FontFamily.HELVETICA, 8, 1)));
                    cell.Padding = 5;
                    cell.HorizontalAlignment = PdfPCell.ALIGN_CENTER; ;
                    cell.VerticalAlignment = PdfPCell.ALIGN_CENTER;
                    pdfPTable.AddCell(cell);
                }
            }

            pdfDocument.Add(headerDoc);
            pdfDocument.Add(new Paragraph("\n"));
            pdfDocument.Add(new Paragraph("\n"));
            pdfDocument.Add(pdfPTable);
            pdfDocument.Add(new Phrase("Created on: " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"), font));
            pdfDocument.Close();
            return pdfFile;
        }
    }
}