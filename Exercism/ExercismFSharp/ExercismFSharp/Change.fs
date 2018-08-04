module Change

open System

let change value coins =
    let map =
        [1..value]
        |> List.fold
            (fun (map : Map<int, (int * int list)>) i -> map.Add(i, (Int32.MaxValue, [])))
            ((Map.empty).Add(0, (0, [])))

    let result =
        [1..value]
        |> List.fold (fun map' i ->
            coins
            |> List.filter (fun c -> c <= i)
            |> List.fold (fun map'' c ->
                let curVal = map'' |> Map.find i |> fst
                let (curMinusCoinVal, curMinusCoin) = map'' |> Map.find (i - c)

                match curMinusCoinVal <> Int32.MaxValue && curMinusCoinVal + 1 < curVal with
                | true  -> map''.Add(i, (curMinusCoinVal + 1, c :: curMinusCoin))
                | false -> map''
                )
                map'
            )
            map
        |> Map.tryFind value

    match result with
    | None -> None
    | Some (numCoins, coins) ->
        match numCoins = Int32.MaxValue with
        | true  -> None
        | false -> Some coins