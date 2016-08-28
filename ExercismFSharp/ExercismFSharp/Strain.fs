module Seq

let rec private keepHelper pred input state =
    match input with
    | [] -> state |> List.rev
    | x::xs -> match pred x with
                | true -> keepHelper pred xs (x :: state)
                | false -> keepHelper pred xs state

let keep pred input =
    keepHelper pred (input |> Seq.toList) List.empty

let rec private discardHelper pred input state =
    match input with
    | [] -> state |> List.rev
    | x::xs -> match pred x with
                | false -> discardHelper pred xs (x :: state)
                | true -> discardHelper pred xs state

let discard pred input =
    discardHelper pred (input |> Seq.toList) List.empty