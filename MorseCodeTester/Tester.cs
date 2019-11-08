using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorseCode;

namespace MorseCodeTester
{
    [TestClass]
    public class Tester
    {
        [TestMethod]
        public void ValidCodeMorse()
        {
            var result =  CodeHelper.translate2Human(".... --- .-.. .-  -- . .-.. ..");
            Assert.AreEqual("HOLA MELI",result.ToUpper());

            result = CodeHelper.translate2Human(".... --- .-.. .-   -- . .-.. ..");
            Assert.AreEqual("HOLA MELI", result.ToUpper());
        }
        [TestMethod]
        public void ValidBit2Morse()
        {
            var result = CodeHelper.decodeBits2Morse("0000000001101101100111000001111110001111110011111100000001110111111110111011100000001100011111100000000111111001111110000000110000110111111110111011100000011011100000000000");
            Assert.AreEqual(".... --- .-.. .-     -- . .-.. ..", result.Trim().ToUpper());

        }
    }
}
