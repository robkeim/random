module ETL

let transform input =
    let mapStringList key values =
        Seq.map
            (fun (value : string) -> (value.ToLower(), key))
            values

    input
    |> Map.toSeq
    |> Seq.collect (fun (k, v) -> mapStringList k v)
    |> Seq.sort
    |> Map.ofSeq