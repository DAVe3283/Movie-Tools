using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

// I totally just rip off their sample code
using DirectShowLib;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// The width of the video frame (if it can be determined)
        /// </summary>
        public int Width = 0;

        /// <summary>
        /// The height of the video frame (if it can be determined)
        /// </summary>
        public int Height = 0;

        /// <summary>
        /// Get the video quality
        /// </summary>
        public VideoQuality Quality
        {
            get
            {
                if ((Width == 0) || (Height == 0))
                {
                    return VideoQuality.Unknown;
                }
                else if (Width > 1888)
                {
                    return VideoQuality.HD_1080;
                }
                else if (Width > 1248)
                {
                    return VideoQuality.HD_720;
                }
                else
                {
                    double aspectRatio = (double)Width / (double)Height;
                    if (aspectRatio > 1.6)                                      // 16:10 or wider
                    {
                        return VideoQuality.SD_Widescreen;
                    }
                    else
                    {
                        return VideoQuality.SD_Fullscreen;
                    }
                }
            }
        }

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
        /// The number of trailers a movie appears to have
        /// </summary>
        public int Trailers = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Match things like "I'm A Moive! (2006) [PG-13] HD 1080p.mkv"
            Regex parser = new Regex(@"(^.+\((TV\s*)?\d{4}\)).*\.(mkv|avi)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex parserBetter = new Regex(@"(^.+\((TV\s*)?\d{4}\)).*", RegexOptions.Compiled | RegexOptions.IgnoreCase);

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
            StreamWriter file = new StreamWriter(@"C:\Users\DAVe\Desktop\movies.csv");
            file.WriteLine("\"Name\",\"Path\",\"Size\",\"Width\",\"Height\",\"Quality\",\"Trailers\",\"Added to Library\",\"Released in Theaters\"");
            // Tip: try setting the Size column's number format to:
            // [>1000000000]0.00,,," GB";[>1000000]0.00,," MB";0.00," KB"

            // Process the results
            System.DateTime startTime = System.DateTime.Now.AddDays(-7d);       // What movies to highlight as added recently
            int addedSinceStartTime = 0;
            foreach (Movie movie in movies)
            {
                // See if the release date is reasonable
                string releasedInTheaters = movie.ModifiedTime.ToShortDateString();
                if ((movie.ModifiedTime.Subtract(new TimeSpan(1, 0, 0)) < movie.CreatedTime) &&
                    (movie.ModifiedTime.Add(     new TimeSpan(1, 0, 0)) > movie.CreatedTime))
                {
                    // The release date is +/- 1 hour from the file creation date.
                    // That means it is probably not real. Because I'm good, but not that good.
                    releasedInTheaters = string.Empty;
                }

                // Write the CSV line
                string movieCSV = string.Format("\"{0}\",\"{1}\",{2},{3},{4},\"{5}\",{6},\"{7}\",\"{8}\"", movie.Name, movie.Path, movie.Size, movie.Width, movie.Height, movie.Quality.ToString(), movie.Trailers.ToString(), movie.CreatedTime, movie.ModifiedTime);
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
                                    if (movie.Name.EndsWith(".mkv", System.StringComparison.InvariantCultureIgnoreCase) ||
                                        movie.Name.EndsWith(".mp4", System.StringComparison.InvariantCultureIgnoreCase) ||
                                        movie.Name.EndsWith(".avi", System.StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        // We got a trailer!
                                        match.Trailers++;
                                    }
                                }
                            }
                        }

                        // Get movie metadata
                        // (this is ripped right out of one of the DirectShowLib samples, because I am lazy)
                        IFilterGraph2 m_FilterGraph = new FilterGraph() as IFilterGraph2;
                        ICaptureGraphBuilder2 icgb2 = new CaptureGraphBuilder2() as ICaptureGraphBuilder2;
                        try
                        {
                            // Link the ICaptureGraphBuilder2 to the IFilterGraph2
                            int hr = icgb2.SetFiltergraph(m_FilterGraph);
                            DsError.ThrowExceptionForHR(hr);

                            // Add the filters necessary to render the file.  This function will
                            // work with a number of different file types.
                            IBaseFilter sourceFilter = null;
                            hr = m_FilterGraph.AddSourceFilter(match.Path, match.Path, out sourceFilter);
                            DsError.ThrowExceptionForHR(hr);

                            // Get the SampleGrabber interface
                            ISampleGrabber m_sampGrabber = (ISampleGrabber)new SampleGrabber();
                            IBaseFilter baseGrabFlt = (IBaseFilter)m_sampGrabber;

                            // Configure the Sample Grabber
                            ConfigureSampleGrabber(m_sampGrabber);

                            // Add it to the filter
                            hr = m_FilterGraph.AddFilter(baseGrabFlt, "Ds.NET Grabber");
                            DsError.ThrowExceptionForHR(hr);

                            // Connect the pieces together, use the default renderer
                            hr = icgb2.RenderStream(null, null, sourceFilter, baseGrabFlt, null);
                            DsError.ThrowExceptionForHR(hr);

                            // Get video size
                            GetSize(m_sampGrabber, out match.Width, out match.Height);
                        }
                        catch (Exception ex)
                        {
                            System.Console.Error.Write("Error getting video metadata for ");
                            System.Console.Error.WriteLine(match.Name + ":");
                            System.Console.Error.WriteLine(ex.Message);
                        }
                        finally
                        {
                            // Cleanup COM objects
                            if (icgb2 != null)
                            {
                                Marshal.ReleaseComObject(icgb2);
                                icgb2 = null;
                            }
                            if (m_FilterGraph != null)
                            {
                                Marshal.ReleaseComObject(m_FilterGraph);
                                m_FilterGraph = null;
                            }

                            // I hate this, but if you don't force a garbage
                            // collection, none of the above released objects
                            // actually get released (stupid COM). So, to avoid
                            // horrible memory leaks, and possible DirectShow
                            // object count limits, I force a garbage collect :/
                            GC.Collect();
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
                System.Console.Out.Write("Ignored ");
                System.Console.Out.WriteLine(dir.FullName);
            }
        }

        // Set the options on the sample grabber
        private static void ConfigureSampleGrabber(ISampleGrabber sampGrabber)
        {
            AMMediaType media;
            int hr;

            // Set the media type to Video/RBG24
            media = new AMMediaType();
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;
            hr = sampGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            // Configure the samplegrabber
            hr = sampGrabber.SetBufferSamples(true);
            DsError.ThrowExceptionForHR(hr);
        }

        /// <summary>
        /// Get the size of the media, if possible
        /// </summary>
        /// <param name="sampGrabber">The sample grabber object to use</param>
        /// <param name="width">The detected width (if any)</param>
        /// <param name="height">The detected height (if any)</param>
        private static void GetSize(ISampleGrabber sampGrabber, out int width, out int height)
        {
            // Initialize out values to 0 in case try fails
            width = 0;
            height = 0;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            int hr = sampGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
                {
                    throw new NotSupportedException("Unknown Grabber Media Format");
                }

                // Get the struct
                VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
                Marshal.PtrToStructure(media.formatPtr, videoInfoHeader);

                // Grab the size info
                width = videoInfoHeader.BmiHeader.Width;
                height = videoInfoHeader.BmiHeader.Height;
            }
            finally
            {
                DsUtils.FreeAMMediaType(media);
                media = null;
            }
        }
    }
}
