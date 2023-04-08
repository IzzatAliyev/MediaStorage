using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaStorage.Web.Models
{
    public class MediaStorageViewModel
    {
        private readonly string mediaFolder;

        public MediaStorageViewModel(string mediaFolder)
        {
            this.mediaFolder = mediaFolder;
        }

        public string MediaFolder
    {
        get { return mediaFolder; }
    }

        public string StoreMedia(IFormFile mediaFile)
        {
            string fileName = Guid.NewGuid().ToString() + GetFileExtension(mediaFile.ContentType);
            string filePath = Path.Combine(mediaFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                mediaFile.CopyTo(stream);
            }

            return GetMediaUrl(fileName);
        }

        public byte[] GetMedia(string mediaUrl)
        {
            string fileName = Path.GetFileName(mediaUrl);
            string filePath = Path.Combine(mediaFolder, fileName);
            return File.ReadAllBytes(filePath);
        }

        private string GetMediaUrl(string fileName)
        {
            return $"http://localhost:5109/media/{fileName}";
        }

        private string GetFileExtension(string mediaType)
        {
            switch (mediaType)
            {
                case "image/jpeg":
                    return ".jpg";
                case "image/png":
                    return ".png";
                case "video/mp4":
                    return ".mp4";
                default:
                    throw new ArgumentException($"Unsupported media type: {mediaType}");
            }
        }
    }
}