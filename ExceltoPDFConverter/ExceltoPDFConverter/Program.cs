class Program
{
    static void Main(string[] args)
    {
        string inputPath = "test2.xlsx";
        string outputPath = "output.pdf";

        try
        {
            if (!File.Exists(inputPath))
                throw new FileNotFoundException($"Il file '{inputPath}' non esiste.");

            Console.WriteLine("Lettura file Excel...");

            var converter = new ExcelToPdfConverter();
            byte[] excelBytes = File.ReadAllBytes(inputPath);
            byte[] pdfBytes = converter.ConvertExcelBinaryToPdf(excelBytes);
            File.WriteAllBytes(outputPath, pdfBytes);

            Console.WriteLine($"Conversione completata. File salvato come '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la conversione:");
            Console.WriteLine(ex.ToString());
        }
    }
}