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
    public class MessageExtandedsController : Controller
    {
        private readonly noam2Context _context;

        public MessageExtandedsController(noam2Context context)
        {
            _context = context;
        }

        // GET: MessageExtandeds
        public async Task<IActionResult> Index()
        {
              return _context.MessageExtanded != null ? 
                          View(await _context.MessageExtanded.ToListAsync()) :
                          Problem("Entity set 'noam2Context.MessageExtanded'  is null.");
        }

        // GET: MessageExtandeds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MessageExtanded == null)
            {
                return NotFound();
            }

            var messageExtanded = await _context.MessageExtanded
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageExtanded == null)
            {
                return NotFound();
            }

            return View(messageExtanded);
        }

        // GET: MessageExtandeds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageExtandeds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Created,Sent,User1,User2")] MessageExtanded messageExtanded)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageExtanded);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageExtanded);
        }

        // GET: MessageExtandeds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MessageExtanded == null)
            {
                return NotFound();
            }

            var messageExtanded = await _context.MessageExtanded.FindAsync(id);
            if (messageExtanded == null)
            {
                return NotFound();
            }
            return View(messageExtanded);
        }

        // POST: MessageExtandeds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Created,Sent,User1,User2")] MessageExtanded messageExtanded)
        {
            if (id != messageExtanded.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageExtanded);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExtandedExists(messageExtanded.Id))
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
            return View(messageExtanded);
        }

        // GET: MessageExtandeds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MessageExtanded == null)
            {
                return NotFound();
            }

            var messageExtanded = await _context.MessageExtanded
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageExtanded == null)
            {
                return NotFound();
            }

            return View(messageExtanded);
        }

        // POST: MessageExtandeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MessageExtanded == null)
            {
                return Problem("Entity set 'noam2Context.MessageExtanded'  is null.");
            }
            var messageExtanded = await _context.MessageExtanded.FindAsync(id);
            if (messageExtanded != null)
            {
                _context.MessageExtanded.Remove(messageExtanded);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExtandedExists(int id)
        {
          return (_context.MessageExtanded?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
