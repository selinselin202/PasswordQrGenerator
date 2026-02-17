using QRCoder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/generate", (List<PasswordEntry> entries) =>
{
    var results = new List<QrResult>();

    foreach (var entry in entries)
    {
        if (string.IsNullOrWhiteSpace(entry.Password))
            continue;

        var qrGenerator = new QRCodeGenerator();
        var qrData = qrGenerator.CreateQrCode(entry.Password, QRCodeGenerator.ECCLevel.Q);
        var qrCode = new PngByteQRCode(qrData);
        var imageBytes = qrCode.GetGraphic(10);
        var base64 = Convert.ToBase64String(imageBytes);

        var result = new QrResult(entry.Label, base64);
        results.Add(result);
    }

    return Results.Ok(results);
});

app.Run();

record PasswordEntry(string Label, string Password);
record QrResult(string Label, string Base64Image);