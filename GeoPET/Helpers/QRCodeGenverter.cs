using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using GeoPet.Entities;
using QRCoder;

namespace GeoPet.Helpers;

public class QRCodePetData
{
    public string Name { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Age { get; set; }
    public QRCodeCarerData Carer { get; set; } = new();
}

public class QRCodeCarerData
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public static class QRCodeConverter
{
    public static string ToQRCode(Pet pet)
    {
        QRCodeCarerData thing = new QRCodeCarerData
        {
            Name = pet.PetCarer.Name,
            Email = pet.PetCarer.Email,
            ZipCode = pet.PetCarer.ZipCode
        };
        QRCodePetData petData = new QRCodePetData
        {
            Name = pet.Name,
            Breed = pet.Breed?.Name ?? "Unknown",
            Weight = pet.Weight,
            Age = pet.Age,
            Carer = thing
        };
        var stringData = JsonSerializer.Serialize(petData);
        return GenerateQRString(stringData);
    }
    public static Bitmap CreateImage(string stringData)
    {
        QRCodeGenerator qrCodeGenerator = new();
        var qrCodeData = qrCodeGenerator.CreateQrCode(stringData, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new(qrCodeData);
        return qrCode.GetGraphic(10);
    }

    public static string GenerateQRString(string stringData)
    {
        var qrCodeImage = CreateImage(stringData);
        var image = ImageByteConvert(qrCodeImage);
        return ByteStringConvert(image);
    }

    private static byte[] ImageByteConvert(Image img)
    {
        using var stream = new MemoryStream();
        img.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    private static string ByteStringConvert(byte[] byteData)
    {
        string imreBase64Data = Convert.ToBase64String(byteData);
        return string.Format("data:image/png;base64,{0}", imreBase64Data);
    }
}
