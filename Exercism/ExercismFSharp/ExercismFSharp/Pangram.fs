module Pangram

open System

let isPangram phrase =
    phrase
    |> Seq.map Char.ToLowerInvariant
    |> Seq.choose (fun c ->
        match c with
        | _ when c >= 'a' && c <= 'z' -> Some c
        | _ -> None)
    |> Seq.distinct
    |> Seq.length = 26