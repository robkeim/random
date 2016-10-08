module Seq

let rec private keepHelper pred input state =
    match input with
    | []    -> state |> List.rev
    | x::xs -> if pred x then
                   keepHelper pred xs (x :: state)
               else
                   keepHelper pred xs state

let keep pred input =
    keepHelper pred (input |> Seq.toList) List.empty

let rec private discardHelper pred input state =
    match input with
    | []    -> state |> List.rev
    | x::xs -> if pred x then
                   discardHelper pred xs state
               else
                   discardHelper pred xs (x :: state)

let discard pred input =
    discardHelper pred (input |> Seq.toList) List.empty