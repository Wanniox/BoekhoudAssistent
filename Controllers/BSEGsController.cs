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
    public class BSEGsController : Controller
    {
        private readonly BoekhoudAssistentContext _context;

        public BSEGsController(BoekhoudAssistentContext context)
        {
            _context = context;
        }

        // GET: BSEGs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BSEG.ToListAsync());
        }

        // GET: BSEGs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bSEG = await _context.BSEG
                .FirstOrDefaultAsync(m => m.BUKRS == id);
            if (bSEG == null)
            {
                return NotFound();
            }

            return View(bSEG);
        }

        // GET: BSEGs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BSEGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BUKRS,BELNR,GJAHR,BUZEI,BUZID,AUGDT,AUGCP,AUGBL,BSCHL")] BSEG bSEG)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bSEG);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bSEG);
        }

        // GET: BSEGs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bSEG = await _context.BSEG.FindAsync(id);
            if (bSEG == null)
            {
                return NotFound();
            }
            return View(bSEG);
        }

        // POST: BSEGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BUKRS,BELNR,GJAHR,BUZEI,BUZID,AUGDT,AUGCP,AUGBL,BSCHL")] BSEG bSEG)
        {
            if (id != bSEG.BUKRS)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bSEG);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BSEGExists(bSEG.BUKRS))
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
            return View(bSEG);
        }

        // GET: BSEGs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bSEG = await _context.BSEG
                .FirstOrDefaultAsync(m => m.BUKRS == id);
            if (bSEG == null)
            {
                return NotFound();
            }

            return View(bSEG);
        }

        // POST: BSEGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bSEG = await _context.BSEG.FindAsync(id);
            if (bSEG != null)
            {
                _context.BSEG.Remove(bSEG);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BSEGExists(int id)
        {
            return _context.BSEG.Any(e => e.BUKRS == id);
        }
    }
}
