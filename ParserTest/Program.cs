using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaTexParser;

namespace ParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = "\\documentclass{article}\n"
                + "\\title{Test Title\\\\Subtitle}\n"
                + "\\author{One\\thanks{Someone}\\and Two\\thanks{Someone Else}\n"
                + "\\date{October, 2016}\n"
                + "\\begin{document}\n"
                + "\\maketitle\n"
                + "\n"
                + "Paragraph 1.\n"
                + "\n"
                + "Paragraph 2.\n"
                + "\\end{document}";

            Scanner scanner = new Scanner(testString);


            while (!scanner.LastChar)
            {
                char cur = scanner.NextChar();
                System.Console.WriteLine("(" + scanner.LineNumber + "," + scanner.CharNumber + "): " + cur);
            }

            System.Console.WriteLine("=== Reverse ===");

            while (!scanner.FirstChar)
            {
                char cur = scanner.PrevChar();
                System.Console.WriteLine("(" + scanner.LineNumber + "," + scanner.CharNumber + "): " + cur);
            }

            System.Console.ReadKey();
        }
    }
}
