using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftJava.Shared;

namespace MicrosoftJava.Test {
    [TestClass]
    public class LexerTest {
        private readonly string CSharpTestPath = @"CSharpTestCode.cs";

        [TestMethod]
        public void TestCSharpLexer() {
            var words = API.PublicAPI.ExtractTokens(new System.IO.StreamReader(CSharpTestPath), TLanguageType.CSharp);
            Assert.IsTrue(words.Count() > 0);
        }
    }
}
