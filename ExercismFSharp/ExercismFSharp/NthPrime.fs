module NthPrime

open System

let nthPrime n =
    let isPrime n =
        match n with
        | 2 -> true
        | n when n % 2 = 0 -> false
        | _ ->
            let max = int (Math.Ceiling (Math.Sqrt (float n)))
            [3 .. 1 .. max]
            |> Seq.forall (fun value -> not (n % value = 0))
    
    Seq.initInfinite (fun v -> v + 1)
    |> Seq.filter isPrime
    |> Seq.skip n
    |> Seq.head