using System;
using System.Threading.Tasks;

namespace SpaceParkConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Menu menu = new();

            bool isValidName = await menu.ShowNamePrompt();
            if (isValidName)
            {
                await menu.MainMenu();
            }
            Environment.Exit(0);

            // Todo: if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Testing
            
            // Todo: (should be waaaay later) make sure you can't park twice, right now we have the menu's doing it for us... but probably better to have a method 
        }
    }
}