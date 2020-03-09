using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Regionalizer.Entities;
using Regionalizer.Services;

namespace Regionalizer.Controllers
{
    public class MunicipalitiesController : Controller
    {
        private readonly IMunicipalityService _service;

        public MunicipalitiesController(IMunicipalityService service)
        {
            _service = service;
        }

        // GET: Municipality
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        // GET: Municipality/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _service.Get(id.Value);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // GET: Municipality/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Municipality/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunicipalityId,Name,IsActive")] Municipality municipality)
        {
            if (ModelState.IsValid)
            {
                await _service.Add(municipality);
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        // GET: Municipality/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _service.Get(id.Value);
            if (municipality == null)
            {
                return NotFound();
            }
            return View(municipality);
        }

        // POST: Municipality/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MunicipalityId,Name,IsActive")] Municipality municipality)
        {
            if (id != municipality.MunicipalityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.Update(municipality);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(municipality);
        }

        // GET: Municipality/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipality = await _service.Get(id.Value);
            if (municipality == null)
            {
                return NotFound();
            }

            return View(municipality);
        }

        // POST: Municipality/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
