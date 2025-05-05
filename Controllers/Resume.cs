using HandlebarsDotNet;
using Microsoft.AspNetCore.Mvc;
using ResumeApiExample.Services;

namespace ResumeApiExample.Controllers;

[ApiController]
[Route("resume")]
public class ResumeController(IPdfService pdfService) : ControllerBase
{
    private readonly IPdfService _pdfService = pdfService;

    [HttpGet("pdf")]
    public IActionResult GetResume()
    {
        var data = new
        {
            name = "John Doe",
            title = "Software Engineer",
            summary = "Experienced in building modern web applications.",
            experiences = new[]
            {
                new
                {
                    company = "ACME Corp",
                    role = "Senior Dev",
                    startDate = "2018",
                    endDate = "2022",
                    description = "Worked on microservices."
                },
                new
                {
                    company = "Beta Inc",
                    role = "Engineer",
                    startDate = "2015",
                    endDate = "2018",
                    description = "Built internal tools."
                }
            }
        };

        var html = Templater.RenderTemplate(data);

        var pdfBytes = _pdfService.GeneratePdf(html);

        return File(pdfBytes, "application/pdf", "resume.pdf");
    }
}

public static class Templater
{
    private static readonly string _templatePath = Path.Combine("Templates", "resume_template.html");

    public static string RenderTemplate(object data)
    {
        var templateText = File.ReadAllText(_templatePath);
        var template = Handlebars.Compile(templateText);
        return template(data);
    }
}
