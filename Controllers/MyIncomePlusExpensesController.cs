using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Models;

namespace MyExpenses.Controllers
{
    public class MyIncomePlusExpensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyIncomePlusExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MyIncomePlusExpenses
        public async Task<IActionResult> Index(int? month, int? year)
        {
            if (month == null)
            {
                month = DateTime.Now.Month;
            }
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            ViewData["month"] = month;
            ViewData["year"] = year;

            var applicationDbContext = _context.MyIncomePlusExpenses.Include(m => m.Category)
                                               .Where(i => i.Date.Month == month && i.Date.Year == year);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MyIncomePlusExpenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIncomePlusExpenses = await _context.MyIncomePlusExpenses
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (myIncomePlusExpenses == null)
            {
                return NotFound();
            }

            return View(myIncomePlusExpenses);
        }

        // GET: MyIncomePlusExpenses/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: MyIncomePlusExpenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,CategoryId,Date,Price")] MyIncomePlusExpenses myIncomePlusExpenses)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myIncomePlusExpenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", myIncomePlusExpenses.CategoryId);
            return View(myIncomePlusExpenses);
        }

        // GET: MyIncomePlusExpenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIncomePlusExpenses = await _context.MyIncomePlusExpenses.FindAsync(id);
            if (myIncomePlusExpenses == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", myIncomePlusExpenses.CategoryId);
            return View(myIncomePlusExpenses);
        }

        // POST: MyIncomePlusExpenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,CategoryId,Date,Price")] MyIncomePlusExpenses myIncomePlusExpenses)
        {
            if (id != myIncomePlusExpenses.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myIncomePlusExpenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyIncomePlusExpensesExists(myIncomePlusExpenses.id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", myIncomePlusExpenses.CategoryId);
            return View(myIncomePlusExpenses);
        }

        // GET: MyIncomePlusExpenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myIncomePlusExpenses = await _context.MyIncomePlusExpenses
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.id == id);
            if (myIncomePlusExpenses == null)
            {
                return NotFound();
            }

            return View(myIncomePlusExpenses);
        }

        // POST: MyIncomePlusExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myIncomePlusExpenses = await _context.MyIncomePlusExpenses.FindAsync(id);
            _context.MyIncomePlusExpenses.Remove(myIncomePlusExpenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyIncomePlusExpensesExists(int id)
        {
            return _context.MyIncomePlusExpenses.Any(e => e.id == id);
        }
    }
}
