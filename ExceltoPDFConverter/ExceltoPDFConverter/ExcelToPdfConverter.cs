using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

public class ExcelToPdfConverter
{
    public byte[] ConvertExcelBinaryToPdf(byte[] excelData)
    {
        if (excelData == null || excelData.Length == 0)
            throw new ArgumentException("Dati Excel non validi.", nameof(excelData));

        IWorkbook workbook;
        using (var ms = new MemoryStream(excelData))
        {
            workbook = new XSSFWorkbook(ms);
        }

        ISheet sheet = workbook.GetSheetAt(0);
        if (sheet == null)
            throw new Exception("Nessun foglio trovato nel file Excel.");

        double margin = 40;
        double baseCellHeight = 20;
        double baseFontSize = 8;
        XFont baseFont = new XFont("Verdana", baseFontSize);

        // Trova colonne non vuote
        HashSet<int> nonEmptyCols = new HashSet<int>();
        for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            if (row == null) continue;

            for (int j = 0; j < row.LastCellNum; j++)
            {
                if (!string.IsNullOrWhiteSpace(row.GetCell(j)?.ToString()))
                    nonEmptyCols.Add(j);
            }
        }

        if (nonEmptyCols.Count == 0)
            throw new Exception("Il file Excel non contiene dati validi.");

        int[] colIndexes = nonEmptyCols.ToArray();
        Array.Sort(colIndexes);
        int maxCol = colIndexes.Length;

        // Calcola larghezze dinamiche colonne
        double[] columnWidths = new double[maxCol];
        using (var gfxMeasure = XGraphics.CreateMeasureContext(new XSize(1000, 1000), XGraphicsUnit.Point, XPageDirection.Downwards))
        {
            for (int i = 0; i < maxCol; i++)
            {
                int colIndex = colIndexes[i];
                double maxWidth = 50;
                for (int r = sheet.FirstRowNum; r <= sheet.LastRowNum; r++)
                {
                    IRow row = sheet.GetRow(r);
                    if (row == null) continue;

                    string text = row.GetCell(colIndex)?.ToString() ?? "";
                    double width = gfxMeasure.MeasureString(text, baseFont).Width + 10;
                    if (width > maxWidth)
                        maxWidth = width;
                }
                columnWidths[i] = maxWidth;
            }
        }

        // Filtra righe non vuote
        List<IRow> validRows = new List<IRow>();
        for (int i = sheet.FirstRowNum + 1; i <= sheet.LastRowNum; i++)  // ESCLUDE intestazione
        {
            IRow row = sheet.GetRow(i);
            if (row == null) continue;

            bool emptyRow = true;
            foreach (int colIndex in colIndexes)
            {
                if (!string.IsNullOrWhiteSpace(row.GetCell(colIndex)?.ToString()))
                {
                    emptyRow = false;
                    break;
                }
            }
            if (!emptyRow)
                validRows.Add(row);
        }

        IRow headerRow = sheet.GetRow(sheet.FirstRowNum);

        // Dimensioni A4 landscape
        double pageWidth = 842;
        double pageHeight = 595;

        double totalWidth = columnWidths.Sum();
        double scale = (pageWidth - 2 * margin) / totalWidth;
        double scaledCellHeight = baseCellHeight * scale;
        double scaledFontSize = baseFontSize * scale;
        XFont scaledFont = new XFont("Verdana", scaledFontSize);

        int rowsPerPage = (int)((pageHeight - 2 * margin - scaledCellHeight) / scaledCellHeight); // -header
        int totalPages = (int)Math.Ceiling(validRows.Count / (double)rowsPerPage);

        PdfDocument document = new PdfDocument();

        for (int pageIndex = 0; pageIndex < totalPages; pageIndex++)
        {
            PdfPage page = document.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Width = pageWidth;
            page.Height = pageHeight;
            XGraphics gfx = XGraphics.FromPdfPage(page);

            double y = margin;

            // ➤ Intestazione
            double x = margin;
            for (int i = 0; i < maxCol; i++)
            {
                int colIndex = colIndexes[i];
                string text = headerRow.GetCell(colIndex)?.ToString() ?? "";
                double colWidth = columnWidths[i] * scale;

                gfx.DrawRectangle(XPens.Black, x, y, colWidth, scaledCellHeight);
                XRect rect = new XRect(x, y, colWidth, scaledCellHeight);
                gfx.DrawString(text, scaledFont, XBrushes.Black, rect, XStringFormats.Center);

                x += colWidth;
            }

            y += scaledCellHeight;

            // ➤ Righe di dati
            int startRow = pageIndex * rowsPerPage;
            int endRow = Math.Min(startRow + rowsPerPage, validRows.Count);
            for (int i = startRow; i < endRow; i++)
            {
                IRow row = validRows[i];
                x = margin;
                for (int c = 0; c < maxCol; c++)
                {
                    int colIndex = colIndexes[c];
                    string cellText = row.GetCell(colIndex)?.ToString() ?? "";
                    double colWidth = columnWidths[c] * scale;

                    gfx.DrawRectangle(XPens.Black, x, y, colWidth, scaledCellHeight);
                    XRect rect = new XRect(x, y, colWidth, scaledCellHeight);
                    gfx.DrawString(cellText, scaledFont, XBrushes.Black, rect, XStringFormats.Center);

                    x += colWidth;
                }
                y += scaledCellHeight;
            }
        }

        using (var msOut = new MemoryStream())
        {
            document.Save(msOut, false);
            return msOut.ToArray();
        }
    }
}
