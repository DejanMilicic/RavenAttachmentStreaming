using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Raven.Client.Documents;

namespace App.Controllers;

[Route("[controller]")]
public class ImageController : Controller
{
    private readonly IDocumentStore _store;

    public ImageController(IDocumentStore store)
    {
        _store = store;
    }

    public FileResult Index()
    {
        using var session = _store.OpenSession();

        using var attachment = session.Advanced.Attachments.Get("employees/8-A", "photo.jpg");

        Stream stream = attachment.Stream;
        MemoryStream ms = new MemoryStream();
        stream.CopyTo(ms);

        return File(ms.ToArray(), attachment.Details.ContentType);
    }
}
