module BracketPush

open System

let private matchClosingBracket closingBracket stack =
    let bracket =
        match closingBracket with
        | ')' -> '('
        | '}' -> '{'
        | ']' -> '['
        | _ -> raise (Exception "unreachable code")

    match stack with
    | [] -> false
    | x::xs when x = bracket -> true
    | _ -> false

let rec private matchedHelper input brackets =
    match input with
    | [] -> Seq.length brackets = 0
    | x::xs ->
        match x with
        | '(' | '{' | '[' -> matchedHelper xs (x :: brackets)
        | ')' | '}' | ']' ->
            match matchClosingBracket x brackets with
            | true  -> matchedHelper xs brackets.[1..]
            | false -> false
        | _ -> matchedHelper xs brackets

let matched input =
    matchedHelper (Seq.toList input) []