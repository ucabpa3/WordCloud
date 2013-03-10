module Statements

open System

type LiteralToken = 
    | LITERALTOKEN of string
    | EO

type CommentToken =
    | COMMENTTOKEN of string
    | EOL 

type Token =
    | IDENTIFIER of string
    | KEYWORD of string
    | LITERAL of string
    | COMMENT of string
    | DONTCARE of string
    | EOF
