using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Data;
using Library.WebMVC.Models.Branch;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebMVC.Controllers
{
    public class BranchController : Controller
    {
        private readonly ILibraryBranchService _branchService;

        public BranchController(ILibraryBranchService branchService)
        {
            _branchService = branchService;
        }

        public IActionResult Index()
        {
            var model = _branchService.GetAll()
                .Select(br => new BranchDetailModel
                {
                    Id = br.Id,
                    BranchName = br.Name,
                    Address = br.Address,
                    NumberOfAssets = _branchService.GetAssetCount(br.Id),
                    NumberOfPatrons = _branchService.GetPatronCount(br.Id)
                }).ToList();

            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var branch = _branchService.Get(id);
            var model = new BranchDetailModel
            {
                BranchName = branch.Name,
                Description = branch.Description,
                Address = branch.Address,
                Telephone = branch.Telephone,
                BranchOpenedDate = branch.OpenDate.ToString("yyyy.MM.dd"),
                NumberOfPatrons = _branchService.GetPatronCount(id),
                NumberOfAssets = _branchService.GetAssetCount(id),
                TotalAssetValue = _branchService.GetAssetsValue(id),
                ImageUrl = branch.ImageUrl,
                HoursOpen = _branchService.GetBranchHours(id)
            };

            return View(model);
        }
    }
}