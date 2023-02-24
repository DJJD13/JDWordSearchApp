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
    public class DetailsModel : PageModel
    {
        private readonly JDWordSearchApp.Data.JDWordSearchAppContext _context;

        public DetailsModel(JDWordSearchApp.Data.JDWordSearchAppContext context)
        {
            _context = context;
        }

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
    }
}
