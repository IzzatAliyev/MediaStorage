using MediaStorage.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace MediaStorage.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly MediaStorageViewModel mediaStore;

        public HomeController(MediaStorageViewModel mediaStore)
        {
            this.mediaStore = mediaStore;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(IFormFile mediaFile)
        {
            string mediaUrl = mediaStore.StoreMedia(mediaFile);
            ViewBag.MediaUrl = mediaUrl;
            return View(mediaStore);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var mediaFiles = Directory.GetFiles(mediaStore.MediaFolder);
            var mediaViewModels = mediaFiles.Select(file => new MediaViewModel
            {
                FileName = Path.GetFileName(file),
                Url = GetMediaUrl(Path.GetFileName(file))
            }).ToList();

            return View(mediaViewModels);
        }

        [HttpGet]
        public IActionResult Media(string mediaUrl)
        {
            byte[] mediaData = mediaStore.GetMedia(mediaUrl);
            ViewBag.MediaData = mediaData;
            ViewBag.MediaUrl = mediaUrl;
            return View();
        }

        private string GetMediaUrl(string fileName)
        {
            return Url.Content($"~/media/{fileName}");
        }
    }

}
