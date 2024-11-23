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
    public class PokemonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PokemonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pokemons
        public async Task<IActionResult> Index(string? id)
        {
            if (id == null){

                var allPokemon = await _context.Pokemon
                    .Include(f => f.Forms)
                    .ThenInclude(t => t.Type1)
                    .Include(f => f.Forms)
                    .ThenInclude(t => t.Type2)
                    .ToListAsync();
                    
                    return View(allPokemon);

            } else {

                int? generation = null;
                string? type1Value = null;
                string? type2Value = null;
                string? ability = null;
                string? eggGroup1 = null;
                string? eggGroup2 = null;
                string? color = null;

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
                    }
                }
                 
                var pokemon = await _context.Pokemon
                    .Include(f => f.Forms)
                    .ThenInclude(t => t.Type1)
                    .Include(f => f.Forms)
                    .ThenInclude(t => t.Type2)
                    .ToListAsync();
                    
                if (generation != null) {
                    pokemon = pokemon.Where(m => m.Forms.FirstOrDefault().generation == generation).ToList();
                }
                    
                if ((type1Value != null && type2Value == null) || (type1Value != null && type2Value == null)) {

                    if (type1Value == null){
                        type1Value = type2Value;
                    }

                    var type = await _context.PkmnType
                            .Where(m => m.Name == type1Value)
                            .FirstOrDefaultAsync(); 

                    pokemon = pokemon.Where(m => m.Forms.FirstOrDefault().Type1 == type ||
                                            m.Forms.FirstOrDefault().Type2 == type ).ToList();

                } else if (type1Value != null && type2Value != null) {
                
                     var type1 = await _context.PkmnType
                                        .Where(m => m.Name == type1Value)
                                        .FirstOrDefaultAsync(); 
                
                     var type2 = await _context.PkmnType
                                        .Where(m => m.Name == type2Value)
                                        .FirstOrDefaultAsync(); 

                    pokemon = pokemon.Where(m => (m.Forms.FirstOrDefault().Type1 == type1 &&
                                            m.Forms.FirstOrDefault().Type2 == type2) ||
                                            (m.Forms.FirstOrDefault().Type1 == type2 &&
                                            m.Forms.FirstOrDefault().Type2 == type1)).ToList();
                }

                if (ability != null) {
                    pokemon = pokemon.Where(m => new[] {m.Forms.FirstOrDefault()?.ability1, m.Forms.FirstOrDefault()?.ability0, 
                                            m.Forms.FirstOrDefault()?.hiddenAbility, m.Forms.FirstOrDefault()?.specialAbility }
                                            .Any(a => string.Equals(a, ability, StringComparison.OrdinalIgnoreCase))).ToList();
                }

                if ((eggGroup1 != null && eggGroup2 == null) || (eggGroup1 != null && eggGroup2 == null)) {

                    if (eggGroup1 == null){
                        eggGroup1 = eggGroup2;
                    }


                    pokemon = pokemon.Where(m => string.Equals(m.Forms.FirstOrDefault().eggGroup1, eggGroup1, StringComparison.OrdinalIgnoreCase) ||
                                            string.Equals(m.Forms.FirstOrDefault().eggGroup2, eggGroup1, StringComparison.OrdinalIgnoreCase)).ToList();
                } else if (eggGroup1 != null && eggGroup1 != null) {

                    pokemon = pokemon.Where( m => (string.Equals(m.Forms.FirstOrDefault().eggGroup1, eggGroup1, StringComparison.OrdinalIgnoreCase) &&
                                                  string.Equals(m.Forms.FirstOrDefault().eggGroup2, eggGroup2, StringComparison.OrdinalIgnoreCase)) ||
                                                  (string.Equals(m.Forms.FirstOrDefault().eggGroup1, eggGroup2, StringComparison.OrdinalIgnoreCase) &&
                                                  string.Equals(m.Forms.FirstOrDefault().eggGroup2, eggGroup1, StringComparison.OrdinalIgnoreCase))
                                                  ).ToList();

                }

                if (color != null) {
                    pokemon = pokemon.Where(m => string.Equals(
                            m.Forms.FirstOrDefault().color, color, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                return View(pokemon);
                }
            }


        return NotFound();


        }


        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .Include(f => f.Forms)
                .ThenInclude(t => t.Type1)
                .Include(f => f.Forms)
                .ThenInclude(t => t.Type2)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }


        // GET: Pokemons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tags")] Pokemon pokemon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tags")] Pokemon pokemon)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Id))
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
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemon
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemon.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemon.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Forms(int pokemonId, int formNumber)
        {
            if (formNumber == null)
            {
                formNumber = 0;
            }

            #pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var pokemon = await _context.Pokemon
                .Include(f => f.Forms)
                .ThenInclude(t => t.Type1)

                .Include(f => f.Forms)
                .ThenInclude(t => t.Type2)

                .Include(f => f.Forms)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.Pokemon)

                .Include(f => f.Forms)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.Pokemon)

                .Include(f => f.Forms)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.Pokemon)
                .ThenInclude(p => p.Forms)

                .Include(f => f.Forms)
                .ThenInclude(p => p.PrevoForm)
                .ThenInclude(p => p.Pokemon)
                .ThenInclude(f => f.Forms)

                .Include(f => f.Forms)
                .ThenInclude(p => p.PostEvoForms)
                .ThenInclude(p => p.PostEvoForms)
                .ThenInclude(p => p.Pokemon)

                .Include(f => f.Forms)
                .ThenInclude(e => e.PostEvoForms)
                .ThenInclude(p => p.Pokemon)

                .Where(m => m.Id == pokemonId)
                .FirstOrDefaultAsync();
            #pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.


            var previousPokemon = await _context.Pokemon
                .Where(m => m.Id == pokemonId-1)
                .Include(f => f.Forms)
                .FirstOrDefaultAsync();

            var subsequentPokemon = await _context.Pokemon
                .Where(m => m.Id == pokemonId + 1)
                .Include(f => f.Forms)
                .FirstOrDefaultAsync();

            var formId = pokemon.Forms.ElementAt(formNumber).Id;

            var form = await _context.Form
                .Where(m => m.Id == formId)
                .FirstOrDefaultAsync();

            if (pokemon == null || form == null)
            {
                return NotFound();
            }

            var formTest = pokemon.Forms.FirstOrDefault(f => f.Id == formId);

            if (formTest == null)
            {
                return NotFound();
            }

            return View((pokemon, form, previousPokemon, subsequentPokemon));
        }



        private bool PokemonExists(int id)
        {
            return _context.Pokemon.Any(e => e.Id == id);
        }
    }
}
