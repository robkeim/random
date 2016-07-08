module Parsers

open FParsec
open Types

// Parser for word
let pWord : Parser<string, unit> =
    manySatisfy ((<>) '\t')

// Parser for gender (either m or f)
let pGender : Parser<Gender, unit> =
    (stringReturn "m" Masculin)
    <|> (stringReturn "f" Feminine)

// Parser for noun with the format word<TAB>gender
let pNoun : Parser<Noun, unit> =
    pipe3
        pWord
        tab
        pGender
        (fun w _ g -> { Word = w; Gender = g })

// Parse for list of nouns separated by newlines
let pInput : Parser<Noun list, unit> =
    sepBy pNoun newline

// Test helper for validing the parsers
let test p str : unit =
    match run p str with
    | Success(result, _, _) -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg