﻿using System;
using NLog.web;
using System.IO;

namespace MediaLibrary
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");
            Movie movie = new Movie{
                mediaId = 123,
                title = "Greatest Movie Ever, the (2020)",
                genres = { "Comedy", "Romance" }
            };
            Console.WriteLine(movie.Display());
            logger.Info("Program ended");
        }
    }
}
