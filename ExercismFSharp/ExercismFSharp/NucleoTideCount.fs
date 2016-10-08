module NucleoTideCount

let count nucleotide strand =
    match nucleotide with
    | 'A' | 'T' | 'C' | 'G' -> 
        strand
        |> Seq.filter (fun c -> c = nucleotide)
        |> Seq.length
    | _ -> failwith "unknown nucleotide type"

let nucleotideCounts strand =
    [ 'A'; 'T'; 'C'; 'G']
    |> Seq.map (fun c -> (c, count c strand))
    |> Map.ofSeq