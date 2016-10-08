module QueenAttack

open System

let canAttack white black =
    match (white : int * int), black with
    | (x1, y1), (x2, y2) when x1 = x2 && y1 = y2                    -> failwith "cannot occupy the same space"
    | (x1, _), (x2, _) when x1 = x2                                 -> true
    | (_, y1), (_, y2) when y1 = y2                                 -> true
    | (x1, y1), (x2, y2) when Math.Abs(x1 - x2) = Math.Abs(y1 - y2) -> true
    | _                                                             -> false