module Minesweeper

open System

let private safeArrayAccess (array : char[][]) i j =
        try
            array.[i].[j]
        with
        | :? System.IndexOutOfRangeException -> ' '

let private countSurrounding board i j =
    let tmp =
        [-1..1]
        |> List.map (fun x ->
            [-1..1]
            |> List.map (fun y ->
                match safeArrayAccess board (i + x) (j + y) with
                | '*' -> 1
                | _ -> 0
            )
        )

    let total =
        tmp
        |> List.concat
        |> List.sum

    match total with
    | 0 -> ' '
    | _ when total < 9 -> total |> string |> Seq.head
    | _ -> raise (Exception "Unreachable code")

let annotate (boardString : string) =
    let board =
        boardString.Split [|'\n'|]
        |> Array.map Seq.toArray

    board
    |> Array.mapi (fun i row ->
        row
        |> Array.mapi (fun j col ->
            match col with
            | '*' -> '*'
            | _ -> countSurrounding board i j
        )
        |> String
    )
    |> Array.reduce (sprintf "%s\n%s")