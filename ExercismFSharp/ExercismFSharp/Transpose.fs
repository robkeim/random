module Transpose

open System

let private normalizeInput (input : string) =
    let split = input.Split [| '\n' |]
    let length =
        split
        |> Array.map Seq.length
        |> Array.max

    split
    |> Array.map (fun s -> s.PadRight length)

let transpose input =
    match input with
    | "" -> ""
    | _ ->
        let result =
            input
            |> normalizeInput
            |> Array.mapi (fun i line ->
                line
                |> Seq.mapi (fun j c -> ((j, i), c))
            )
            |> Seq.concat
            |> Seq.groupBy (fun item -> item |> fst |> fst)
            |> Seq.map snd
            |> Seq.map (Seq.map snd)
            |> Seq.map (fun line ->
                line
                |> Seq.fold (sprintf "%s%c") String.Empty
            )
            |> Seq.reduce (sprintf "%s\n%s")

        result.TrimEnd [| ' ' |]