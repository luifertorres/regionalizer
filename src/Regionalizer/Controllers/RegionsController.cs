using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Regionalizer.Entities;
using Regionalizer.Services;
using Regionalizer.Models;

namespace Regionalizer.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IRegionService _service;

        public RegionsController(IRegionService service)
        {
            _service = service;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _service.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            var regionMunicipalities = new RegionMunicipalitiesViewModel
            {
                Region = region
            };

            return View(regionMunicipalities);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Region region)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(region);
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _service.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Regions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Region region)
        {
            if (id != region.RegionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(region);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _service.Get(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Regions/EditMunicipalities/5
        public async Task<IActionResult> EditMunicipalities(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _service.Get(id.Value);

            if (region == null)
            {
                return NotFound();
            }

            var municipalities = await _service.GetAllMunicipalities();
            var regionMunicipalities = new RegionMunicipalitiesViewModel
            {
                Region = region,
                AllMunicipalities = municipalities
            };

            return View(regionMunicipalities);
        }

        // POST: Regions/EditMunicipalities/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMunicipality(RegionMunicipalitiesViewModel regionMunicipalities)
        {
            var region = regionMunicipalities.Region;

            try
            {
                var selected = regionMunicipalities.SelectedMunicipality.Value;

                if (!int.TryParse(selected, out var municipalityId))
                {
                    return RedirectToAction(nameof(EditMunicipalities), region.RegionId);
                }

                await _service.AddMunicipalityToRegion(region, municipalityId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(EditMunicipalities), region.RegionId);
        }
    }
}
