using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.WebMVC.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService _assetsService;

        public CatalogController(ILibraryAssetService assetsService)
        {
            _assetsService = assetsService;
        }

        public IActionResult Index()
        {
            var assetModels = _assetsService.GetAll();

            var model = assetModels
                .Select(a => new AssetListingModel
                {
                    Id = a.Id,
                    ImageUrl = a.ImageUrl,
                    Title = _assetsService.GetTitle(a.Id),
                    AuthorOrDirector = _assetsService.GetAuthorOrDirector(a.Id),
                    Type = _assetsService.GetType(a.Id),
                    Dewey = _assetsService.GetDeweyIndex(a.Id)    
                }).ToList();

            return View(model);
        }
    }
}