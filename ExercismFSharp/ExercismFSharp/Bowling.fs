module Bowling

open System

type Game =
    { rolls : int list }

let newGame =
    { rolls = [] }

let roll pins game =
     { rolls = pins :: game.rolls }

let rec private scoreHelper rolls score frame =
    match frame with
    | 11 -> score
    | _ ->  match rolls with
            | 10::x::y::xs -> scoreHelper (x::y::xs) (score + 10 + x + y) (frame + 1) // Strike
            | x::y::z::xs when x + y = 10 -> scoreHelper (z::xs) (score + 10 + z) (frame + 1) // Spare
            | x::y::xs -> scoreHelper xs (score + x + y) (frame + 1) // Open frame
            | _ -> raise (Exception "unreachable code")

let score game =
    scoreHelper (game.rolls |> List.rev) 0 1