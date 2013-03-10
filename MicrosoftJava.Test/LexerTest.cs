using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MicrosoftJava.Shared;

namespace MicrosoftJava.Test {
    [TestClass]
    public class LexerTest {
        private readonly string CSharpTestPath = @"CSharpTestCode.cs";
        private readonly string CTestPath = @"C:\Users\vasil.vasilev\Dropbox\UCL\Advanced Analysis and Design A\Project\klee-build-env\klee-uclibc-0.02-x64\libpthread\linuxthreads\sysdeps\mips\tls.h";

        [TestMethod]
        public void TestCSharpLexer() {
            var words = API.PublicAPI.ExtractTokens(new System.IO.StreamReader(CSharpTestPath), TLanguageType.CSharp);
            Assert.IsTrue(words.Count() > 0);
        }

        [TestMethod]
        public void TestCLexer() {
            var words = API.PublicAPI.ExtractTokens(new System.IO.StreamReader(CTestPath), TLanguageType.C);
            Assert.IsTrue(words.Count() > 0);
        }
     
           
    }
}
