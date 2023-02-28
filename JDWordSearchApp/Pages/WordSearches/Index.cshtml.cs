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
        public void OnGet()
        {
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

                    // If < 50, the word will be horizontal, else it will be vertical
                    if (horOrVer < 50)
                    {
                        var space = dims - rndCol;
                        if(space < word.Length)
                        {
                            continue;
                        }
                        if(!CheckWordFit(word, wordSearchArray, rndRow, rndCol, 1))
                        {
                            continue;
                        }
                        for (int j = 0; j < word.Length; j++)
                        {
                            wordSearchArray[rndRow, rndCol + j] = word[j];
                        }
                    }
                    else
                    {
                        var space = dims - rndRow;
                        if (space < word.Length)
                        {
                            continue;
                        }
                        if (!CheckWordFit(word, wordSearchArray, rndRow, rndCol, 0))
                        {
                            continue;
                        }
                        for (int j = 0; j < word.Length; j++)
                        {
                            wordSearchArray[j + rndRow, rndCol] = word[j];
                        }
                    }
                    inserted = true;
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

        private bool CheckWordFit(string word, char[,] wordArr, int row, int col, int dir)
        {
            if(dir == 1)
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (j < word.Length)
                    {
                        if (wordArr[row, col + j] == word[j] || wordArr[row, col + j] == '\0')
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < word.Length; j++)
                {
                    if (j < word.Length)
                    {
                        if (wordArr[j + row, col] == word[j] || wordArr[j + row, col] == '\0')
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    
                }
            }
            
            return true;
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
