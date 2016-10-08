module RNATranscription

let toRna dna =
    dna
    |> Seq.map (function
        | 'G' -> 'C'
        | 'C' -> 'G'
        | 'T' -> 'A'
        | 'A' -> 'U'
        | _   -> failwith "Invalid DNA character")