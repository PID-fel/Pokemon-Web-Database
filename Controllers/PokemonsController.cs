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

                var segments = id.Split('|');


                if (segments.Count() == 0){

                    return NotFound();

                } else {

                    if (segments[0] == "generation"){
                        var pokemonByGeneration = await _context.Pokemon
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type1)
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type2)
                            .Where(m => m.Forms.FirstOrDefault().generation == Int32.Parse(segments[1]))
                            .ToListAsync();
                    return View(pokemonByGeneration);
        
                    } else if (segments[0]=="type"){

                        var type = await _context.PkmnType
                            .Where(m => m.Name == segments[1])
                            .FirstOrDefaultAsync();           
            
                        var pokemonsbyType = await _context.Pokemon
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type1)
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type2)
                            .Where(m => 
                            (m.Forms.FirstOrDefault().Type1.Id == type.Id || 
                             m.Forms.FirstOrDefault().Type2.Id == type.Id))
                            .ToListAsync();
                        return View(pokemonsbyType);
                             
                    } else if (segments[0]=="ability"){        
            
                        var pokemonsbyType = await _context.Pokemon
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type1)
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type2)
                            .Where(m => 
                            (m.Forms.FirstOrDefault().ability1 == segments[1] || 
                             m.Forms.FirstOrDefault().ability0 == segments[1] ||
                             m.Forms.FirstOrDefault().hiddenAbility == segments[1] || 
                             m.Forms.FirstOrDefault().specialAbility == segments[1]))
                            .ToListAsync();
                        return View(pokemonsbyType);

                    } else if (segments[0] == "eggGroup"){

                        var pokemonByEggGroup = await _context.Pokemon
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type1)
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type2)
                            .Where(m =>
                            m.Forms.FirstOrDefault().eggGroup1 == segments[1] ||
                            m.Forms.FirstOrDefault().eggGroup2 == (segments[1]))
                            .ToListAsync();
                        return View(pokemonByEggGroup);

                    }  else if (segments[0] == "color"){

                        var pokemonByColor = await _context.Pokemon
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type1)
                            .Include(f => f.Forms)
                            .ThenInclude(t => t.Type2)
                            .Where(m =>
                            m.Forms.FirstOrDefault().color == segments[1])
                            .ToListAsync();
                        return View(pokemonByColor);
                    }   
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
