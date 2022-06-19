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
    public class MessageNewsController : Controller
    {
        private readonly noam2Context _context;

        public MessageNewsController(noam2Context context)
        {
            _context = context;
        }

        // GET: MessageNews
        public async Task<IActionResult> Index()
        {
              return _context.MessageNew != null ? 
                          View(await _context.MessageNew.ToListAsync()) :
                          Problem("Entity set 'noam2Context.MessageNew'  is null.");
        }

        // GET: MessageNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MessageNew == null)
            {
                return NotFound();
            }

            var messageNew = await _context.MessageNew
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageNew == null)
            {
                return NotFound();
            }

            return View(messageNew);
        }

        // GET: MessageNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessageNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Created,Sent,User1,User2")] MessageNew messageNew)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messageNew);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messageNew);
        }

        // GET: MessageNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MessageNew == null)
            {
                return NotFound();
            }

            var messageNew = await _context.MessageNew.FindAsync(id);
            if (messageNew == null)
            {
                return NotFound();
            }
            return View(messageNew);
        }

        // POST: MessageNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Created,Sent,User1,User2")] MessageNew messageNew)
        {
            if (id != messageNew.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messageNew);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageNewExists(messageNew.Id))
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
            return View(messageNew);
        }

        // GET: MessageNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MessageNew == null)
            {
                return NotFound();
            }

            var messageNew = await _context.MessageNew
                .FirstOrDefaultAsync(m => m.Id == id);
            if (messageNew == null)
            {
                return NotFound();
            }

            return View(messageNew);
        }

        // POST: MessageNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MessageNew == null)
            {
                return Problem("Entity set 'noam2Context.MessageNew'  is null.");
            }
            var messageNew = await _context.MessageNew.FindAsync(id);
            if (messageNew != null)
            {
                _context.MessageNew.Remove(messageNew);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageNewExists(int id)
        {
          return (_context.MessageNew?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
