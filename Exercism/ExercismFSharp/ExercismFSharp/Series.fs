﻿module Series

open System

let slices (input : string) length =
    let inputLength = input |> Seq.length

    match inputLength with
    | _ when length > inputLength -> failwith "length specified is too large"
    | _ ->
        [0 .. (input |> Seq.length) - length]
        |> List.map (fun x ->
            input
            |> Seq.skip x
            |> Seq.take length)
        |> List.map (Seq.map (fun c -> Int32.Parse (c |> string)))