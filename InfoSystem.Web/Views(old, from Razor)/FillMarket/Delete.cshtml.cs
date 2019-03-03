﻿using System.Threading.Tasks;
using InfoSystem.Core.Entities;
using InfoSystem.Infrastructure.DataBase.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InfoSystem.Web.Views.FillMarket
{
    public class DeleteModel : PageModel
    {
        private readonly InfoSystemDbContext _context;

        public DeleteModel(InfoSystemDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public MarketProduct MarketProduct { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MarketProduct = await _context.MarketProducts
                .Include(m => m.Market)
                .Include(m => m.Product).FirstOrDefaultAsync(m => m.Id == id);

            if (MarketProduct == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MarketProduct = await _context.MarketProducts.FindAsync(id);

            if (MarketProduct != null)
            {
                _context.MarketProducts.Remove(MarketProduct);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}