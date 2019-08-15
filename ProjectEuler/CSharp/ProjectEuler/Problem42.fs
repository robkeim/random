module Problem42

open System

let private words =
    [|"FOO";"BAR"|]

let private triangleNumbers =
    Seq.initInfinite (fun i -> (i + 1) * (i + 2) / 2)
    |> Seq.cache

let private getWordValue (word : string) =
    word
    |> Seq.map (fun c -> (c |> int) - 64)
    |> Seq.fold (+) 0

let numTriangleWords =
    let result =
        words
        |> Array.filter (fun w ->
            let value = getWordValue w
            value = (triangleNumbers |> Seq.skipWhile (fun n -> n < value) |> Seq.head)
        )
        |> Array.length

    Console.WriteLine result
    ()