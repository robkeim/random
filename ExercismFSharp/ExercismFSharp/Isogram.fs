module Isogram

open System

let isogram phrase =
    let normalizedPhrase =
        phrase
        |> Seq.filter Char.IsLetter
        |> Seq.map Char.ToLowerInvariant

    normalizedPhrase |> Seq.length = (normalizedPhrase |> Seq.distinct |> Seq.length)