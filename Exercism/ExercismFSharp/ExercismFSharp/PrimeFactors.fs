module PrimeFactors

open System

// Adapted from NthPrime exercise
let private isPrime n =
    match n with
    | 0 | 1 -> false
    | 2 -> true
    | n when n % 2 = 0 -> false
    | _ ->
        let max = int (Math.Ceiling (Math.Sqrt (float n)))
        [3 .. 1 .. max]
        |> Seq.forall (fun value -> n % value <> 0)

let private primeSeq =
    Seq.initInfinite id
    |> Seq.filter isPrime
    |> Seq.cache

let private getSmallestPrimeFactor n =
    primeSeq
    |> Seq.filter (fun value -> n % value = 0)
    |> Seq.head

let rec private primeFactorsForHelper input factors =
    match input with
    | _ when input < 2 -> factors
    | _ ->  let smallestPrime = getSmallestPrimeFactor input
            match smallestPrime = input with
            | true -> smallestPrime::factors
            | false -> primeFactorsForHelper (input / smallestPrime) (smallestPrime::factors)

let primeFactorsFor (input : int64) =
    primeFactorsForHelper (int input) []
    |> Seq.sort
    |> Seq.toList