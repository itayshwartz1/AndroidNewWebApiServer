using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using noam2.Data;
using noam2.Model;

namespace noam2.Controllers
{
    public class ContactExtendedsController : Controller
    {
        private readonly noam2Context _context;

        public ContactExtendedsController(noam2Context context)
        {
            _context = context;
        }

        // GET: ContactExtendeds
        public async Task<IActionResult> Index()
        {
              return _context.ContactExtended != null ? 
                          View(await _context.ContactExtended.ToListAsync()) :
                          Problem("Entity set 'noam2Context.ContactExtended'  is null.");
        }

        // GET: ContactExtendeds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ContactExtended == null)
            {
                return NotFound();
            }

            var contactExtended = await _context.ContactExtended
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactExtended == null)
            {
                return NotFound();
            }

            return View(contactExtended);
        }

        // GET: ContactExtendeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactExtendeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MyUser,Name,Server,Last,Lastdate")] ContactExtended contactExtended)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactExtended);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactExtended);
        }

        // GET: ContactExtendeds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ContactExtended == null)
            {
                return NotFound();
            }

            var contactExtended = await _context.ContactExtended.FindAsync(id);
            if (contactExtended == null)
            {
                return NotFound();
            }
            return View(contactExtended);
        }

        // POST: ContactExtendeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,MyUser,Name,Server,Last,Lastdate")] ContactExtended contactExtended)
        {
            if (id != contactExtended.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactExtended);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExtendedExists(contactExtended.Id))
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
            return View(contactExtended);
        }

        // GET: ContactExtendeds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ContactExtended == null)
            {
                return NotFound();
            }

            var contactExtended = await _context.ContactExtended
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactExtended == null)
            {
                return NotFound();
            }

            return View(contactExtended);
        }

        // POST: ContactExtendeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ContactExtended == null)
            {
                return Problem("Entity set 'noam2Context.ContactExtended'  is null.");
            }
            var contactExtended = await _context.ContactExtended.FindAsync(id);
            if (contactExtended != null)
            {
                _context.ContactExtended.Remove(contactExtended);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExtendedExists(string id)
        {
          return (_context.ContactExtended?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
