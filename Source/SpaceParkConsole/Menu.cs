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
            Console.Clear();
            if (options == null || options.Length == 0)
            {
                throw new ArgumentException("Cannot show a menu for an empty array of options.");
            }
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                Console.WriteLine(prompt);
            }

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

        public void MainMenu()
        {
            Console.Clear();
            // TODO: changed quit to logout if logged in.
            // TODO: if you are in the spaceship options -> add a Quit option
            string[] options = { "Park", "Show History", "Log out", "Quit" };
            bool isCheckedIn = DBQuery.IsCheckedIn(ActivePerson);
            if (isCheckedIn)
            {
                options = new string[] { "Pay and leave", "Show History", "Log out", "Quit" };
            }

            int selection = Show($"Welcome to SpacePark {ActivePerson}, what would you like to do?", options);

            if (selection == options.Length - 1)
            {
                Environment.Exit(0);
            }
            else if (isCheckedIn && selection == 0)
            {
                ShowPayAndLeave();
                LogOut();
            }
            else if (!isCheckedIn && selection == 0)
            {
                ShowParkingMenu();
            }
            else if (selection == 1)
            {
                ShowPersonalHistory(ActivePerson);
            }
            else if (selection == options.Length - 2)
            {
                LogOut();
            }
            MainMenu();
        }

        public bool ShowNamePrompt()
        {
            Console.Clear();
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

            if (selection == options.Length - 1)
            {
                Environment.Exit(0);
            }
            else if (selection == 0)
            {
                return ShowNamePrompt();
            }

            return false;
        }

        private void ShowParkingMenu()
        {
            Console.Clear();
            List<SwStarship> starships = swApi.GetAllResources<SwStarship>(SwApiResource.starships).Result;
            List<SwStarship> personalStarships = swApi.GetPersonStarships(ActivePerson);
            
            foreach (var ship in personalStarships)
            {
                int shipIndex = starships.FindIndex(s => s.Name == ship.Name);
                starships.RemoveAt(shipIndex);
                starships.Insert(0, ship);
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
            Console.Clear();
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
            Console.Clear();
            Console.WriteLine($"Thank you for choosing us {ActivePerson}. You paid {totalPrice} for {billingHours} hours.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            LogOut();
        }

        private void ShowPersonalHistory(string name)
        {
            Console.Clear();
            List<OccupancyHistory> history = DBQuery.GetPersonalHistory(name);

            Console.WriteLine("Spaceship                           Date          Duration (in hours)");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach(var data in history)
            {
                string spaceshipName = ResizeTextToLength(data.SpaceshipName, 32);

                string date = data.ArrivalTime.ToString("yyyy-MM-dd");
                date = ResizeTextToLength(date, 10);

                double duration = (data.DepartureTime - data.ArrivalTime).TotalHours;
                duration = Math.Round(duration, 1, MidpointRounding.ToPositiveInfinity);
                string durationText = duration.ToString();
                durationText = ResizeTextToLength(durationText, 4);

                Console.WriteLine($"{spaceshipName}    {date}    {durationText}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static string ResizeTextToLength(string text, int length)
        {
            string result = "";
            if (text.Length == length)
            {
                result = text;
            }
            else if (text.Length > length)
            {
                result = text.Substring(0, length - 3) + "...";
            }
            else if (text.Length < length)
            {
                result = text.PadRight(length);
            }
            return result;
        }

        private void LogOut()
        {
            ActivePerson = "";
            Console.Clear();
            ShowNamePrompt();
        }
    }
}
