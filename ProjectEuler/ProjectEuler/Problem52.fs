module Problem52

open System

let private sortNum n =
    n
    |> string
    |> Seq.sort
    |> Seq.fold (sprintf "%s%c") String.Empty

let permutedMultiples =
    let result =
        Seq.initInfinite(fun i -> (i, sortNum i))
        |> Seq.tail
        |> Seq.filter (fun (num, seq) -> (sortNum (2 * num)) = seq)
        |> Seq.filter (fun (num, seq) -> (sortNum (3 * num)) = seq)
        |> Seq.filter (fun (num, seq) -> (sortNum (4 * num)) = seq)
        |> Seq.filter (fun (num, seq) -> (sortNum (5 * num)) = seq)
        |> Seq.filter (fun (num, seq) -> (sortNum (6 * num)) = seq)
        |> Seq.head
        |> fst

    Console.WriteLine result
    ()