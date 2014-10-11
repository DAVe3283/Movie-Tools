using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace MovieInfo
{
    /// <summary>
    /// A Move object to hold details on the move
    /// </summary>
    public class Movie
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
        /// Gets or sets the modification time of the movie.
        /// (This is usually when the movie was released in theaters.)
        /// </summary>
        public DateTime ModifiedTime
        {
            get
            {
                return File.LastWriteTime;
            }
            set
            {
                File.LastWriteTime = value;
            }
        }

        /// <summary>
        /// Get the video quality
        /// </summary>
        public VideoQuality Quality
        {
            get
            {
                // Try and grab movie quality from the filename
                if (hd1080.IsMatch(Path))
                {
                    return VideoQuality.HD_1080;
                }
                else if (hd720.IsMatch(Path))
                {
                    return VideoQuality.HD_720;
                }
                else if (sdWidescreen.IsMatch(Path))
                {
                    return VideoQuality.SD_Widescreen;
                }
                else if (sdFullscreen.IsMatch(Path))
                {
                    return VideoQuality.SD_Fullscreen;
                }
                else
                {
                    return VideoQuality.Unknown;
                }
            }
        }

        // Match video quality in filenames like "The Shawshank Redemption (1994) [R] HD 1080p.mkv"
        private static Regex hd1080 = new Regex(@".*\bHD\s+1080[pi]\b.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex hd720 = new Regex(@".*\bHD\s+720[pi]\b.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex sdWidescreen = new Regex(@".*\bWidescreen\b.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex sdFullscreen = new Regex(@".*\bFullscreen\b.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The quality of a video
        /// </summary>
        public enum VideoQuality
        {
            /// <summary>
            /// Unknown quality (could not get dimensions?)
            /// </summary>
            Unknown,

            /// <summary>
            /// Standard Definition (DVD), Fullscreen
            /// </summary>
            SD_Fullscreen,

            /// <summary>
            /// Standard Definition (DVD), Widescreen
            /// </summary>
            SD_Widescreen,

            /// <summary>
            /// High Definition (Blu-Ray), 720p/i
            /// </summary>
            HD_720,

            /// <summary>
            /// High Definition (Blu-Ray), 1080p/i
            /// </summary>
            HD_1080,
        }

        /// <summary>
        /// Get the MPAA Rating of the file
        /// </summary>
        public MpaaRating Rating
        {
            get
            {
                // Test in order from most to least offensive, in case a path is misleading
                // (better to err on the side of caution)
                if (ratingUnrated.IsMatch(Path))
                {
                    return MpaaRating.Unrated;
                }
                else if (ratingG.IsMatch(Path))
                {
                    return MpaaRating.G;
                }
                else if (ratingPG.IsMatch(Path))
                {
                    return MpaaRating.PG;
                }
                else if (ratingPG13.IsMatch(Path))
                {
                    return MpaaRating.PG13;
                }
                else if (ratingR.IsMatch(Path))
                {
                    return MpaaRating.R;
                }
                else if (ratingNC17.IsMatch(Path))
                {
                    return MpaaRating.NC17;
                }
                else if (ratingApproved.IsMatch(Path))
                {
                    return MpaaRating.Approved;
                }
                else
                {
                    return MpaaRating.Unknown;
                }
            }
        }

        /// <summary>
        /// Get the raw rating string for the movie
        /// </summary>
        public string RatingString
        {
            get
            {
                MatchCollection matches = appearsToHaveRating.Matches(Path);
                if (matches.Count >= 1)
                {
                    return matches[0].Groups[1].Value;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        // Match MPAA rating in filenames like "RED (2010) [PG-13] HD 1080p.mkv"
        private static Regex appearsToHaveRating = new Regex(@".*\s+\[([\w\-]{1,8})\]\s.*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingUnrated = new Regex(@"\[(UR|Unrated)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingG = new Regex(@"\[G\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingPG = new Regex(@"\[PG\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingPG13 = new Regex(@"\[PG\-?13\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingR = new Regex(@"\[R\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingNC17 = new Regex(@"\[NC\-?17\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static Regex ratingApproved = new Regex(@"\[Approved\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// The MPAA Film Rating of a movie
        /// </summary>
        public enum MpaaRating
        {
            /// <summary>
            /// Cannot determine movie rating (not in filename, or invalid rating text)
            /// </summary>
            Unknown,

            /// <summary>
            /// Not rated by the MPAA (i.e. older movies and director's cuts)
            /// </summary>
            Unrated,

            /// <summary>
            /// General Audiences
            /// </summary>
            G,

            /// <summary>
            /// Parental Guidance Suggested
            /// </summary>
            PG,

            /// <summary>
            /// Parents Strongly Cautioned
            /// </summary>
            PG13,

            /// <summary>
            /// Restricted
            /// </summary>
            R,

            /// <summary>
            /// No One 17 And Under Admitted
            /// </summary>
            NC17,

            /// <summary>
            /// Pre-1968 titles only
            /// (from the MPAA site: "Under the Hays Code, films were simply approved or disapproved based on whether they were deemed 'moral' or 'immoral'.")
            /// </summary>
            Approved,
        }

        /// <summary>
        /// The number of trailers a movie appears to have
        /// </summary>
        public int Trailers = 0;

        private static Regex hasAVS = new Regex(@"\[AVS\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Does the movie have an Alternate Video Stream?
        /// </summary>
        public bool AVS
        {
            get
            {
                return hasAVS.IsMatch(Path);
            }
        }

        /// <summary>
        /// Does the movie appear to have a MBMovie.json file?
        /// </summary>
        public bool hasJson = false;
    }

    class Program
    {
        /// <summary>
        /// Valid file extensions for movie files
        /// </summary>
        public static string[] validExtensions = 
        {
            ".avi",
            ".mkv",
            ".mp4",
            ".wmv",
        };

        /// <summary>
        /// Determines if a filename has a valid extension
        /// (and is therefore likely to be a movie file)
        /// </summary>
        /// <param name="filename">The filename or path of the movie</param>
        /// <returns>If the file is valid</returns>
        public static bool hasValidExtension(string filename)
        {
            bool isValid = false;
            foreach (string extension in validExtensions)
            {
                if (filename.EndsWith(extension, StringComparison.InvariantCultureIgnoreCase))
                {
                    isValid = true;
                    break;
                }
            }
            return isValid;
        }

        static void Main(string[] args)
        {
            // Allowable time drift (hours)
            int allowedTimeError = 4;
            bool fixFileTimesWithinError = true;

            // Match the movie folders (things like "I'm A Moive! (2006) [tmdbid=3283]"
            Regex parserBetter = new Regex(@"(^.+\((TV\s*)?\d{4}\)).*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Prepare to search
            DirectoryInfo movieDir = new DirectoryInfo(@"\\Legion\Movies");     // Where to search for movies
            List<Movie> movies = new List<Movie>();                             // Holds the matched movies
            string[] ignores =                                                  // A list of folders to ignore
            {
                "From ",
                "Trailers",
                "Special Features",
            };

            // Get the moves
            Console.WriteLine("Reading movie directory " + movieDir.FullName + "...");
            ParseDirectory(movieDir, ref parserBetter, ref ignores, ref movies);
            Console.WriteLine("Completed reading movie directory.");
            Console.WriteLine();

            // Do we want to fix time error?
            Console.WriteLine("Do you want to fix time offset error on each movie?");
            Console.WriteLine("-- Only movies within ±" + allowedTimeError.ToString() + " hour(s) of midnight will be fixed. --");
            Console.WriteLine("-- This is usually harmless, and very fast --");
            Console.Write("Correct time error (Y/N)? [");
            Console.Write(fixFileTimesWithinError ? "Y" : "N");
            Console.Write("] ");
            do
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(true);              // Don't show the key press
                if (keyPressed.Key == ConsoleKey.Y)
                {
                    fixFileTimesWithinError = true;
                    break;
                }
                else if (keyPressed.Key == ConsoleKey.N)
                {
                    fixFileTimesWithinError = false;
                    break;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    // Leave default
                    break;
                }
            } while (true);
            Console.WriteLine();
            Console.WriteLine();

            // Make a new csv
            StreamWriter file = new StreamWriter("C:\\Users\\DAVe\\Google Drive\\Movie List\\movies " + DateTime.Now.ToShortDateString() + ".csv");
            file.WriteLine("\"Name\",\"Path\",\"Size\",\"Quality\",\"Rating\",\"Trailers\",\"AVS\",\"Added to Library\",\"Released in Theaters\"");
            // Tip: try setting the Size column's number format to:
            // [>1000000000]0.00,,," GB";[>1000000]0.00,," MB";0.00," KB"
            // or since most movies are ≫ 1 MB
            // [>1000000000000]0.000,,,," TB";[>1000000000]0.00,,," GB";0.00,," MB"

            // Process the results
            System.DateTime startTime = System.DateTime.Now.AddDays(-7d);       // What movies to highlight as added recently
            int addedSinceStartTime = 0;
            int timeErrors = 0;
            foreach (Movie movie in movies)
            {
                // Sanity check release date & time
                string releasedInTheaters = string.Empty;
                if ((movie.ModifiedTime.Minute == 0) &&
                    (movie.ModifiedTime.Second == 0))
                {
                    // If the release minute & second are 0, it is probably a real date
                    // Now we just need to allow for time zones & DST
                    TimeSpan lowErr = new TimeSpan(allowedTimeError, 0, 0);
                    TimeSpan highErr = new TimeSpan(24 - allowedTimeError, 0, 0);
                    TimeSpan err = TimeSpan.MaxValue;
                    if (movie.ModifiedTime.TimeOfDay <= lowErr)
                    {
                        // Movie is slightly after midnight
                        err = -movie.ModifiedTime.TimeOfDay;
                    }
                    else if (movie.ModifiedTime.TimeOfDay >= highErr)
                    {
                        // Movie is slightly before midnight
                        err = new TimeSpan(24, 0, 0) - movie.ModifiedTime.TimeOfDay;
                    }

                    // Let's correct the movie's time
                    if (err != TimeSpan.MaxValue)
                    {
                        DateTime releaseDate = movie.ModifiedTime.Add(err);
                        releasedInTheaters = releaseDate.ToShortDateString();

                        // Fix file time?
                        if (!err.Equals(TimeSpan.Zero))
                        {
                            ++timeErrors;
                            if (fixFileTimesWithinError)
                            {
                                movie.ModifiedTime = releaseDate;
                                Console.WriteLine("Fixed time error of " + err.ToString() + " on " + movie.Name);
                            }
                        }
                    }
                }

                // Write the CSV line
                string movieCSV = string.Format(
                    "\"{0}\",\"{1}\",{2},\"{3}\",\"{4}\",{5},\"{6}\",\"{7}\",\"{8}\"",
                    movie.Name, movie.Path, movie.Size, movie.Quality.ToString(), movie.Rating.ToString(), movie.Trailers.ToString(), movie.AVS.ToString(), movie.CreatedTime, releasedInTheaters);
                file.WriteLine(movieCSV);

                // See if it is new
                if (movie.CreatedTime >= startTime)
                {
                    ++addedSinceStartTime;
                    Console.Out.WriteLine("Added recently: " + movie.Name);
                }
            }

            // Close the csv
            file.Close();
            file.Dispose();

            // Finish
            Console.WriteLine();
            Console.Out.WriteLine("Found " + movies.Count.ToString() + " movies.");
            Console.Out.WriteLine(addedSinceStartTime.ToString() + " movie(s) added since " + startTime.ToString());
            Console.Out.WriteLine(timeErrors + " movie(s) " + (fixFileTimesWithinError ? "had" : "have") + " time errors that were " + (fixFileTimesWithinError ? string.Empty : "NOT ") + "fixed.");
            Console.Out.WriteLine("Press any key to quit.");
            Console.ReadKey();
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
                    if (subdir.Name.Contains(ignore))
                    {
                        skip = true;
                        break;
                    }
                }
                if (skip)
                {
                    //System.Console.Out.Write("Ignored ");
                    //System.Console.Out.WriteLine(subdir.FullName);
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
                    if (hasValidExtension(movie.Name))
                    {
                        // We have a movie
                        Movie match = new Movie();
                        match.Name = name;
                        match.File = movie;

                        // Do we have trailers?
                        foreach (DirectoryInfo trailers in dir.GetDirectories())
                        {
                            // Look for a Trailers subdirectory
                            if (trailers.Name.Equals("Trailers", StringComparison.InvariantCultureIgnoreCase))
                            {
                                // Find movie files
                                foreach (FileInfo trailer in trailers.GetFiles())
                                {
                                    if (hasValidExtension(trailer.Name))
                                    {
                                        // We got a trailer!
                                        match.Trailers++;
                                    }
                                }
                            }
                        }

                        // Do we have MBMovie.json?
                        if (dir.GetFiles("MBMovie.json").Length == 1)
                        {
                            match.hasJson = true;
                        }
                        else
                        {
                            System.Console.Out.Write("??? Could not find MBMovie.json in ");
                            System.Console.Out.WriteLine(dir.FullName);
                        }

                        // Sanity check the MPAA rating
                        if ((match.Rating == Movie.MpaaRating.Unknown) && !string.IsNullOrEmpty(match.RatingString))
                        {
                            System.Console.Out.WriteLine(string.Format(
                                "??? Can't determine movie rating from rating string \"{0}\" in {1}",
                                match.RatingString, match.Path));
                        }

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
                // Only warn on folders that are not obviously a collection
                if (!dir.Name.Contains("Collection"))
                {
                    System.Console.Out.Write("Ignored ");
                    System.Console.Out.WriteLine(dir.FullName);
                }
            }
        }
    }
}
