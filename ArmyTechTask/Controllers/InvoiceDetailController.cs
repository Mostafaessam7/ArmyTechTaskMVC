using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArmyTechTask.Models;

namespace ArmyTechTask.Controllers
{
    public class InvoiceDetailController : Controller
    {
        private readonly ArmyTechTaskContext _context;

        public InvoiceDetailController(ArmyTechTaskContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetail
        public async Task<IActionResult> Index()
        {
            var armyTechTaskContext = _context.InvoiceDetails.Include(i => i.InvoiceHeader);
            return View(await armyTechTaskContext.ToListAsync());
        }

        // GET: InvoiceDetail/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.InvoiceHeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // GET: InvoiceDetail/Create
        public IActionResult Create()
        {
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "Id");
            return View();
        }

        // POST: InvoiceDetail/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceHeaderId,ItemName,ItemCount,ItemPrice")] InvoiceDetail invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "Id", invoiceDetail.InvoiceHeaderId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetail/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "Id", invoiceDetail.InvoiceHeaderId);
            return View(invoiceDetail);
        }

        // POST: InvoiceDetail/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,InvoiceHeaderId,ItemName,ItemCount,ItemPrice")] InvoiceDetail invoiceDetail)
        {
            if (id != invoiceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoiceDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailExists(invoiceDetail.Id))
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
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "Id", invoiceDetail.InvoiceHeaderId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetail/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.InvoiceHeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // POST: InvoiceDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'ArmyTechTaskContext.InvoiceDetails'  is null.");
            }
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(long id)
        {
          return (_context.InvoiceDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
