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
        }
    }
}