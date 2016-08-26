module RailFenceCipher

open System

let private getPositions n length =
    Seq.init length (fun index ->
        let offset = ((index - 1) % (n - 1)) + 1
        match index with
        | 0 -> (0, 0)
        | _ -> match ((index - 1) / (n - 1)) % 2 with
                | 0 -> (index, offset)
                | _ -> (index, n - 1 - offset)
    )

let encode n input =
    Seq.zip
        ((getPositions n (Seq.length input)) |> Seq.map snd)
        input
    |> Seq.groupBy fst
    |> Seq.map snd
    |> Seq.map (fun row ->
        row
        |> Seq.map snd
        |> Seq.fold (sprintf "%s%c") String.Empty
    )
    |> Seq.fold (sprintf "%s%s") String.Empty

let decode n input =
    let tmp =
        getPositions n (Seq.length input)
        |> Seq.groupBy snd
        |> Seq.map snd
        |> Seq.concat
        |> Seq.map fst

    Seq.zip
        tmp
        input
    |> Seq.sort
    |> Seq.map snd
    |> Seq.fold (sprintf "%s%c") String.Empty