module PascalsTriangle

let private factorialSeq =
    let rec factorial n =
        match n with
        | _ when n <= 0I -> 1I
        | _ -> n * factorial (n - 1I)

    Seq.initInfinite (fun v -> v |> bigint |> factorial)
    |> Seq.cache

let factorial n =
    factorialSeq |> Seq.skip (n |> int) |> Seq.head

let nCk n k =
    (factorial n) / ((factorial k) * (factorial (n - k)))

let triangle (height : int) =
    let row n =
        [0I..n]
        |> List.map (nCk n)

    [0I .. (height |> bigint) - 1I]
    |> List.map row
    |> List.map (List.map int)