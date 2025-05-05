using System.Diagnostics;

namespace ResumeApiExample.Services;

public interface IPdfService
{
    byte[] GeneratePdf(string html);
}

public class PdfService(IConfiguration configuration) : IPdfService
{
    private readonly IConfiguration _configuration = configuration;

    public byte[] GeneratePdf(string html)
    {
        var wkhtmltopdfPath = _configuration["wkhtmltopdfPath"];
        // Write HTML to a temp file
        var tempHtmlPath = Path.GetTempFileName() + ".html";
        var tempPdfPath = Path.GetTempFileName() + ".pdf";
        File.WriteAllText(tempHtmlPath, html);

        try
        {
            // Build the process to run wkhtmltopdf
            var psi = new ProcessStartInfo
            {
                FileName = wkhtmltopdfPath,
                Arguments = $"\"{tempHtmlPath}\" \"{tempPdfPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = Process.Start(psi);
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var error = process.StandardError.ReadToEnd();
                throw new Exception($"wkhtmltopdf failed: {error}");
            }

            return File.ReadAllBytes(tempPdfPath);
        }
        finally
        {
            File.Delete(tempHtmlPath);
            if (File.Exists(tempPdfPath))
                File.Delete(tempPdfPath);
        }
    }
}
