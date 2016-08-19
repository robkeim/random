module RNATranscription

open System

let toRna dna =
    dna
    |> Seq.map (function
        | 'G' -> 'C'
        | 'C' -> 'G'
        | 'T' -> 'A'
        | 'A' -> 'U'
        | _ -> raise (ArgumentException "Invalid DNA character"))