using SpaceParkModel;
using SpaceParkModel.Data;
using SpaceParkModel.Database;
using SpaceParkModel.SwApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceParkConsole
{
    class Menu
    {
        private readonly SwApi swApi;
        public string ActivePerson { get; set; }

        public Menu()
        {
            swApi = new SwApi();
        }

        public static int Show(string prompt, string[] options)
        {
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty array of options.");
            }

            Console.WriteLine(prompt);

            int selected = 0;

            // Hide the cursor that will blink after calling ReadKey.
            Console.CursorVisible = false;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                // If this is not the first iteration, move the cursor to the first line of the menu.
                if (key != null)
                {
                    Console.CursorLeft = 0;
                    Console.CursorTop = Console.CursorTop - options.Length;
                }

                // Print all the options, highlighting the selected one.
                for (int i = 0; i < options.Length; i++)
                {
                    var option = options[i];
                    if (i == selected)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("- " + option);
                    Console.ResetColor();
                }

                // Read another key and adjust the selected value before looping to repeat all of this.
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Length - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }
            }

            // Reset the cursor and return the selected option.
            Console.CursorVisible = true;
            return selected;
        }

        public bool ShowNamePrompt()
        {
            Console.WriteLine("Please enter your name: ");
            string nameAnswer = Console.ReadLine();
            bool isSwChar = swApi.ValidateSwName(nameAnswer);
            if (isSwChar)
            {
                ActivePerson = nameAnswer;
                return true;
            }

            string[] options = { "Re-enter name ", "Quit" };
            int selection = Show("Sorry, looks like you entered an invalid name... what would you like to do?", options);
            if (selection == 0)
            {
                ShowNamePrompt();
            }

            return false;
        }

        public void MainMenu()
        {
            // TODO: changed quit to logout if logged in.
            // TODO: if you are in the spaceship options -> add a Quit option
            string[] options = { "Park", "Quit" };
            bool isCheckedIn = DBQuery.IsCheckedIn(ActivePerson);
            if (isCheckedIn)
            {
                options = new string[] { "Pay and leave", "Quit" };
            }

            int selection = Show("Menu:", options);

            if (selection == options.Length - 1)
            {
                Environment.Exit(0);
            }
            else if (isCheckedIn && selection == 0)
            {
                // sends you to pay, then to print receipt etc..
                ShowPayAndLeave();
                Console.Clear();
                ShowNamePrompt();
            }
            else if (!isCheckedIn && selection == 0)
            {
                ShowParkingMenu();
            }
            MainMenu();
        }

        private void ShowParkingMenu()
        {
            // TODO: ==> if parking spots are full => check if bigger spot is open and offer it (yes/no), if not then quit

            List<SwStarship> starships = swApi.GetPersonStarships(ActivePerson);

            if (starships.Count == 0)
            {
                Console.WriteLine("Hitchhikers get expelled into space, goodbye!");
                Console.ReadKey();
                Environment.Exit(0);
            }

            // Getting the Names for the menu
            string[] starshipOptions = starships.Select(starship => starship.Name).ToArray();
            int selection = Show("Please select your spaceship.", starshipOptions);
            SwStarship starship = starships[selection];

            double starshipLength = starship.Length;

            // get all parking spot sizes
            List<ParkingSize> parkingSizes = DBQuery.GetParkingSizes();

            // find appropriate parking spot size (smallest size the ship will fit in)
            ParkingSize appropriateParkingSize = parkingSizes.Where(p => p.Size > starshipLength).OrderBy(p => p.Size).First();

            // check if there is a free spot for that size in the database and get the id
            int availableParkingSpotID = DBQuery.GetAvailableParking(appropriateParkingSize.ID);

            // TODO: test this at some point...
            // create occupancy for person/starship
            if (availableParkingSpotID == 0)
            {
                Console.WriteLine("Sorry... No parking spot available");
                Console.ReadKey();
                Environment.Exit(0);
            }
            DBQuery.FillOccupancy(ActivePerson, starship.Name, availableParkingSpotID);
        }

        private void ShowPayAndLeave()
        {
            Occupancy occupancy = DBQuery.GetOpenOccupancyByName(ActivePerson);
            int parkingSpotID = occupancy.ParkingSpotID;
            int parkingSizeID = DBQuery.GetParkingSizeIDBySpot(parkingSpotID);
            decimal parkingSpotPrice = DBQuery.GetParkingSpotPriceByID(parkingSizeID);

            TimeSpan parkingDuration = DateTime.Now - occupancy.ArrivalTime;
            decimal billingHours = Convert.ToDecimal(Math.Ceiling(parkingDuration.TotalHours));

            decimal totalPrice = billingHours * parkingSpotPrice;

            occupancy.DepartureTime = DateTime.Now;
            DBQuery.UpdateOccupancy(occupancy);

            ShowInvoiceAndLogout(totalPrice, billingHours);
        }

        private void ShowInvoiceAndLogout(decimal totalPrice, decimal billingHours)
        {
            Console.WriteLine($"Thank you for choosing us {ActivePerson}. You paid {totalPrice} for {billingHours} hours.");
            ActivePerson = "";
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
