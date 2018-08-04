module Bob

open System

let hey (phrase : string) =
    let isNothing =
        String.IsNullOrWhiteSpace phrase

    let isQuestion =
        phrase.EndsWith "?"

    let isYelling =
        Seq.exists Char.IsLetter phrase && phrase = phrase.ToUpperInvariant()

    match phrase with
    | _ when isYelling  -> "Whoa, chill out!"
    | _ when isQuestion -> "Sure."
    | _ when isNothing  -> "Fine. Be that way!"
    | _                 -> "Whatever."