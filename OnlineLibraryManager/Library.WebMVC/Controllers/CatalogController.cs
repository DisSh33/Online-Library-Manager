using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.WebMVC.Models;
using Library.WebMVC.Models.Catalog;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService _assetsService;
        private readonly ICheckoutService _checkoutService;

        public CatalogController(ILibraryAssetService assetsService, ICheckoutService checkoutService)
        {
            _assetsService = assetsService;
            _checkoutService = checkoutService;
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

            var currentHolds = _checkoutService.GetCurrentHolds(id).Select(a => new AssetHoldModel
            {
                PatronCardId = a.LibraryCard.Id.ToString(),
                HoldPlaced = _checkoutService.GetCurrentHoldPlaced(a.Id),
                PatronName = _checkoutService.GetCurrentHoldPatron(a.Id)
            });

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
                CheckoutHistory = _checkoutService.GetCheckoutHistory(id),
                LatestCheckout = _checkoutService.GetLatestCheckout(id),
                CurrentHolds = currentHolds,
                PatronName = _checkoutService.GetCurrentPatron(id)
            };

            return View(model);
        }

        public IActionResult Checkout(int id)
        {
            var asset = _assetsService.Get(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                IsCheckedOut = _checkoutService.IsCheckedOut(id)
            };
            return View(model);
        }

        public IActionResult Hold(int id)
        {
            var asset = _assetsService.Get(id);

            var model = new CheckoutModel
            {
                AssetId = id,
                ImageUrl = asset.ImageUrl,
                Title = asset.Title,
                LibraryCardId = "",
                HoldCount = _checkoutService.GetCurrentHolds(id).Count()
            };
            return View(model);
        }

        public IActionResult CheckIn(int id)
        {
            _checkoutService.CheckInItem(id);
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult MarkLost(int id)
        {
            _checkoutService.MarkLost(id);
            return RedirectToAction("Detail", new { id });
        }

        public IActionResult MarkFound(int id)
        {
            _checkoutService.MarkFound(id);
            return RedirectToAction("Detail", new { id });
        }

        [HttpPost]
        public IActionResult PlaceCheckout(int assetId, int libraryCardId)
        {
            _checkoutService.CheckoutItem(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }

        [HttpPost]
        public IActionResult PlaceHold(int assetId, int libraryCardId)
        {
            _checkoutService.PlaceHold(assetId, libraryCardId);
            return RedirectToAction("Detail", new { id = assetId });
        }
    }
}