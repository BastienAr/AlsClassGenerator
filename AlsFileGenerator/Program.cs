using AlsFileGenerator.descriptor;
using AlsFileGenerator.generator;
using AlsFileGenerator.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AlsFileGenerator
{
  class Program
  {
    static void Main(string[] args)
    {
      ConsoleColor fgColor = Console.ForegroundColor;

      if (args.Length != 2)
      {
        Console.WriteLine("Usage : AlsFileGenerator inputDir outputDir");
        Environment.Exit(3);
      }

      DirectoryInfo inputDir = new DirectoryInfo(args[0]);
      if (!inputDir.Exists)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Couldn't find input directory {0}", args[0]);
        Console.ForegroundColor = fgColor;
        Environment.Exit(3);
      }

      DirectoryInfo outputDir = new DirectoryInfo(args[1]);
      if (!outputDir.Exists)
      {
        Console.WriteLine("Creating output dir...");
        outputDir.Create();
      }

      Console.WriteLine("Analyzing input ...");

      int nbFiles = inputDir.GetFiles("*.xml").Length;

      Console.WriteLine("Found {0} file(s) to generate.", nbFiles);
      Console.WriteLine("Starting generation...");

      AlsClassDescriptor als;
      XmlSerializer xs = new XmlSerializer(typeof(AlsClassDescriptor));
      Dictionary<FileInfo, Exception> errors = new Dictionary<FileInfo, Exception>();
      int currFile = 1;

      TextProgressBar progressBar = new TextProgressBar();
      progressBar.Size = 50;
      progressBar.MaxValue = nbFiles;
      progressBar.CurrentValue = 0;

      foreach (var file in inputDir.GetFiles("*.xml"))
      {
        progressBar.CurrentValue++;
        try
        {
          using (StreamReader rd = new StreamReader(file.FullName))
          {
            als = xs.Deserialize(rd) as AlsClassDescriptor;
          }
          HGenerator hGene = new HGenerator();
          CppGenerator cppGene = new CppGenerator();
          hGene.Generate(als, Path.Combine(args[1], als.ClassName.ToLower() + ".h"));
          cppGene.Generate(als, Path.Combine(args[1], als.ClassName.ToLower() + ".cpp"));
        }
        catch (Exception e)
        {
          errors.Add(file, e);
        }
        currFile++;
      }

      Console.WriteLine("");
      if (errors.Count > 0)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("The generator has encoutered error on the following files :");
        errors.ToList().ForEach(pair => Console.WriteLine("{0} error : {1}", pair.Key.FullName, pair.Value.Message));
        Console.ForegroundColor = fgColor;
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Sucess.");
        Console.ForegroundColor = fgColor;
      }
    }
  }


}
