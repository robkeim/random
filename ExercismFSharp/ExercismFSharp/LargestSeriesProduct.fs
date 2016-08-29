module LargestSeriesProduct

open System

let private toInt char =
    char
    |> string
    |> Int32.Parse

let largestProduct (digits : string) seriesLength =
    match seriesLength with
    | 0 -> 1
    | _ ->
            digits
            |> Seq.windowed seriesLength
            |> Seq.map (Seq.fold (fun acc elem -> acc * (toInt elem)) 1)
            |> Seq.sort
            |> Seq.max