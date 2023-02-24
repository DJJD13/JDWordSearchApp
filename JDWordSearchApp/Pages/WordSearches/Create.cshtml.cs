using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JDWordSearchApp.Data;
using JDWordSearchApp.Models;

namespace JDWordSearchApp.Pages.WordSearches
{
    public class CreateModel : PageModel
    {
        private readonly JDWordSearchApp.Data.JDWordSearchAppContext _context;

        public CreateModel(JDWordSearchApp.Data.JDWordSearchAppContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public WordSearch WordSearch { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.WordSearch == null || WordSearch == null)
            {
                return Page();
            }

            _context.WordSearch.Add(WordSearch);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
