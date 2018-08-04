module Matrix

open System

let fromString (input : string) =
    input.Split [|'\n'|]
    |> Array.map (fun row ->
        row.Split [|' '|]
        |> Array.map Int32.Parse
    )

let rows matrix =
    matrix

let cols matrix =
    let numRows = matrix |> Array.length
    let numCols = matrix |> Array.head |> Array.length

    [|0 .. numCols - 1|]
    |> Array.map (fun j ->
        [|0 .. numRows - 1|]
        |> Array.map (fun i ->
            matrix.[i].[j]
        )
    )