using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System.IO;
using Microsoft.AspNetCore.Http;
using System;

namespace WeddingWebsiteCore.Helpers
{
    public class ImageFactory
    {
        private static int _maxHeight = 500;
        private static int _maxWidth = 500;

        public static Models.Image FromFile(IFormFile file)
        {
            var imageData = GetDataFrom(file);
            var previewImageData = GetPreviewFrom(imageData);

            return new Models.Image
            {
                Data = imageData,
                Name = file.Name,
                Preview = previewImageData
            };
        }

        private static byte[] GetDataFrom(IFormFile file)
        {
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                return stream.ToArray();
            }
        }

        private static byte[] GetPreviewFrom(byte[] imageData)
        {
            using (var image = Image.Load(imageData))
            {
                image.Mutate(x => x.Resize(GetPreviewSize(image)));

                using (var stream = new MemoryStream())
                {
                    image.SaveAsPng(stream);
                    return stream.ToArray();
                }
            }
        }

        private static Size GetPreviewSize(Image input)
        {
            var height = input.Height;
            var width = input.Width;

            double heightRatio = (double)_maxHeight / (double)height;
            double widthRatio = (double)_maxWidth / (double)width;

            var adjustmentRatio = heightRatio < widthRatio ? heightRatio : widthRatio;

            var adjustedHeight = height * adjustmentRatio;
            var adjustedWidth = width * adjustmentRatio;

            return new Size
            {
                Height = Convert.ToInt32(adjustedHeight),
                Width = Convert.ToInt32(adjustedWidth)
            };
        }
    }
}
