module Accumulate

let rec private accumulateHelper f input state =
    match input with
    | [] -> state |> List.rev
    | x::xs -> accumulateHelper f xs (f x :: state)

let accumulate f input =
    accumulateHelper f input []