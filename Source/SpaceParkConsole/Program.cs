using SpaceParkModel;
using SpaceParkModel.Data;
using SpaceParkModel.SwApi;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
                Console.WriteLine($"Welcome to SpacePark {menu.ActivePerson}, what would you like to do?");
                menu.MainMenu();
            }
            Environment.Exit(0);


            // Todo: check when you enter name {Luke (with a space)} fix that bug
            // Todo: check the instructions if the database stuff needs to be async too 
            // Todo: if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Seeding?
            // Todo: Add a history on the menu?
            // Todo: Testing
            // Todo: SwApi, check the validate name, could make it more exact and check for the exact name
            // Todo: (should be waaaay later) make sure you can't park twice, right now we have the menu's doing it for us... but probably better to have a method 
        }
    }
}
