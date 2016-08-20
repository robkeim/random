module WordCount

open System

let wordCount phrase =
    // Remove all characters except letters, digits, and apostrophes
    let normalizeString string =
        string
        |> Seq.map (fun c ->
            match c with
            | _ when Char.IsLetterOrDigit c  -> Char.ToLowerInvariant c
            | ''' -> '''
            | _ -> ' ')
        |> String.Concat

    let filterNonEmptyWords w =
        match w with
        | _ when Seq.length w > 0 -> Some w
        | _ -> None

    let normalizedPhrase =
        phrase |> normalizeString
    
    normalizedPhrase.Split [|' '|]
    |> Seq.map (fun w -> w.Trim ''')
    |> Seq.choose filterNonEmptyWords
    |> Seq.groupBy (fun w -> w)
    |> Seq.map (fun (k, v) -> (k, Seq.length v))
    |> Map.ofSeq