module SaddlePoints

let saddlePoints (matrix : int list list) =
    let isValidRow value row =
        matrix.[row]
        |> Seq.forall (fun n -> value >= n)
    
    let isValidCol value col =
        matrix
        |> Seq.mapi(fun row _ -> matrix.[row].[col])
        |> Seq.forall(fun n -> value <= n)

    matrix
    |> List.mapi (fun i _ ->
        matrix.[i]
        |> List.mapi (fun j _ ->
            let value = matrix.[i].[j]

            match ((isValidRow value i) && (isValidCol value j)) with
            | true -> Some (i, j)
            | _ -> None
        )
        |> List.choose id
    )
    |> List.collect id