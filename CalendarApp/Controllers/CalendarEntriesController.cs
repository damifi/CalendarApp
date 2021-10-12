using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CalendarApp.Data;
using CalendarApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace CalendarApp.Controllers
{
    public class CalendarEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalendarEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CalendarEntries
        public async Task<IActionResult> Index()
        {
            return View(await _context.CalendarEntry.ToListAsync());
        }

        // GET: CalendarEntries/OpenSearchForm
        public async Task<IActionResult> OpenSearchForm()
        {
            return View();
        }

        // POST: CalendarEntries/ShowSearchResult
        public async Task<IActionResult> ShowSearchResult(String SearchPhrase)
        {
            return View("Index", await _context.CalendarEntry.Where( i => i.Title.Contains(SearchPhrase)).ToListAsync());
        }


        // GET: CalendarEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEntry = await _context.CalendarEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendarEntry == null)
            {
                return NotFound();
            }

            return View(calendarEntry);
        }

        // GET: CalendarEntries/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CalendarEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] CalendarEntry calendarEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calendarEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(calendarEntry);
        }

        [Authorize]
        // GET: CalendarEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEntry = await _context.CalendarEntry.FindAsync(id);
            if (calendarEntry == null)
            {
                return NotFound();
            }
            return View(calendarEntry);
        }

        // POST: CalendarEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] CalendarEntry calendarEntry)
        {
            if (id != calendarEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calendarEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalendarEntryExists(calendarEntry.Id))
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
            return View(calendarEntry);
        }

        // GET: CalendarEntries/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calendarEntry = await _context.CalendarEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (calendarEntry == null)
            {
                return NotFound();
            }

            return View(calendarEntry);
        }

        // POST: CalendarEntries/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calendarEntry = await _context.CalendarEntry.FindAsync(id);
            _context.CalendarEntry.Remove(calendarEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalendarEntryExists(int id)
        {
            return _context.CalendarEntry.Any(e => e.Id == id);
        }
    }
}
