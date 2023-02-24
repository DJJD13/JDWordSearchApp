using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using JDWordSearchApp.Data;
using JDWordSearchApp.Models;
using System.Drawing;


namespace JDWordSearchApp.Pages.WordSearches
{
    public class IndexModel : PageModel
    {
        private readonly JDWordSearchApp.Data.JDWordSearchAppContext _context;

        public IndexModel(JDWordSearchApp.Data.JDWordSearchAppContext context)
        {
            _context = context;
        }

        public IList<WordSearch> WordSearch { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.WordSearch != null)
            {
                WordSearch = await _context.WordSearch.ToListAsync();
            }
        }

        public char[,] GenerateWordSearch(int dims, string[] words)
        {
            List<string> wordsList = words.ToList();
            var wordSearchArray = new char[dims, dims];
            // Array of characters to choose from randomly to fill empty space
            var letters = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            var rows = new int[dims];
            var cols = new int[dims];

            for (int i = 0; i < dims; i++)
            {
                rows[i] = i;
                cols[i] = i;
            }

            Random rnd = new Random();
            foreach (var word in wordsList)
            {
                var inserted = false;
                // Turn the word into an array of characters for insertion
                while (inserted == false)
                {
                    // We need to generate random numbers to determine if the word will be 
                    // horizontal or vertical, and either backwards or forwards
                    int rndRow = rnd.Next(dims);
                    int rndCol = rnd.Next(dims);
                    int horOrVer = rnd.Next(100);
                    
                    var len = word.Length;
                    var count = 0;


                    // If < 50, the word will be horizontal, else it will be vertical
                    if (horOrVer < 50)
                    {
                        var space = dims - rndCol;
                        if(space < word.Length)
                        {
                            continue;
                        }
                        for (int j = 0; j < word.Length; j++)
                        {
                            if (j < word.Length)
                            {
                                if (wordSearchArray[rndRow, rndCol + j] == word[j] || wordSearchArray[rndRow, rndCol + j] == '\0')
                                {
                                    wordSearchArray[rndRow, rndCol + j] = word[j];
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                        if(count == len)
                        {
                            inserted = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        var space = dims - rndCol;
                        if (space < word.Length)
                        {
                            continue;
                        }
                        for (int j = 0; j < word.Length; j++)
                        {
                            if (j < word.Length)
                            {
                                if (wordSearchArray[j + rndRow, rndCol] == word[j] || wordSearchArray[j + rndRow, rndCol] == '\0')
                                {
                                    wordSearchArray[j + rndRow, rndCol] = word[j];
                                    count++;
                                }
                            }
                        }
                        if (count == len)
                        {
                            inserted = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }

            for (int i = 0; i < dims; i++)
            {
                for(int j = 0; j < dims; j++)
                {
                    if (wordSearchArray[i, j] == '\0')
                    {
                        wordSearchArray[i, j] = letters[rnd.Next(26)];
                    }
                }
            }

            return wordSearchArray;
        }


        public IActionResult OnGetPartial(int dims, string[] words)
        {
            var testWord = GenerateWordSearch(dims, words);

            return new PartialViewResult
            {
                ViewName = "_PartialWordSearch",
                ViewData = new ViewDataDictionary<char[,]>(ViewData, testWord)
            };
        }
    }
}
