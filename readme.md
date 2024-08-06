This project is a console application
It will seperate DTOs and Logic classes
A class either contains data, or works with data objects

This is an incremental game with a Console theme, where the player shall get a lot of "Characters"

The Console is 80x25 in dimensions
There will be "buildings" on the screen all the time, telling the name, the number of buildings of the type, and a progress bar of the next "harvest"
"[]" marks the currently selected building by the user
When a "harvest" happens on the right the harvested number of characters shall be visible for a brief amount of time

for example:
  Console.WriteLine     8x    |########-------------|
[ Json.Parse            2x    |################-----| ]
  ex.PrintStackTrace    1x    |########-------------|
  Enumerable.Repeat     1x    |---------------------|      +8474 Characters


  Buy 1 Json.Parse            5000 Characters
  
  Characters: 21200

The project shall contain unit tests to stay correct after each modification
The unit tests should also test what are on the screen is correct

etc