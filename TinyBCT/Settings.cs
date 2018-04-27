﻿using Fclp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyBCT
{
    class Settings
    {
        public static IList<string> InputFiles 
            = new List<string>();

        public static string OutputFile;

        // options should start with /  (currently there are no options)
        // every argument found after the first arg not starting with / will be considered a file to be processed
        public static void Load(string[] args)
        {
            var defaultFiles = new List<string>();
            defaultFiles.Add(@"..\..\..\Test\bin\Debug\Test.dll");
            //defaultFiles.Add(@"..\..\..\Test2\bin\Debug\Test2.dll");
            //defaultFiles.Add(@"..\..\..\Test3\bin\Debug\Test3.dll");

            var p = new FluentCommandLineParser();

            // example --inputFiles C:\file1.txt C:\file2.txt "C:\other file.txt"
            p.Setup<List<string>>('i', "inputFiles")
                .Callback(items => InputFiles = items)
                .SetDefault(defaultFiles)
                .WithDescription(@"Path for input files. By default it targets the test dlls. --inputFiles C:\file1.dll C:\file2.exe ""C:\other file.dll""");

            p.Setup<string>('o', "outputFile")
                .Callback(o => OutputFile = o)
                .SetDefault(String.Empty)
                .WithDescription("Path to output file. Any previous extension will be removed and .bpl will be added. By default it is the same name and path of the first input file.");

            var result = p.Parse(args);

            if (result.HasErrors)
            {
                foreach (var error in result.Errors)
                    Console.WriteLine(error);

                Console.ReadLine();
                System.Environment.Exit(1);
            }
            else
            {
                // check if the input files exist
                bool error = false;
                foreach (var f in InputFiles)
                {
                    if (!File.Exists(f))
                    {
                        error = true;
                        Console.WriteLine(String.Format("File {0} doesn't exit.", f));
                    }
                }

                if (error)
                {
                    Console.ReadLine();
                    System.Environment.Exit(1);
                }

                if (OutputFile.Equals(String.Empty))
                    OutputFile = InputFiles.First();
            }
        }

    }
}