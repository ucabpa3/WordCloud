﻿module Statements

open System

type Token =
    | IDENTIFIER of string
    | KEYWORD of string
    | LITERAL of string
    | COMMENT of string 
    | DONTCARE of string
    | EOF
