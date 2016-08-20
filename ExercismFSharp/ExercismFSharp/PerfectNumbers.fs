module PerfectNumbers

open System

type NumberType =
    | Perfect
    | Abundant
    | Deficient

let classify num =
    let factorSum =
        [1 .. num - 1]
        |> Seq.filter (fun n -> num % n = 0)
        |> Seq.sum

    let diff = factorSum - num

    match diff with
    | _ when diff < 0 -> NumberType.Deficient
    | 0 -> NumberType.Perfect
    | _ when diff > 0 -> NumberType.Abundant
    | _ -> raise (Exception "unreachable code")