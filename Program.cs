using System.Net.Http.Headers;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Collections;
using System.Reflection.Metadata;
using System.Reflection;
/*Lab 2: Advanced C# Basics
 * Last Edited: 
 * Sources used:
 *         -OpenAI. "GPT-3: A Language Model for Natural Language Processing." OpenAI, Year of model release (e.g., 2020), chat.openai.com/c/1bec495c-6ce9-449b-b126-de5a5cfbd69f
 *         -Daniels, Tyler. “Mapping .CSV Rows to a Dictionary.” Code Review Stack Exchange, 1 Sept. 1963, codereview.stackexchange.com/questions/162057/mapping-csv-rows-to-a-dictionary. 
 */
namespace ServerSide_Lab2_Rhudy_Trevor
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            string currentFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.ToString();
            string filePath = currentFolder + Path.DirectorySeparatorChar + "videogames.csv";

            //Creation of the game library list
            List<VideoGame> gameLibrary = new List<VideoGame>();

            //Adds all values from the file into the list
            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string? linePulled = reader.ReadLine();

                    string[] lineInformation = linePulled.Split(',');

                    VideoGame vg = new VideoGame()
                    {
                        Name = lineInformation[0],
                        Platform = lineInformation[1],
                        Year = Int32.Parse(lineInformation[2]),
                        Genre = lineInformation[3],
                        Publisher = lineInformation[4],

                    };

                    gameLibrary.Add(vg);
                }

            } //end using statement

            //sorts list
            gameLibrary.Sort();

            //Generate list of unique platforms
            var uniquePlatforms = gameLibrary.Select(x => x.Platform).Distinct();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("----------------------------------------------------------------------------------------");
            Console.WriteLine("Welcome to Lab 2: Advanced C#, programmed by Trevor \"Goose\" Rhudy");
            Console.WriteLine("----------------------------------------------------------------------------------------\n\n");

            Console.WriteLine("Now if you've seen my last lab, displaying all the developers is a little much....");
            Console.WriteLine($"So this time around we're using platforms! Please input a number from 1 to {uniquePlatforms.Count()}.\n");
            Console.WriteLine("----------------------------------------------------------------------------------------\n");

            int platformCounter =1;
            foreach (var platform in uniquePlatforms)
            {

                Console.WriteLine($"{platformCounter}. {platform}");
                platformCounter++;
            }
            Console.WriteLine("\n\n");

            //checks the numerical input
            string platformSelectionInString= "";
            int platformSelectionInInt;
            bool validInput = false;

            while (!validInput)
            {
                if (int.TryParse(Console.ReadLine(), out platformSelectionInInt) && platformSelectionInInt >= 1 && platformSelectionInInt <= uniquePlatforms.Count())
                {
                    var uniquePlatformList = uniquePlatforms.ToList();

                    platformSelectionInString += uniquePlatformList[platformSelectionInInt - 1];

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nYou Selected: {platformSelectionInString}\n");
                    Console.ForegroundColor = ConsoleColor.White;

                    validInput = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nInvalid input! Please enter a valid number.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }




            List<VideoGame> filteredLibraryByPublisher = gameLibrary.Where(vg => vg.Platform == platformSelectionInString).ToList();

            //Create Stack and queue and dictionary for filtered games
         
            Dictionary<string, List<VideoGame>> gameByPlatform = new Dictionary<string, List<VideoGame>>();
           
            //populates the dictionary with the filtered platform
            foreach (var game in filteredLibraryByPublisher)
            {
                if(!gameByPlatform.ContainsKey(game.Platform))
                {
                    gameByPlatform[game.Platform] = new List<VideoGame>();

                }
                gameByPlatform[game.Platform].Add(game);
            }

            bool hasPlatform = gameByPlatform.ContainsKey(platformSelectionInString);

            //Displays all values in the dictionary if the platform is present
            if (hasPlatform)
            {
                Console.WriteLine($"Each video game from {platformSelectionInString} displayed using a Dictionary:\n");
                foreach(var game in gameByPlatform[platformSelectionInString])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Name: {game.Name},\nYear: {game.Year},\nGenre: {game.Genre},\nPublisher: {game.Publisher}\n\n");
                }
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine($"No games found for the {platformSelectionInString}!");
            }

            Console.ForegroundColor= ConsoleColor.White;

          


            Stack<VideoGame> videoGameStack = new Stack<VideoGame>(filteredLibraryByPublisher);
            Queue<VideoGame> videoGameQueue = new Queue<VideoGame>(filteredLibraryByPublisher);

            //Peeks at the top game using Stack
            Console.ForegroundColor = ConsoleColor.Yellow;
            VideoGame peekedItem = videoGameStack.Peek();
            Console.WriteLine($"Peeking at the top video game using Stack:\n");
            Console.WriteLine(peekedItem);

            
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Peeking at the top game using queue
            Console.WriteLine("The same can be done using queue:\n");
            VideoGame queuePeekedItem = videoGameQueue.Peek();
            Console.WriteLine(queuePeekedItem);

            //Removing a random game, first checking if the stack is empty, and displaying it
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (videoGameStack.Count > 0)
            {
                //Generate a random index within the range of the stack
                Random random = new Random();
                int randomIndex = random.Next(0, videoGameStack.Count);

                //Create an array from the stack, remove the random item, and convert it back to a stack
                VideoGame[] gamesArray = videoGameStack.ToArray();
                VideoGame randomGame = gamesArray[randomIndex];
                videoGameStack.Clear();
                foreach (var game in gamesArray)
                {
                    if (!game.Equals(randomGame))
                    {
                        videoGameStack.Push(game);
                    }
                }

                Console.WriteLine("Random popped game from the Stack:\n\n" + randomGame);
            }
            else
            {
                Console.WriteLine("Stack is empty.\n");
            }

            //This Dequeues a random game from the Queue and displays the removed game
            Console.ForegroundColor= ConsoleColor.Cyan;
            if (videoGameQueue.Count > 0)
            {
                // Generate a random index within the range of the queue
                Random random2 = new Random();
                int randomIndex2 = random2.Next(0, videoGameQueue.Count);

                // Dequeue items until you reach the random index
                for (int i = 0; i < randomIndex2; i++)
                {
                    VideoGame removedGame = videoGameQueue.Dequeue();
                    // You can optionally process or save the removed items here
                }

                // Now, 'randomGame' contains the dequeued random item
                VideoGame randomGame = videoGameQueue.Dequeue();
                Console.WriteLine("Random dequeud game from the Queue:\n\n" + randomGame);
            }
            else
            {
                Console.WriteLine("Queue is empty.\n");
            }

            Console.ForegroundColor = ConsoleColor.Black;
         
        }
    }
}