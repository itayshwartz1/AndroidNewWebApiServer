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
    public class UserExtendedsController : Controller
    {
        private readonly noam2Context _context;

        public UserExtendedsController(noam2Context context)
        {
            _context = context;
        }

        // GET: UserExtendeds
        public async Task<IActionResult> Index()
        {
              return _context.UserExtended != null ? 
                          View(await _context.UserExtended.ToListAsync()) :
                          Problem("Entity set 'noam2Context.UserExtended'  is null.");
        }

        // GET: UserExtendeds/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.UserExtended == null)
            {
                return NotFound();
            }

            var userExtended = await _context.UserExtended
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExtended == null)
            {
                return NotFound();
            }

            return View(userExtended);
        }

        // GET: UserExtendeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserExtendeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Password,Server")] UserExtended userExtended)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userExtended);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userExtended);
        }

        // GET: UserExtendeds/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.UserExtended == null)
            {
                return NotFound();
            }

            var userExtended = await _context.UserExtended.FindAsync(id);
            if (userExtended == null)
            {
                return NotFound();
            }
            return View(userExtended);
        }

        // POST: UserExtendeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Password,Server")] UserExtended userExtended)
        {
            if (id != userExtended.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userExtended);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExtendedExists(userExtended.Id))
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
            return View(userExtended);
        }

        // GET: UserExtendeds/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.UserExtended == null)
            {
                return NotFound();
            }

            var userExtended = await _context.UserExtended
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userExtended == null)
            {
                return NotFound();
            }

            return View(userExtended);
        }

        // POST: UserExtendeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.UserExtended == null)
            {
                return Problem("Entity set 'noam2Context.UserExtended'  is null.");
            }
            var userExtended = await _context.UserExtended.FindAsync(id);
            if (userExtended != null)
            {
                _context.UserExtended.Remove(userExtended);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExtendedExists(string id)
        {
          return (_context.UserExtended?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
