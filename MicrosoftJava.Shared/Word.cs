using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicrosoftJava.Shared {
    public enum TokenType {
        Identifier = 1,
        Keyword = 2,
        Literal = 3
    }

    public class Word {
        #region Initialization

        public Word(TokenType type, string name, int count) {
            this.Type = type;
            this.Name = name;
            this.Count = count;
        }

        #endregion

        #region Properties

        public TokenType Type { get; private set; }
        public string Name { get; private set; }
        public int Count { get; private set; }

        #endregion

        #region Overrides

        public override string ToString() {
            return Type.ToString() + " " + Name + " " + Count.ToString();
        }

        #endregion
    }
}
