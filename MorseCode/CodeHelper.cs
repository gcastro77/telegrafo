using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MorseCode
{
    
    public class Frecuency
    {
        public int minDashLength { get; set; }
        public int maxDotLength { get; set; }
        public int maxSignalSpaceLength { get; set; }
        public int maxCharacterSpaceLength { get; set; }
        public int maxWordSpaceLength { get; set; }
    }
    public static class CodeHelper
    {
        private static Dictionary<char, string> translator = null;
        private const char dot = '.';
        private const char dash = '-';
        public static Dictionary<char, string> Translator {
            get
            {
                if (translator == null)
                {
                    

                    translator = new Dictionary<char, string>()
                        {
                            {'a', string.Concat(dot, dash)},
                            {'b', string.Concat(dash, dot, dot, dot)},
                            {'c', string.Concat(dash, dot, dash, dot)},
                            {'d', string.Concat(dash, dot, dot)},
                            {'e', dot.ToString()},
                            {'f', string.Concat(dot, dot, dash, dot)},
                            {'g', string.Concat(dash, dash, dot)},
                            {'h', string.Concat(dot, dot, dot, dot)},
                            {'i', string.Concat(dot, dot)},
                            {'j', string.Concat(dot, dash, dash, dash)},
                            {'k', string.Concat(dash, dot, dash)},
                            {'l', string.Concat(dot, dash, dot, dot)},
                            {'m', string.Concat(dash, dash)},
                            {'n', string.Concat(dash, dot)},
                            {'o', string.Concat(dash, dash, dash)},
                            {'p', string.Concat(dot, dash, dash, dot)},
                            {'q', string.Concat(dash, dash, dot, dash)},
                            {'r', string.Concat(dot, dash, dot)},
                            {'s', string.Concat(dot, dot, dot)},
                            {'t', string.Concat(dash)},
                            {'u', string.Concat(dot, dot, dash)},
                            {'v', string.Concat(dot, dot, dot, dash)},
                            {'w', string.Concat(dot, dash, dash)},
                            {'x', string.Concat(dash, dot, dot, dash)},
                            {'y', string.Concat(dash, dot, dash, dash)},
                            {'z', string.Concat(dash, dash, dot, dot)},
                            {'0', string.Concat(dash, dash, dash, dash, dash)},
                            {'1', string.Concat(dot, dash, dash, dash, dash)},
                            {'2', string.Concat(dot, dot, dash, dash, dash)},
                            {'3', string.Concat(dot, dot, dot, dash, dash)},
                            {'4', string.Concat(dot, dot, dot, dot, dash)},
                            {'5', string.Concat(dot, dot, dot, dot, dot)},
                            {'6', string.Concat(dash, dot, dot, dot, dot)},
                            {'7', string.Concat(dash, dash, dot, dot, dot)},
                            {'8', string.Concat(dash, dash, dash, dot, dot)},
                            {'9', string.Concat(dash, dash, dash, dash, dot)}
                        };

                }
                return translator;
            } }

        private static char EvaluateSignal(int signalCount, int maxDotLength, int minDashLength)
        {
            char result;
            //int maxDotLength = 5;
            //int minDashLength = 6;
            if (signalCount <= maxDotLength)
                result= dot;
            else if (signalCount >= minDashLength)
                result = dash;
            else
                throw new Exception();

            return result;
        }
        private static string EvaluatePause(int zeroCount, int maxCodeSpaceLength, int maxCharSpaceLength, int maxWordSpaceLength)
        {
            string result="";
            //int maxCodeSpaceLength = 3;
            //int maxCharSpaceLength = 7;
            //int maxWordSpaceLength = 8;
            
            if (zeroCount <= maxCodeSpaceLength)
                result = null;
            else if (zeroCount <= maxCharSpaceLength)
                result = " ";
            else if (zeroCount <= maxWordSpaceLength)
                result = "     ";

            return result;
        }

        public static string decodeBits2Morse(string bits)
        {
            var frecuency = DetectFrecuency(bits);
            
            int zeroCount = 0;
            int signalCount = 0;

            var listOfBits = bits.ToCharArray();

            StringBuilder morseCode = new StringBuilder();
            var ii = 0;
            foreach (char elem in listOfBits)
            {
                if (elem == '0')
                {
                    if (signalCount > 0)
                    {
                        morseCode.Append(EvaluateSignal(signalCount, frecuency.maxDotLength, frecuency.minDashLength));
                        signalCount = 0;
                    }

                    zeroCount++;
                }
                else
                {
                    if (zeroCount>0)
                    {
                        var pause = EvaluatePause(zeroCount,frecuency.maxSignalSpaceLength, frecuency.maxCharacterSpaceLength, frecuency.maxWordSpaceLength);
                        if (pause != null)
                            morseCode.Append(pause);
                    }
                    zeroCount = 0;
                    signalCount++;
                }
                ii  ++;
            }
            if (signalCount>0)
                morseCode.Append(EvaluateSignal(signalCount, frecuency.maxDotLength, frecuency.minDashLength));
            return morseCode.ToString();
        }


        public static string translate2Human(string codeMorse)
        {
            StringBuilder result = new StringBuilder();
            List<string> characters = codeMorse.Split(' ').ToList();
            bool spaceWasAdded = false;
            foreach(string character in characters)
            {
                var characterTranslated = Translator.FirstOrDefault(x => x.Value == character);
                if (characterTranslated.Value != null)
                {
                    spaceWasAdded = false;
                    result.Append(characterTranslated.Key);
                }
                else if (!spaceWasAdded)
                {
                    result.Append(" ");
                    spaceWasAdded = true;

                }
            }
            return result.ToString();
        }

        public static string translate2Morse(string human)
        {
            StringBuilder result = new StringBuilder();
            var characters = human.ToLower().ToArray();
         
            foreach (char character in characters)
            {
                if (character != ' ')
                {
                    result.Append(Translator[character]);
                    result.Append(' ');
                }
                else
                    result.Append(" ");
               
            }
            return result.ToString();
        }

        public static Frecuency DetectFrecuency(string bits)
        {
            Frecuency result = new Frecuency();
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
                else if (signalLength > 0)
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

            result.minDashLength = 3;
            result.maxDotLength = 2;

            signals = signals.OrderBy(x => x.Length).ToList();


            var prevSignal = signals[0];

            if (prevSignal.Length < 3)
                result.maxDotLength = prevSignal.Length;
            else
                result.minDashLength = prevSignal.Length;

            int i = 1;
            while (i < signals.Count())
            {
                if (signals[i].Length > prevSignal.Length)
                {
                    if ((decimal)prevSignal.Length / (decimal)signals[i].Length > decimal.Parse("0,5"))
                    {
                        if (signals[i].Length < 3)
                       
                        {
                            result.minDashLength = 3;
                            result.maxDotLength = 2;
                        }
                    }
                    else
                    {
                        if (signals[i].Length >= 3)
                        {
                            result.minDashLength = signals[i].Length;
                            result.maxDotLength = prevSignal.Length;
                        }
                        else
                        {
                            result.minDashLength = 3;
                            result.maxDotLength =2;
                        }
                    }


                }
                prevSignal = signals[i];
                i++;
            };

            result.maxSignalSpaceLength = result.maxDotLength;
            result.maxCharacterSpaceLength = result.maxDotLength*3;
            result.maxWordSpaceLength = result.maxDotLength * 3 * 3;
            return result;
        }
    }
}
