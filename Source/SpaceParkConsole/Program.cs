using System;

namespace SpaceParkConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new();

            bool isValidName = menu.ShowNamePrompt();
            if (isValidName)
            {
                menu.MainMenu();
            }
            Environment.Exit(0);

            // Todo: Testing

            // Todo: if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: check the instructions if the database stuff needs to be async too
            // Todo: (should be waaaay later) make sure you can't park twice, right now we have the menu's doing it for us... but probably better to have a method 
        }
    }
}