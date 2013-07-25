using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MovieInfo
{
    /// <summary>
    /// A Move object to hold details on the move
    /// </summary>
    public struct Movie
    {
        /// <summary>
        /// The (nice) name of the movie
        /// </summary>
        public string Name;

        /// <summary>
        /// The raw FileInfo object of the move file
        /// </summary>
        public FileInfo File;

        /// <summary>
        /// The full path to the movie file
        /// </summary>
        public string Path
        {
            get
            {
                return File.FullName;
            }
        }

        /// <summary>
        /// The size of the movie file
        /// </summary>
        public long Size
        {
            get
            {
                return File.Length;
            }
        }

        /// <summary>
        /// The creation time of the movie.
        /// (This is usually when I added it to my collection.)
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return File.CreationTime;
            }
        }

        /// <summary>
        /// The modification time of the movie.
        /// (This is usually when the movie was released in theaters.)
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return File.LastWriteTime;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Move movies to subdirectories
            //// Match things like "I'm A Moive! (2006) [PG-13] HD 1080p.mkv"
            //Regex parser = new Regex(@"(^.+\(\d{4}\)).*\.(mkv|avi)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            ////DirectoryInfo movieDir = new DirectoryInfo(@"\\DAVe-Server-2\Movies");
            //DirectoryInfo movieDir = new DirectoryInfo(@"T:\DAVe-Server-2\Data\Movies");
            //foreach (FileInfo movie in movieDir.GetFiles())
            //{
            //    Match movieName = parser.Match(movie.Name);
            //    if (movieName.Success)
            //    {
            //        string newFolder = Path.Combine(movieDir.FullName, movieName.Groups[1].Value);
            //        if (!Directory.Exists(newFolder))
            //        {
            //            Directory.CreateDirectory(newFolder);
            //        }
            //        string newPath = Path.Combine(newFolder, movie.Name);
            //        movie.MoveTo(newPath);
            //    }
            //}


            // Match things like "I'm A Moive! (2006) [PG-13] HD 1080p.mkv"
            Regex parser = new Regex(@"(^.+\((TV\s*)?\d{4}\)).*\.(mkv|avi)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex parserBetter = new Regex(@"(^.+\((TV\s*)?\d{4}\)).*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            StreamWriter file = new StreamWriter(@"C:\Users\DAVe\Desktop\movies.csv");

            // Prepare to search
            DirectoryInfo movieDir = new DirectoryInfo(@"\\DAVe-Server-2\Movies");  // Where to search for movies
            List<Movie> movies = new List<Movie>();                             // Holds the matched movies
            string[] ignores =                                                  // A list of folders to ignore
            {
                "From ",
                "Trailers",
                "Special Features",
            };

            // Get the moves
            ParseDirectory(movieDir, ref parserBetter, ref ignores, ref movies);

            // Make a new csv
            file.WriteLine("\"Name\",\"Path\",\"Size\",\"Added to Library\",\"Released in Theaters (may be wrong)\"");

            // Process the results
            System.DateTime startTime = System.DateTime.Now.AddDays(-7d);       // What movies to highlight as added recently
            int addedSinceStartTime = 0;
            foreach (Movie movie in movies)
            {
                // Write the CSV line
                string movieCSV = string.Format("\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\"", movie.Name, movie.Path, movie.Size, movie.CreatedTime, movie.ModifiedTime);
                file.WriteLine(movieCSV);

                // See if it is new
                if (movie.CreatedTime >= startTime)
                {
                    ++addedSinceStartTime;
                    System.Console.Out.WriteLine("Added recently: " + movie.Name);
                }
            }

            // Close the csv
            file.Close();
            file.Dispose();

            // Finish
            System.Console.WriteLine();
            System.Console.Out.WriteLine("Found " + movies.Count.ToString() + " movies.");
            System.Console.Out.WriteLine(addedSinceStartTime.ToString() + " were added since " + startTime.ToString());
            System.Console.Out.WriteLine("Press any key to quit.");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Parses a directory to get the movies in that directory
        /// </summary>
        /// <param name="dir">The directory to parse</param>
        /// <param name="parser">The Regex to match movies</param>
        /// <param name="ignores">A list of subdirectory names to ignore</param>
        /// <param name="movies">The list of movies</param>
        static void ParseDirectory(DirectoryInfo dir, ref Regex parser, ref string[] ignores, ref List<Movie> movies)
        {
            // Do all subdirectories (recursively)
            foreach (DirectoryInfo subdir in dir.GetDirectories())
            {
                // Skip ignores
                bool skip = false;
                foreach (string ignore in ignores)
                {
                    if (subdir.FullName.Contains(ignore))
                    {
                        skip = true;
                        break;
                    }
                }
                if (skip)
                {
                    System.Console.Out.Write("Ignored ");
                    System.Console.Out.WriteLine(subdir.FullName);
                    continue;
                }

                // Process directory
                ParseDirectory(subdir, ref parser, ref ignores, ref movies);
            }

            Match dirName = parser.Match(dir.Name);
            if (dirName.Success)
            {
                // We have a movie directory
                string name = dirName.Groups[1].Value;

                // Get the actual file
                foreach (FileInfo movie in dir.GetFiles())
                {
                    if (movie.Name.EndsWith(".mkv", System.StringComparison.InvariantCultureIgnoreCase) ||
                        movie.Name.EndsWith(".mp4", System.StringComparison.InvariantCultureIgnoreCase) ||
                        movie.Name.EndsWith(".avi", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        // We have a movie
                        Movie match;
                        match.Name = name;
                        match.File = movie;

                        // Done
                        movies.Add(match);
                        return;
                    }
                }

                // Failure
                System.Console.Out.Write("!!! Could not find movie in ");
                System.Console.Out.WriteLine(dir.FullName);
            }
            else
            {
                System.Console.Out.Write("Ignored ");
                System.Console.Out.WriteLine(dir.FullName);
            }
        }
    }
}
