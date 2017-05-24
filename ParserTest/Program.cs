using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LatexDotNet.Parser;
using System.IO;

namespace ParserTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = "\\this is a #$ test.\nPlease \\dis\\regard.";
            Tokenizer tokenizer = new Tokenizer(new StringReader(testString));
            while(true)
            {
                Token? token = tokenizer.GetNextToken();
                if(token.HasValue)
                {
                    Console.WriteLine(token);
                }
                else
                {
                    break;
                }
            }
            System.Console.ReadKey();
        }
    }
}
