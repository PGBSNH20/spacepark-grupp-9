using SpaceParkModel.SwApi;
using SpaceParkModel.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpaceParkConsole
{
    public class Menu
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
                    Console.CursorTop -= options.Length;
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

        public async Task MainMenu()
        {
            Console.Clear();
            string[] options = { "Park", "Show History", "Log out", "Quit" };
            bool isCheckedIn = await DBQuery.IsCheckedIn(ActivePerson);
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
                await ShowPayAndLeave();
            }
            else if (!isCheckedIn && selection == 0)
            {
                await ShowParkingMenu();
            }
            else if (selection == 1)
            {
                await ShowPersonalHistory(ActivePerson);
            }
            else if (selection == options.Length - 2)
            {
                await LogOut();
            }
            await MainMenu();
        }

        public async Task<bool> ShowNamePrompt()
        {
            Console.Clear();
            Console.WriteLine("Please enter your name: ");
            string nameAnswer = Console.ReadLine();
            bool isSwChar = await swApi.ValidateSwName(nameAnswer);
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
                return await ShowNamePrompt();
            }

            return false;
        }

        private async Task ShowParkingMenu()
        {
            Console.Clear();
            List<SwStarship> starships = swApi.GetAllResources<SwStarship>(SwApiResource.starships).Result;
            List<SwStarship> personalStarships = await swApi.GetPersonStarships(ActivePerson);
            
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

            int availableParkingSpotID = await DBQuery.GetAvailableParkingSpotID(starship);

            if (availableParkingSpotID == 0)
            {
                Console.Clear();
                availableParkingSpotID = await DBQuery.GetNextSmallestAvailableParkingSpotID(starship);
                if (availableParkingSpotID == 0)
                {
                    Console.WriteLine("Sorry... No parking spot for your ship size available.");
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    return;
                }
                string menuPrompt = "Looks like there are no appropriate parking spots available, but there is a larger one available (more expensive), would you like to take that instead?";
                string[] yesNoOptions = { "Yes", "No" };
                int selectionForParkingSpot = Show(menuPrompt, yesNoOptions);
                if (selectionForParkingSpot == 1)
                {
                    return;
                }

            }
            await DBQuery.FillOccupancy(ActivePerson, starship.Name, availableParkingSpotID);
        }

        private async Task ShowPayAndLeave()
        {
            Console.Clear();
            Occupancy occupancy = await DBQuery.GetOpenOccupancyByName(ActivePerson);

            await DBQuery.AddPaymentAndDeparture(occupancy);

            await ShowInvoiceAndLogout(occupancy);
        }

        private async Task ShowInvoiceAndLogout(Occupancy occupancy)
        {
            Console.Clear();

            decimal billingHours = DBQuery.CalculateBillingHours(occupancy);

            decimal totalPrice = await DBQuery.GetPaymentForOccupancy(occupancy);

            Console.WriteLine($"Thank you for choosing us {ActivePerson}. You paid {totalPrice} for {billingHours} hours.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            await LogOut();
        }

        private async Task ShowPersonalHistory(string name)
        {
            Console.Clear();
            List<OccupancyHistory> history = await DBQuery.GetPersonalHistory(name);

            Console.WriteLine("Spaceship                           Date          Duration (in hours)   Amount Paid");
            Console.WriteLine("-----------------------------------------------------------------------------------");

            foreach(var data in history)
            {
                string spaceshipName = PadText(data.SpaceshipName, 32);

                string date = data.ArrivalTime.ToString("yyyy-MM-dd");
                date = PadText(date, 10);

                double duration = (data.DepartureTime - data.ArrivalTime).TotalHours;
                duration = Math.Round(duration, 1, MidpointRounding.ToPositiveInfinity);
                string durationText = duration.ToString();
                durationText = PadText(durationText, 18);

                string amountPaid = PadDecimal(data.AmountPaid, 11);

                Console.WriteLine($"{spaceshipName}    {date}    {durationText}    {amountPaid}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static string PadText(string text, int length)
        {
            string result = "";
            if (text.Length > length)
            {
                result = text.Substring(0, length - 3) + "...";
            }
            else
            {
                result = text.PadRight(length);
            }
            return result;
        }

        public static string PadDecimal(decimal number, int length)
        {
            string text = number.ToString();

            return text.PadLeft(length);
        }

        private async Task LogOut()
        {
            ActivePerson = "";
            Console.Clear();
            await ShowNamePrompt();
        }
    }
}
