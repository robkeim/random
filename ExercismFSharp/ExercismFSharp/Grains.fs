module Grains

let rec square n =
    match n with
    | 1 -> 1I
    | _ -> 2I * (square (n - 1))

let total =
    [ 1 .. 64 ]
    |> List.map (fun n -> square n)
    |> List.sum