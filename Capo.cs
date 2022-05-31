using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCapo
{
    public static class Capo
    {
        static object Interpreter;
        static bool debug = true;
        static bool hadRuntimeError = false;

        static void Main(string[] args)
        {
            if (!debug)
            {
                if (args.Length > 1)
                {
                    PrintUsage();
                    Environment.Exit(1);
                } else if (args.Length == 1)
                {
                    RunFile(args[1]);
                } else
                {
                    RunPrompt();
                }
            } else
            {
                RunFile(@"C:\Users\irwin.SUBIFOR\source\repos\ElCapo\sample.capo");
            }
        }

        static void RunFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"error: file not found: {path}");
                PrintUsage();
                return;
            }
            // read all text
            string source = File.ReadAllText(path);
            Error.fileName = path;
            Run(source + '\n');
            if (Error.hadError) Environment.Exit(1);
        }
        static void RunPrompt()
        {
            PrintHeader();
            while (true)
            {
                Console.Write(">>> ");
                string line = Console.ReadLine();
                if (line.Length == 0) break;
                Run(line + '\n');
                Error.hadError = false;
            }
        }

        static void Run(string source)
        {
            Lexer l = new Lexer(source);
            // debug
            bool outputTokens = true;
            if (outputTokens)
            {
                Token tok = l.NextToken();
                while (tok.type != TokenType.Eof)
                {
                    Console.WriteLine(tok);
                    tok = l.NextToken();
                }
                Console.WriteLine(tok);
            }
            // debug
        }

        static void PrintUsage()
        {
            Console.WriteLine("Usage: capo [script]");
            Console.WriteLine("use --help for a list of possible options");
        }

        static void PrintHeader()
        {
            string logo = @"
  ______ _    _____                  
 |  ____| |  / ____|                   | Welcome to `El Capo` programming language REPL console.
 | |__  | | | |     __ _ _ __   ___    | here you can experiment with the language syntax. For best
 |  __| | | | |    / _` | '_ \ / _ \   | experience, use a text editor and save your program in a
 | |____| | | |___| (_| | |_) | (_) |  | `program.capo` file and then execute: capo run program.capo
 |______|_|  \_____\__,_| .__/ \___/   | Use Ctrl+C or `exit` to exit, or `help` for other commands.
                        | |          
                        |_|                  
";
            Console.WriteLine(logo);
        }
    }
}
