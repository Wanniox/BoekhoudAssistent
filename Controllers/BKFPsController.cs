using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoekhoudAssistent.Data;
using BoekhoudAssistent.Models;

namespace BoekhoudAssistent.Controllers
{
    public class BKFPsController : Controller
    {
        private readonly BoekhoudAssistentContext _context;

        public BKFPsController(BoekhoudAssistentContext context)
        {
            _context = context;
        }

        // GET: BKFPs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BKFP.ToListAsync());
        }

        // GET: BKFPs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bKFP = await _context.BKFP
                .FirstOrDefaultAsync(m => m.BUKRS == id);
            if (bKFP == null)
            {
                return NotFound();
            }

            return View(bKFP);
        }

        // GET: BKFPs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BKFPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BUKRS,BELNR,GJAHR,BLART,BLDAT,BUDAT,MONAT,CPUDT,CPUTM")] BKFP bKFP)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bKFP);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bKFP);
        }

        // GET: BKFPs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bKFP = await _context.BKFP.FindAsync(id);
            if (bKFP == null)
            {
                return NotFound();
            }
            return View(bKFP);
        }

        // POST: BKFPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BUKRS,BELNR,GJAHR,BLART,BLDAT,BUDAT,MONAT,CPUDT,CPUTM")] BKFP bKFP)
        {
            if (id != bKFP.BUKRS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bKFP);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BKFPExists(bKFP.BUKRS))
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
            return View(bKFP);
        }

        // GET: BKFPs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bKFP = await _context.BKFP
                .FirstOrDefaultAsync(m => m.BUKRS == id);
            if (bKFP == null)
            {
                return NotFound();
            }

            return View(bKFP);
        }

        // POST: BKFPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bKFP = await _context.BKFP.FindAsync(id);
            if (bKFP != null)
            {
                _context.BKFP.Remove(bKFP);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BKFPExists(int id)
        {
            return _context.BKFP.Any(e => e.BUKRS == id);
        }
    }
}
