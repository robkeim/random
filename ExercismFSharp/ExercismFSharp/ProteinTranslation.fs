module ProteinTranslation

open System

let private toProtein =
    function
    | "AUG"                         -> "Methionine"
    | "UUU" | "UUC"                 -> "Phenylalanine"
    | "UUA" | "UUG"                 -> "Leucine"
    | "UCU" | "UCC" | "UCA" | "UCG" -> "Serine"
    | "UAU" | "UAC"                 -> "Tyrosine"
    | "UGU" | "UGC"                 -> "Cysteine"
    | "UGG"                         -> "Tryptophan"
    | "UAA" | "UAG" | "UGA"         -> "STOP"
    | _                             -> failwith "Invalid protein"

let translate codon =
    codon
    |> Seq.chunkBySize 3
    |> Seq.map String
    |> Seq.map toProtein
    |> Seq.takeWhile (fun protein -> protein <> "STOP")
    |> Seq.toList