﻿using System;
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
        public string ExportToPdf(string fileName, DataTable dataTable)
        {
            var pdfDocument = new Document();
            var pdfFile = string.Format("{0}{1}{2}.pdf", Path.GetTempPath(), fileName, DateTime.Now.ToString("ddMMyyyyhhmmss"));
            if (File.Exists(pdfFile))
            {
                File.Delete(pdfFile);
            }
            var pdfWriter = PdfWriter.GetInstance(pdfDocument, new FileStream(pdfFile, FileMode.Create));
            pdfDocument.Open();
            var pdfPTable = new PdfPTable(dataTable.Columns.Count);
            pdfPTable.WidthPercentage = 100f;
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
            pdfDocument.Add(pdfPTable);
            pdfDocument.Close();
            return pdfFile;
        }
    }
}