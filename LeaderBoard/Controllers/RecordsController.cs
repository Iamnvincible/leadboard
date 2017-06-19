using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaderBoard.Data;
using LeaderBoard.Models;
using Microsoft.AspNetCore.Authorization;

namespace LeaderBoard.Controllers
{
   
    public class RecordsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecordsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Records
        public async Task<IActionResult> Index()
        {
            return View(await _context.Record.ToListAsync());
        }
        public async Task<IActionResult> Index(string stunum)
        {
            IQueryable<string> genreQuery = from m in _context.Record
                                            orderby m.
                                            select m.Genre;

            var movies = from m in _context.
                         select m;

            if (!String.IsNullOrEmpty(name))
            {
                movies = movies.Where(s => s.Title.Contains(name));
            }

           

            var movieGenreVM = new MovieGenreViewModel();
            movieGenreVM.genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            movieGenreVM.movies = await movies.ToListAsync();

            return View(movieGenreVM);
        }

        // GET: Records/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record
                .SingleOrDefaultAsync(m => m.ID == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }
        // GET: Records/Create
        [Authorize(Roles ="Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Records/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Create([Bind("ID,Number,Name,Sex,Score")] Record record)
        {
            if (ModelState.IsValid)
            {
                _context.Add(record);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(record);
        }

        // GET: Records/Edit/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record.SingleOrDefaultAsync(m => m.ID == id);
            if (record == null)
            {
                return NotFound();
            }
            return View(record);
        }

        // POST: Records/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("ID,Number,Name,Sex,Score")] Record record)
        {
            if (id != record.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(record);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecordExists(record.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(record);
        }

        // GET: Records/Delete/5
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var record = await _context.Record
                .SingleOrDefaultAsync(m => m.ID == id);
            if (record == null)
            {
                return NotFound();
            }

            return View(record);
        }

        // POST: Records/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var record = await _context.Record.SingleOrDefaultAsync(m => m.ID == id);
            _context.Record.Remove(record);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RecordExists(int id)
        {
            return _context.Record.Any(e => e.ID == id);
        }
    }
}
