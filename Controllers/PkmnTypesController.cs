﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BulbaClone.Data;
using BulbaClone.Models;

namespace BulbaClone.Controllers
{
    public class PkmnTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PkmnTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PkmnTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PkmnType.ToListAsync());
        }

        // GET: PkmnTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pkmnType = await _context.PkmnType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pkmnType == null)
            {
                return NotFound();
            }

            return View(pkmnType);
        }

        // GET: PkmnTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PkmnTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PkmnType pkmnType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pkmnType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pkmnType);
        }

        // GET: PkmnTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pkmnType = await _context.PkmnType.FindAsync(id);
            if (pkmnType == null)
            {
                return NotFound();
            }
            return View(pkmnType);
        }

        // POST: PkmnTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PkmnType pkmnType)
        {
            if (id != pkmnType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pkmnType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PkmnTypeExists(pkmnType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pkmnType);
        }

        // GET: PkmnTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pkmnType = await _context.PkmnType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pkmnType == null)
            {
                return NotFound();
            }

            return View(pkmnType);
        }

        // POST: PkmnTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pkmnType = await _context.PkmnType.FindAsync(id);
            if (pkmnType != null)
            {
                _context.PkmnType.Remove(pkmnType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PkmnTypeExists(int id)
        {
            return _context.PkmnType.Any(e => e.Id == id);
        }
    }
}
