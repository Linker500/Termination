// See https://aka.ms/new-console-template for more information

/*
 * TODO
 * Console.CursorVisible = false; (kept on for now cuz debug)
 * Change titlebar text?
 * manually set console (desktop) window size?
 * catch Ctrl + C to close program and to proper cleanup, or disable at all maybe?
 */

/*
 *  Hierachy:
 *      Screen:     The Entire screen
 *          Window:     a section of a screen, a "window".
 *              Textblock:  Text optimized window
 *              Canvas:     art optimized window
 *          Token: A single character an color information.
 *
 */

using Termination;
Terminal term = new();
//Task.Factory.StartNew(term.Start, TaskCreationOptions.LongRunning);
term.Start();



// while(true)
// {
//     Console.WriteLine(Console.BufferWidth + " " + Console.BufferHeight);
// }