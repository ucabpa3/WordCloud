module API

open System
open System.Linq
open Statements
open MicrosoftJava.Shared


type PublicAPI =
    static member ExtractTokens (path:string) =
        let file = new System.IO.StreamReader(path)

        let lexbuf = Lexing.LexBuffer<char>.FromTextReader file
        let tokens = [
            while not lexbuf.IsPastEndOfStream do
                let y = Lexer.tokenize lexbuf
                yield y
                //printfn "%A" y
            ] 

        let keywords = [
            for t in tokens do
                match t with
                    | IDENTIFIER(name) -> yield new Word(TokenType.Identifier, name, 1)
                    | KEYWORD(name) -> yield new Word(TokenType.Keyword, name, 1)
                    | LITERAL(name) -> yield new Word(TokenType.Literal, name, 1)
                    | DONTCARE(name) -> yield null
                    | EOF -> yield null
        ]

        //printfn "%A" keywords

        Seq.ofList(keywords)