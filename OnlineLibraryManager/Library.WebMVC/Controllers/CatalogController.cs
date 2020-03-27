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
                    Dewey = _assetsService.GetDeweyIndex(a.Id),
                    NumberOfCopies = a.NumberOfCopies
                }).ToList();

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var asset = _assetsService.Get(id);


            var model = new AssetDetailModel
            {
                AssetId = id,
                Title = asset.Title,
                Type = _assetsService.GetType(id),
                Year = asset.Year,
                Cost = asset.Cost,
                Status = asset.Status.Name,
                ImageUrl = asset.ImageUrl,
                AuthorOrDirector = _assetsService.GetAuthorOrDirector(id),
                CurrentLocation = _assetsService.GetCurrentLocation(id)?.Name,
                Dewey = _assetsService.GetDeweyIndex(id),
                Isbn = _assetsService.GetIsbn(id), 
                CurrentAssociatedLibraryCard = _assetsService.GetLibraryCardByAssetId(id),
            };

            return View(model);
        }
    }
}