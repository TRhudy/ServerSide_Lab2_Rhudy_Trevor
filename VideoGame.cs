using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide_Lab2_Rhudy_Trevor
{
    public class VideoGame : IComparable<VideoGame>
    {
        public string Name { get; set; }
        public string Platform { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Publisher { get; set; }

        //Default empty constructor
        public VideoGame() { }

        //Class constructor w/ values
        public VideoGame(string name, string platform, int year, string genre, string publisher)
        {
            Name = name;
            Platform = platform;
            Year = year;
            Genre = genre;
            Publisher = publisher;
           
        }

        //sort the names of video games from a-z       
        public int CompareTo(VideoGame? other)
        {
            return Name.CompareTo(other.Name);
        }


        //Formats all the data into a string
        public override string ToString()
        {
            string VideoGameDisplay = "";
            VideoGameDisplay += $"Game Title: {Name}\n";
            VideoGameDisplay += $"Platform: {Platform}\n";
            VideoGameDisplay += $"Year: {Year}\n";
            VideoGameDisplay += $"Publisher: {Publisher}\n";

            return VideoGameDisplay;
        }

    }

}
