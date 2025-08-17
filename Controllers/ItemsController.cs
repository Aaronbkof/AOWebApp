using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AOWebApp.Data;
using AOWebApp.Models;
using AOWebApp.ViewModels;

namespace AOWebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly AmazonDbContext _context;

        public ItemsController(AmazonDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(ItemSearchViewModel vm)
        {
            #region CategoryQuery
            var Categories = _context.ItemCategories
                .Where(ic => !ic.ParentCategoryId.HasValue)
                // OR .Where(ic => ic.ParentCategory == null)
                .OrderBy(ic => ic.CategoryName)
                .Select(ic => new { ic.CategoryId, ic.CategoryName })
                .ToList();

            vm.CategoryList = new SelectList(Categories,
                                           nameof(ItemCategory.CategoryId),
                                           nameof(ItemCategory.CategoryName),
                                           vm.CategoryId);
            #endregion

            #region ItemQuery
            var amazonOrdersContext = _context.Items.Include(i => i.Category).OrderBy(i => i.ItemName).AsQueryable();

            if (!string.IsNullOrWhiteSpace(vm.SearchText))
            {
                amazonOrdersContext = amazonOrdersContext.Where(i => i.ItemName.Contains(vm.SearchText));
            }

            if (vm.CategoryId != null)
            {
                amazonOrdersContext = amazonOrdersContext.Where(i => i.Category.ParentCategoryId == vm.CategoryId);
            }

            vm.ItemList = await amazonOrdersContext
                .Select(i => new Models.ViewModels.Item_ItemDetail
                {
                    TheItem = i,
                    ReviewCount = (i.Reviews != null ? i.Reviews.Count : 0),
                    AverageRating = (i.Reviews != null && i.Reviews.Count() > 0 ? i.Reviews.Select(r => r.Rating).Average() : 0)  // Added missing closing parenthesis
                }).ToListAsync();

            #endregion

            return View(vm);
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemDescription,ItemCost,ItemImage,CategoryId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
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
            ViewData["CategoryId"] = new SelectList(_context.ItemCategories, "CategoryId", "CategoryId", item.CategoryId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
