module ProteinTranslation

open System

let private toProtein codon =
    match codon with
    | "AUG" -> "Methionine"
    | "UUU" | "UUC" -> "Phenylalanine"
    | "UUA" | "UUG" -> "Leucine"
    | "UCU" | "UCC" | "UCA" | "UCG" -> "Serine"
    | "UAU" | "UAC" -> "Tyrosine"
    | "UGU" | "UGC" -> "Cysteine"
    | "UGG" -> "Tryptophan"
    | "UAA" | "UAG" | "UGA" -> "STOP"
    | _ -> raise (Exception "invalid protein")

let translate codon =
    codon
    |> Seq.chunkBySize 3
    |> Seq.map (Array.fold (sprintf "%s%c") String.Empty)
    |> Seq.map toProtein
    |> Seq.takeWhile (fun protein -> not (protein = "STOP"))
    |> Seq.toList