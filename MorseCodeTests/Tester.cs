using MorseCode;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

               
        [TestCase(".... --- .-.. .-  -- . .-.. ..", ExpectedResult = "HOLA MELI")]
        [TestCase(".... --- .-.. .-   -- . .-.. ..", ExpectedResult = "HOLA MELI")]
        public string ValidCodeMorse(string code)
        {
            var result = CodeHelper.translate2Human(code);
            return result.ToUpper();
           
        }

        [TestCase("10111010111000101", ExpectedResult = ".-.- ..")]
        [TestCase("000000000110110110011100000111111000111111001111110000000111011111111011101110000000110001111110000000000111111001111110000000110000110111111110111011100000011011100000000000", ExpectedResult = ".... --- .-.. .-     -- . .-.. ..")]
        [TestCase("10101010001110111011100010111010100010111000000000111011100010001011101010001010000000000000000000000", ExpectedResult = ".... --- .-.. .-     -- . .-.. ..")]
        [TestCase("101110101000000000111111011111101111110000001011111101010000001011111100000000000000000111111011111100000010000001011111101010000001010000000000000000000000", ExpectedResult = ".... --- .-.. .-     -- . .-.. ..")]
        public string ValidBit2Morse(string bits)
        {
            var result = CodeHelper.decodeBits2Morse(bits);
            return result.Trim();

        }
        [TestCase("HOLA MELI", ExpectedResult = ".... --- .-.. .-  -- . .-.. ..")]
        public string ValidHuman2Morse(string human)
        {
            var result = CodeHelper.translate2Morse(human);
            return result.Trim();


        }

        [TestCase("11101111", ExpectedResult = "maxDotLength=2;minDashLength:3")]
        [TestCase("111011110111111111", ExpectedResult = "maxDotLength=4;minDashLength:9")]
        [TestCase("10111", ExpectedResult = "maxDotLength=1;minDashLength:3")]
        [TestCase("1011", ExpectedResult = "maxDotLength=2;minDashLength:3")]
        [TestCase("0000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000000111111001111110000000110000110111111110111011100000011011100000000000", ExpectedResult = "maxDotLength=3;minDashLength:6")]
        
        public string DetectarFrecuenciaTrazo(string bits)
        {
            var result = CodeHelper.DetectFrecuency(bits);
            return $"maxDotLength={result.maxDotLength.ToString()};minDashLength:{result.minDashLength.ToString()}";
            


        }
    }
}