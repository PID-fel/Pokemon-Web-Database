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

            var allForms = await _context.Form
                .Include(p => p.Pokemon)
                .Include(r => r.PrevoForm)
                .Include(t => t.Type1)
                .Include(t2 => t2.Type2)
                .OrderBy(f => f.Id)
                .ToListAsync();
                    
                return View(allForms);

            } else {

                int? generation = null;
                string? type1Value = null;
                string? type2Value = null;
                string? ability = null;
                string? eggGroup1 = null;
                string? eggGroup2 = null;
                string? color = null;
                bool includeAlternateForms = true;

                var segments = id.Split('|');

                if (segments.Count() == 0){

                    return NotFound();

                } else {

                foreach (var segment in segments)
                {
                    string attribute = segment.Split('=')[0];
                    string value = segment.Split('=')[1];

                    if (attribute == "generation"){
                        generation = Int32.Parse(value);
                    } else if (attribute=="type1"){
                        type1Value = value; 
                    } else if (attribute=="type2"){
                        type2Value = value; 
                    } else if (attribute=="ability"){
                        ability = value; 
                    } else if (attribute=="eggGroup1"){
                        eggGroup1 = value; 
                    } else if (attribute=="eggGroup2"){
                        eggGroup2 = value; 
                    } else if (attribute=="color"){
                        color = value; 
                    } else if (attribute=="forms"){
                        if (string.Equals(value, "false", StringComparison.OrdinalIgnoreCase)){
                            includeAlternateForms = false; 
                        }
                    }
                }
                 
                var forms = await _context.Form
                    .Include(p => p.Pokemon)
                    .Include(r => r.PrevoForm)
                    .Include(t => t.Type1)
                    .Include(t2 => t2.Type2)
                    .OrderBy(f => f.Id)
                    .ToListAsync();
                    
                if (generation != null) {
                    forms = forms.Where(m => m.generation == generation).ToList();
                }
                    
                if ((type1Value != null && type2Value == null) || (type1Value != null && type2Value == null)) {

                    if (type1Value == null){
                        type1Value = type2Value;
                    }

                    var type = await _context.PkmnType
                            .Where(m => m.Name == type1Value)
                            .FirstOrDefaultAsync(); 

                    forms = forms.Where(m => m.Type1 == type ||
                                            m.Type2 == type ).ToList();

                } else if (type1Value != null && type2Value != null) {
                
                     var type1 = await _context.PkmnType
                                        .Where(m => m.Name == type1Value)
                                        .FirstOrDefaultAsync(); 
                
                     var type2 = await _context.PkmnType
                                        .Where(m => m.Name == type2Value)
                                        .FirstOrDefaultAsync(); 

                    forms = forms.Where(m => (m.Type1 == type1 &&
                                            m.Type2 == type2) ||
                                            (m.Type1 == type2 &&
                                            m.Type2 == type1)).ToList();
                }

                if (ability != null) {
                    forms = forms.Where(m => new[] {m.ability1, m.ability0, 
                                            m.hiddenAbility, m.specialAbility }
                                            .Any(a => string.Equals(a, ability, StringComparison.OrdinalIgnoreCase))).ToList();
                }

                if ((eggGroup1 != null && eggGroup2 == null) || (eggGroup1 != null && eggGroup2 == null)) {

                    if (eggGroup1 == null){
                        eggGroup1 = eggGroup2;
                    }

                    forms = forms.Where(m => string.Equals(m.eggGroup1, eggGroup1, StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(m.eggGroup2, eggGroup1, StringComparison.OrdinalIgnoreCase)).ToList();
                } else if (eggGroup1 != null && eggGroup1 != null) {

                    forms = forms.Where( m => (string.Equals(m.eggGroup1, eggGroup1, StringComparison.OrdinalIgnoreCase) &&
                                                  string.Equals(m.eggGroup2, eggGroup2, StringComparison.OrdinalIgnoreCase)) ||
                                                  (string.Equals(m.eggGroup1, eggGroup2, StringComparison.OrdinalIgnoreCase) &&
                                                  string.Equals(m.eggGroup2, eggGroup1, StringComparison.OrdinalIgnoreCase))
                                                  ).ToList();

                }

                if (includeAlternateForms == false){

                    forms = forms.Where( m => m.isBase == true).ToList();
                    
                }

                if (color != null) {
                    forms = forms.Where(m => string.Equals(
                            m.color, color, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                return View(forms);
                
                }
            }
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
