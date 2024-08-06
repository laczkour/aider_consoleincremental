using System;

// Set console window size
Console.SetWindowSize(80, 25);
Console.SetBufferSize(80, 25);

// Calculate center position
int centerX = Console.WindowWidth / 2;
int centerY = Console.WindowHeight / 2;

// Display title
string title = "Console Incremental";
Console.SetCursorPosition(centerX - title.Length / 2, centerY);
Console.WriteLine(title);

// Wait for user to press Enter
Console.SetCursorPosition(0, Console.WindowHeight - 1);
Console.WriteLine("Press Enter to quit...");
Console.ReadLine();
