# JDWordSearchApp
Word Search App

# Idea
For this, I wanted to keep it (relatively) simple by leaving most of the fucntionality either on the client side or in a method behind the scenes.
When making this, I realized I was a bit out of practice, especially with AJAX and Partial Views. Those took a little while to get working, but I was 
able to figure it out and get the partial view to successfully update on a button request.

The 'grid' itself is pretty simple. I check each word, assign a random starting row and column based on the dimensions given, then try to insert that word into
the word search. If it fails, it will attempt to place the word elsewhere with a new row and column. If successful, the word is printed on a 2D character array,
and the next word is inserted. After all the words are inserted, the rest of the null parts are inserted with random letters. Once finished, the array is passed 
to the Partial View and iterated across to be printed there. Regenerating calls the function again with simply the same data as original.
