using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace App.Controllers;

[ApiController]
[Route("[controller]")]
public class ImageController : Controller
{
    private readonly IDocumentStore _store;

    public ImageController(IDocumentStore store)
    {
        _store = store;
    }

    [HttpGet]
    [Route("{docId}/{fileName}")]
    public IActionResult Index(string docId, string fileName)
    {
        try
        {
            using var session = _store.OpenSession();

            string decodedDocId = Uri.UnescapeDataString(docId);
            using var attachment = session.Advanced.Attachments.Get(decodedDocId, fileName);

            Stream stream = attachment.Stream;
            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);

            return File(ms.ToArray(), attachment.Details.ContentType);
        }
        catch
        {
            return NotFound();
        }
    }
}
