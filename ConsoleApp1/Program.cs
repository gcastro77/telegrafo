using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string bits = "1011";
            //remuevo principio y fin, luego hacer automatico
            //string pattern = @"11";
            List<string> signals = new List<string>();

           // Match m = Regex.Match(bits, pattern, RegexOptions.Multiline);
            var listOfBits = bits.ToCharArray();
            int signalLength = 0;
            StringBuilder morseCode = new StringBuilder();
            foreach (char elem in listOfBits)
            {
                if (elem == '1')
                {
                    signalLength++;
                }
                else if (signalLength>0)
                {
                    signals.Add("".PadRight(signalLength, '1'));
                    signalLength = 0;
                }
            }
            if (signalLength > 0)
            {
                signals.Add("".PadRight(signalLength, '1'));
                signalLength = 0;
            }
            int minSignalLength = signals.First().Length;
            int maxDotLength = 2;
            int minDashLength = 3;

            
           
            var prevSignal = signals[0];

            if (prevSignal.Length < 3)
                maxDotLength = prevSignal.Length;
            else
                minDashLength = prevSignal.Length;

            int i = 1;
            while (i<signals.Count())
            {
                if (signals[i].Length > prevSignal.Length)
                {
                    if ((decimal)prevSignal.Length / (decimal)signals[i].Length > decimal.Parse("0,5"))
                    {

                    }
                    else
                    {
                        if (signals[i].Length >= 3) { 
                        minDashLength = signals[i].Length;
                        maxDotLength = minDashLength - 1;
                    }
                }

                    
                }
                prevSignal = signals[i];
                i++;
            };



        }
    }
}
