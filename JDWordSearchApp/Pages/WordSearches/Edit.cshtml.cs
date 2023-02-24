using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JDWordSearchApp.Data;
using JDWordSearchApp.Models;

namespace JDWordSearchApp.Pages.WordSearches
{
    public class EditModel : PageModel
    {
        private readonly JDWordSearchApp.Data.JDWordSearchAppContext _context;

        public EditModel(JDWordSearchApp.Data.JDWordSearchAppContext context)
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

            var wordsearch =  await _context.WordSearch.FirstOrDefaultAsync(m => m.Id == id);
            if (wordsearch == null)
            {
                return NotFound();
            }
            WordSearch = wordsearch;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(WordSearch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordSearchExists(WordSearch.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool WordSearchExists(int id)
        {
          return (_context.WordSearch?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
