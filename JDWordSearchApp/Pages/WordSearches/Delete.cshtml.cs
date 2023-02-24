using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JDWordSearchApp.Data;
using JDWordSearchApp.Models;

namespace JDWordSearchApp.Pages.WordSearches
{
    public class DeleteModel : PageModel
    {
        private readonly JDWordSearchApp.Data.JDWordSearchAppContext _context;

        public DeleteModel(JDWordSearchApp.Data.JDWordSearchAppContext context)
        {
            _context = context;
        }

        [BindProperty]
      public WordSearch WordSearch { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.WordSearch == null)
            {
                return NotFound();
            }

            var wordsearch = await _context.WordSearch.FirstOrDefaultAsync(m => m.Id == id);

            if (wordsearch == null)
            {
                return NotFound();
            }
            else 
            {
                WordSearch = wordsearch;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.WordSearch == null)
            {
                return NotFound();
            }
            var wordsearch = await _context.WordSearch.FindAsync(id);

            if (wordsearch != null)
            {
                WordSearch = wordsearch;
                _context.WordSearch.Remove(WordSearch);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
