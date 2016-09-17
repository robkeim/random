module ParallelLetterFrequency

open System

let private countFrequencies (text : string) =
    text.ToLowerInvariant()
    |> Seq.filter Char.IsLetter
    |> Seq.countBy id
    |> Map.ofSeq

let mergeMap (map : Map<char, int>) =
    map
    |> Map.fold (fun acc k v ->
        match Map.tryFind k acc with
        | Some v' -> acc.Add(k, v + v')
        | None -> acc.Add(k, v)
    )

let frequency input =
    input
    |> Seq.map (fun text -> async { return countFrequencies text })
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Array.fold mergeMap Map.empty