module Acronym

open System

let acronym (input : string) =
    let phrase =
        input.Split [|':'|]
        |> Array.head

    phrase.Split [|' '; '-'|]
    |> Seq.filter (fun word -> not (Seq.length word = 0))
    |> Seq.map (fun word ->
        Seq.append
            (word |> Seq.head |> Char.ToUpperInvariant |> Seq.singleton)
            (word |> Seq.tail |> Seq.filter Char.IsUpper)
    )
    |> Seq.concat
    |> String.Concat