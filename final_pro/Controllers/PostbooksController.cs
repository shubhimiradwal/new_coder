using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using final_pro.Models;

namespace final_pro.Controllers
{
    public class PostbooksController : Controller
    {
        private readonly AppDbContext _context;

        public PostbooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Postbooks
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Books.ToListAsync());
        //}

        // GET: Postbooks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postbook = await _context.Books
                .FirstOrDefaultAsync(m => m.title == id);
            if (postbook == null)
            {
                return NotFound();
            }

            return View(postbook);
        }
        public ActionResult Index(string searchBy, string search)
        {
            if (searchBy == "title")
            {
                return View(_context.Books.Where(x => x.title.StartsWith(search) || search == null).ToList());
            }
            else 
            {
                return View(_context.Books.Where(x => x.author.StartsWith(search)|| search == null).ToList());
            }
            
        }

        // GET: Postbooks/Create
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Search()
        {
            return View();
        }

        // POST: Postbooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("title,author,ISBN,number,year")] Postbook postbook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postbook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postbook);
        }

        // GET: Postbooks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postbook = await _context.Books.FindAsync(id);
            if (postbook == null)
            {
                return NotFound();
            }
            return View(postbook);
        }

        // POST: Postbooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("title,author,ISBN,number,year")] Postbook postbook)
        {
            if (id != postbook.title)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postbook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostbookExists(postbook.title))
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
            return View(postbook);
        }

        // GET: Postbooks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postbook = await _context.Books
                .FirstOrDefaultAsync(m => m.title == id);
            if (postbook == null)
            {
                return NotFound();
            }

            return View(postbook);
        }

        // POST: Postbooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var postbook = await _context.Books.FindAsync(id);
            _context.Books.Remove(postbook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostbookExists(string id)
        {
            return _context.Books.Any(e => e.title == id);
        }
    }
}
