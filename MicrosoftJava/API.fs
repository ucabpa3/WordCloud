module API

open System
open System.Linq
open Statements
open MicrosoftJava.Shared


type PublicAPI =
    static member ExtractTokens (fileContents:System.IO.StreamReader, lang: TLanguageType) =
        let lexbuf = Lexing.LexBuffer<char>.FromTextReader fileContents
        let tokens = [
            while not lexbuf.IsPastEndOfStream do
                let y = 
                    match lang with 
                        | TLanguageType.Java -> JavaLexer.tokenize lexbuf
                        | TLanguageType.CSharp -> CSharpLexer.tokenize lexbuf
                        | TLanguageType.C -> CPPLexer.tokenize lexbuf
                        | TLanguageType.CPlusPlus -> CPPLexer.tokenize lexbuf
                        | _ -> JavaLexer.tokenize lexbuf
                yield y
                //printfn "%A" y
            ] 

        let keywords = [
            for t in tokens do
                match t with
                    | IDENTIFIER(name) -> yield new Word(TokenType.Identifier, name, 1)
                    | KEYWORD(name) -> yield new Word(TokenType.Keyword, name, 1)
                    | LITERAL(name) -> 
                                            let lexbuf = Lexing.LexBuffer<char>.FromString name
                                            while not lexbuf.IsPastEndOfStream do
                                                let y = LiteralLexer.tokenize lexbuf
                                                match y with
                                                    | LITERALTOKEN(name1) -> yield new Word(TokenType.Literal, name1, 1)
                                                    | EO -> ()
                    | COMMENT(name) -> 
                                            let lexbuf = Lexing.LexBuffer<char>.FromString name
                                            while not lexbuf.IsPastEndOfStream do
                                                let y = CommentLexer.tokenize lexbuf
                                                match y with
                                                    | COMMENTTOKEN(name2) -> yield new Word(TokenType.Comment, name2, 1)
                                                    | EOL -> ()
                    | DONTCARE(name) -> ()
                    | EOF -> ()
        ]

        //printfn "%A" keywords

        Seq.ofList(keywords)