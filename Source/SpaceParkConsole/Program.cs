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

            // ** Todo: SHOW Parkingmenu() refacture data into dbquery (use join) **
            
            // ** Todo: Seeding? **

            // Todo: Fix namespaces
            // Todo: if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Use joins for "complex" lookups instead of finding ids for each step. See: Menu.ShowParkingMenu(). Maybe try putting that query in DBQuery, too.
            // TODO: ==> if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Testing
            // Todo: check the instructions if the database stuff needs to be async too 
            // Todo: (should be waaaay later) make sure you can't park twice, right now we have the menu's doing it for us... but probably better to have a method 
        }
    }
}