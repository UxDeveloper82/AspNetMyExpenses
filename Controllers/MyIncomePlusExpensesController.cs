using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyExpenses.Data;
using MyExpenses.Models;
using MyExpenses.ViewModels;

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

        // GET: MyIncomePlusExpenses/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");

            if (id == null)
            {
                return View(new MyIncomePlusExpensesViewModel());
            }
           
            else {
                var MyExpensePlusIncome =await _context.MyIncomePlusExpenses.Include(m => m.Category).FirstOrDefaultAsync(m => m.id == id);
           
                return View(new MyIncomePlusExpensesViewModel
                {
                    id = MyExpensePlusIncome.id,
                    Category = MyExpensePlusIncome.Category,
                    Date = MyExpensePlusIncome.Date,
                    Price = MyExpensePlusIncome.Price,
                    CategoryId = MyExpensePlusIncome.CategoryId,

                });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MyIncomePlusExpensesViewModel vm)
        {
            var myexpenseplusincome = new MyIncomePlusExpenses
            {
                id = vm.id,
                Category = vm.Category,
                CategoryId = vm.CategoryId,
                Date = vm.Date,
                Price = vm.Price
            };

            if (myexpenseplusincome.id > 0)
            {
                _context.MyIncomePlusExpenses.Update(myexpenseplusincome);
            }
            else {
                _context.MyIncomePlusExpenses.Add(myexpenseplusincome);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

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
