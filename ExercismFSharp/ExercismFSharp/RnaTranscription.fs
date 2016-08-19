module RNATranscription

open System

let toRna dna =
    dna
    |> Seq.map (fun c ->
        match c with
        | 'G' -> 'C'
        | 'C' -> 'G'
        | 'T' -> 'A'
        | 'A' -> 'U'
        | _ -> raise (ArgumentException "Invalid DNA character"))