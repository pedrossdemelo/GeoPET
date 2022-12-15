using GeoPet.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing;

namespace GeoPET.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class GetQrCodeController : ControllerBase
    {
        private readonly IPetService _petService;

        public GetQrCodeController(IPetService searchService)
        {
            _petService = searchService;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetQrCode(int id)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            var pet = await _petService.GetPetById(id);
            if (pet == null) return NotFound();

            var json = JsonConvert.SerializeObject(pet);
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(json, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);

            Bitmap qrCodeImage = qRCode.GetGraphic(20);

            var bitmapBytes = BitmapToBytes(qrCodeImage);

            return File(bitmapBytes, "index/html");
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
