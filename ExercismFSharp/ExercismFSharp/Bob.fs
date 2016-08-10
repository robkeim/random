module Bob

open System

let hey (phrase : string) =
    let isUpperCaseLetter letter =
        Char.IsLetter letter && Char.IsUpper letter

    let areAllLettersUpperCase string =
        string
        |> Seq.filter Char.IsLetter
        |> Seq.forall isUpperCaseLetter

    let hasUpperCaseLetter string =
        string
        |> Seq.exists (fun n -> isUpperCaseLetter n)

    let isQuestion phrase =
        phrase |> Seq.tryLast = Some '?'

    let isYelling phrase =
         hasUpperCaseLetter phrase && areAllLettersUpperCase phrase

    let isNothing phrase =
        (Seq.isEmpty phrase) || (Seq.forall Char.IsWhiteSpace phrase)

    match phrase with
    | _ when isYelling phrase -> "Whoa, chill out!"
    | _ when isQuestion phrase -> "Sure."
    | _ when isNothing phrase -> "Fine. Be that way!"
    | _ -> "Whatever."