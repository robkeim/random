module NthPrime

open System

let private isPrime n =
    match n with
    | 2 -> true
    | n when n % 2 = 0 -> false
    | _ ->
        let max = int (Math.Ceiling (Math.Sqrt (float n)))
        [3 .. 1 .. max]
        |> Seq.forall (fun value -> not (n % value = 0))

let private primesSeq =
    Seq.initInfinite (fun v -> v + 1)
    |> Seq.filter isPrime
    |> Seq.cache

let nthPrime n =
    primesSeq
    |> Seq.skip n
    |> Seq.head