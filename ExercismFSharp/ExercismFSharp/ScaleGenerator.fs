module ScaleGenerator

open System

let private notes =
    [
        (0,  "A" , "A" )
        (1,  "A#", "Bb")
        (2,  "B" , "B" )
        (3,  "C" , "C" )
        (4,  "C#", "Db")
        (5,  "D" , "D" )
        (6,  "D#", "Eb")
        (7,  "E" , "E" )
        (8,  "F" , "F" )
        (9,  "F#", "Gb")
        (10, "G" , "G" )
        (11, "G#", "Ab")
    ]

let private normalizeTonic (tonic : string) =
    let tmp = tonic.ToUpperInvariant()
    match tmp.EndsWith "B" with
    | true  -> sprintf "%cb" tmp.[0]
    | false -> tmp

let private getIndex index interval =
    (index + interval) % 12

let private isSharp tonic =
    [ "G"; "D"; "C"; "A"; "E"; "B"; "F#"; "e"; "b"; "a"; "f#"; "g#"; "d#" ]
    |> List.contains tonic

let pitches tonic intervals =
    let normalizedTonic = normalizeTonic tonic
    let (tonicIndex, sharp, flat) =
        notes |>
        List.find (fun (_, sharp, flat) -> normalizedTonic = sharp || normalizedTonic = flat)
    
    intervals
    |> Seq.fold (fun (result, index) interval ->
        let nextIndex =
            match interval with
            | 'A' -> getIndex index 3
            | 'M' -> getIndex index 2
            | 'm' -> getIndex index 1
            | _   -> raise (Exception "unknown interval")

        let (_, sharp, flat) = notes.[nextIndex]
        match isSharp tonic with
        | true  -> (sharp :: result, nextIndex)
        | false -> (flat ::  result, nextIndex)
        )
        (match isSharp tonic with
        | true  -> ([sharp], tonicIndex)
        | false -> ([flat],  tonicIndex)
    )
    |> fst
    |> List.tail
    |> List.rev