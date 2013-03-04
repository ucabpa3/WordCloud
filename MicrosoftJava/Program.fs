open System
open System.Linq
open Statements

[<EntryPoint>]
let main argv = 

    let file = new System.IO.StreamReader("IApplicationDataContext.cs")


    let lexbuf = Lexing.LexBuffer<char>.FromTextReader file
    let tokens = [
        while not lexbuf.IsPastEndOfStream do
            let y = Lexer.tokenize lexbuf
            yield y
            //printfn "%A" y
        ]

    //printfn "%A" tokens

//    let keywords = [
//        for t in tokens do
//            match t with
//                | IDENTIFIER(name) -> yield new Word(TokenType.Identifier, name, 1)
//                | KEYWORD(name) -> yield new Word(TokenType.Keyword, name, 1)
//                | LITERAL(name) -> yield new Word(TokenType.Literal, name, 1)
//                | DONTCARE(name) -> yield null
//                | EOF -> yield null
//    ]
//
//    printfn "%A" keywords

    //Console.WriteLine("{0}\n", Statements.evaluate y);
    Console.ReadKey() |> ignore

    0 // return an integer exit code
