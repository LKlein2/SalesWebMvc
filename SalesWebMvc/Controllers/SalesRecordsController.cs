using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initial, DateTime? final)
        {
            if (!initial.HasValue)
            {
                initial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!final.HasValue)
            {
                final = DateTime.Now;
            }
            //Serve para deixar a data salva no formulario
            ViewData["initial"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["final"] = final.Value.ToString("yyyy-MM-dd");

            ViewData["initialLabel"] = initial.Value.ToString("dd/MM/yyyy");
            ViewData["finalLabel"] = final.Value.ToString("dd/MM/yyyy");

            var result = await _salesRecordService.FindByDateAsync(initial, final);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch(DateTime? initial, DateTime? final)
        {
            if (!initial.HasValue)
            {
                initial = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!final.HasValue)
            {
                final = DateTime.Now;
            }
            ViewData["initial"] = initial.Value.ToString("yyyy-MM-dd");
            ViewData["final"] = final.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(initial, final);
            return View(result);
        }
    }
}