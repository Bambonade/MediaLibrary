using System;
using NLog.Web;
using System.IO;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);
            MovieFile movieFile = new MovieFile(scrubbedFile);

            string choice = "";
            do{
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("Enter to quit");
                choice = Console.ReadLine();
                logger.Info("User choice: {choice}", choice);

                if(choice == "1"){
                    Movie movie = new Movie();
                    Console.WriteLine("Enter movie title");
                    movie.title = Console.ReadLine();
                    if(movieFile.isUniqueTitle(movie.title)){
                        string input;
                        do{
                            Console.WriteLine("Enter genre (or done to quit)");
                            input = Console.ReadLine();
                            if(input != "done" && input.Length > 0) movie.genres.Add(input);
                        }while(input != "done");
                        if(movie.genres.Count == 0) movie.genres.Add("(no genres listed)");

                        Console.WriteLine("Enter movie director");
                        string director = Console.ReadLine();
                        if(director == "") movie.director = "unassigned";
                        else movie.director = director;

                        Console.WriteLine("Enter running time (h:m:s)");
                        string timeEntry = Console.ReadLine();
                        string[] time = timeEntry.Split(":");

                        try {
                            int hours = Int32.Parse(time[0]);
                            try {
                                int minutes = Int32.Parse(time[1]);
                                try {
                                    int seconds = Int32.Parse(time[2]);
                                    movie.runningTime = new TimeSpan(hours,minutes,seconds);
                                } catch (Exception){
                                    movie.runningTime = new TimeSpan(hours,minutes,0);
                                }
                            } catch (Exception) {
                                movie.runningTime = new TimeSpan(hours,0,0);
                            }
                        } catch (Exception) {
                            movie.runningTime = new TimeSpan(0,0,0);
                        }
                    }
                    movieFile.AddMovie(movie);
                } else if(choice == "2") foreach(Movie m in movieFile.Movies) Console.WriteLine(m.Display());
            }while (choice == "1" || choice =="2");
            logger.Info("Program ended");
        }
    }
}