using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElCapo
{
    public static class Error
    {
        public static bool hadError = false;
        public static string fileName = "";

        public static void ShowError(int line, int col, string message)
        {
            Report(line, col, message);
        }

        public static void ShowError(int line, int col, string message, bool exit)
        {
            Report(line, col, message);
            if (exit) Environment.Exit(1);
        }

        public static void ShowError(Token tok, string message)
        {
            Report(tok, message);
        }

        public static void Report(Token tok, string message)
        {
            StringBuilder strErr = new StringBuilder();
            if (fileName.Length > 0) strErr.Append($"{fileName}:{tok.line}:{tok.col} ");
            strErr.Append($"error: {message}");

            Console.WriteLine(strErr);
            hadError = true;
        }

        public static void Report(int line, int col, string message)
        {
            StringBuilder strErr = new StringBuilder();
            if (fileName.Length > 0) strErr.Append($"{fileName}:");
            strErr.Append($"{line}:{col}: ");
            strErr.Append($"error: {message}");

            Console.WriteLine(strErr.ToString());
            hadError = true;
        }
    }
}
