using System;
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
    public class FormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Forms
        public async Task<IActionResult> Index(string? id)
        {

            if (id == null){

                var forms = _context.Form
                    .Include(p => p.Pokemon)
                    .Include(r => r.PrevoForm)
                    .Include(t => t.Type1)
                    .Include(t2 => t2.Type2)
                    .OrderBy(f => f.Id);

                return View(await forms.ToListAsync());

            } else {

                var segments = id.Split('|');


                if (segments.Count() == 0){

                    return NotFound();

                } else {

                    if (segments[0] == "generation"){
                        var formsByGeneration = await _context.Form
                            .Include(p => p.Pokemon)
                            .Include(t => t.Type1)
                            .Include(t => t.Type2)
                            .Where(m => m.generation == Int32.Parse(segments[1]))
                            .ToListAsync();
                        return View(formsByGeneration);
        
                    } else if (segments[0]=="type"){

                        var type = await _context.PkmnType
                            .Where(m => m.Name == segments[1])
                            .FirstOrDefaultAsync();           
            
                        var formsByType = await _context.Form
                            .Include(p => p.Pokemon)
                            .Include(t => t.Type1)
                            .Include(t => t.Type2)
                            .Where(m => 
                            (m.Type1.Id == type.Id || 
                             m.Type2.Id == type.Id))
                            .ToListAsync();
                        return View(formsByType);
                             
                    } else if (segments[0]=="ability"){        
            
                        var formsByType = await _context.Form
                            .Include(p => p.Pokemon)
                            .Include(t => t.Type1)
                            .Include(t => t.Type2)
                            .Where(m => 
                            (m.ability1 == segments[1] || 
                             m.ability0 == segments[1] ||
                             m.hiddenAbility == segments[1] || 
                             m.specialAbility == segments[1]))
                            .ToListAsync();
                        return View(formsByType);

                    } else if (segments[0] == "eggGroup"){

                        var formByEggGroup = await _context.Form
                            .Include(p => p.Pokemon)
                            .Include(f => f.Type1)
                            .Include(f => f.Type2)
                            .Where(m =>
                            m.eggGroup1 == segments[1] ||
                            m.eggGroup2 == (segments[1]))
                            .ToListAsync();
                        return View(formByEggGroup);

                    }  else if (segments[0] == "color"){

                        var formByColor = await _context.Form
                            .Include(p => p.Pokemon)
                            .Include(t => t.Type1)
                            .Include(t => t.Type2)
                            .Where(m =>
                            m.color == segments[1])
                            .ToListAsync();
                        return View(formByColor);
                    }   
                }
            }


        return NotFound();
        }


        // GET: Forms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .Include(f => f.Pokemon)
                .Include(p => p.PrevoForm)
                .Include(r => r.Type1)
                .Include(t => t.Type2)
                .FirstOrDefaultAsync(m => m.Id == id);


            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // GET: Forms/Create
        public IActionResult Create()
        {
            ViewData["PokemonId"] = new SelectList(_context.Set<Pokemon>(), "Id", "Id");
            ViewData["PrevoFormId"] = new SelectList(_context.Form, "Id", "Id");
            ViewData["Type1Id"] = new SelectList(_context.PkmnType, "Id", "Id");
            ViewData["Type2Id"] = new SelectList(_context.PkmnType, "Id", "Id");
            return View();
        }

        // POST: Forms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PokemonId,Name,isBase,Type1Id,Type2Id,genderRatio,hp,atk,def,spa,spd,spe,ability0,ability1,hiddenAbility,heightm,weightkg,color,eggGroup1,eggGroup2,PrevoFormId,evoLevel,canGigantamax,forme,requiredItem,evoCondition,evoType,evoItem,evoRegion,evoMove")] Form form)
        {
            if (ModelState.IsValid)
            {
                _context.Add(form);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PokemonId"] = new SelectList(_context.Set<Pokemon>(), "Id", "Id", form.PokemonId);
            ViewData["PrevoFormId"] = new SelectList(_context.Form, "Id", "Id", form.PrevoFormId);
            ViewData["Type1Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type1Id);
            ViewData["Type2Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type2Id);
            return View(form);
        }

        // GET: Forms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form.FindAsync(id);
            if (form == null)
            {
                return NotFound();
            }
            ViewData["PokemonId"] = new SelectList(_context.Set<Pokemon>(), "Id", "Id", form.PokemonId);
            ViewData["PrevoFormId"] = new SelectList(_context.Form, "Id", "Id", form.PrevoFormId);
            ViewData["Type1Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type1Id);
            ViewData["Type2Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type2Id);
            return View(form);
        }

        // POST: Forms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PokemonId,Name,isBase,Type1Id,Type2Id,genderRatio,hp,atk,def,spa,spd,spe,ability0,ability1,hiddenAbility,heightm,weightkg,color,eggGroup1,eggGroup2,PrevoFormId,evoLevel,canGigantamax,forme,requiredItem,evoCondition,evoType,evoItem,evoRegion,evoMove")] Form form)
        {
            if (id != form.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(form);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormExists(form.Id))
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
            ViewData["PokemonId"] = new SelectList(_context.Set<Pokemon>(), "Id", "Id", form.PokemonId);
            ViewData["PrevoFormId"] = new SelectList(_context.Form, "Id", "Id", form.PrevoFormId);
            ViewData["Type1Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type1Id);
            ViewData["Type2Id"] = new SelectList(_context.PkmnType, "Id", "Id", form.Type2Id);
            return View(form);
        }

        // GET: Forms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var form = await _context.Form
                .Include(p => p.Pokemon)
                .Include(f => f.PrevoForm)
                .Include(f => f.Type1)
                .Include(f => f.Type2) 
                .FirstOrDefaultAsync(m => m.Id == id);
            if (form == null)
            {
                return NotFound();
            }

            return View(form);
        }

        // POST: Forms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var form = await _context.Form.FindAsync(id);
            if (form != null)
            {
                _context.Form.Remove(form);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormExists(int id)
        {
            return _context.Form.Any(e => e.Id == id);
        }
    }
}
