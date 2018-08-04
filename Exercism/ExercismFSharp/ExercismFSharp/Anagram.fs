module Anagram

open System

let anagrams (candidates : string list) word =
    let toLower (word : string) =
        word.ToLowerInvariant()

    let order word =
        word
        |> toLower
        |> Seq.sort
        |> Seq.toArray
        |> String

    let orderedWord = word |> order

    candidates
    |> List.filter (fun w ->
        order w = orderedWord && toLower w <> toLower word)