﻿using SpaceParkModel;
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
                menu.MainMenu();
            }
            Environment.Exit(0);

            // ** Todo: everyone can get a list of ships, if you own a ship, put those ontop so they can easily access them **
            // ** Todo: check when you enter name {Luke (with a space)} fix that bug ** 
            // ** Todo: SwApi, check the validate name, could make it more exact and check for the exact name ** 

            // Todo: Add a history on the menu?
            // so if luke skywalker is logged on then only show his history

            // Todo: if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Use joins for "complex" lookups instead of finding ids for each step. See: Menu.ShowParkingMenu(). Maybe try putting that query in DBQuery, too.
            // TODO: ==> if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit
            // Todo: Testing
            // Todo: Seeding?
            // Todo: check the instructions if the database stuff needs to be async too 
            // Todo: (should be waaaay later) make sure you can't park twice, right now we have the menu's doing it for us... but probably better to have a method 
        }
    }
}