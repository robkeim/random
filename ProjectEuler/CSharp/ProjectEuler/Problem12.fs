module Problem12

open System

let triangleNumber =
    let triangleNumbers =  
        Seq.initInfinite (fun i ->
            i * (i + 1) / 2
            )
        |> Seq.cache

    let countFactors n =
        let sqrt = Math.Sqrt (n |> float) |> int
        
        let numFactors =
            [1 .. sqrt]
            |> Seq.filter (fun i -> n % i = 0)
            |> Seq.length

        match sqrt * sqrt = n with
        | true  -> 2 * numFactors - 1
        | false -> 2 * numFactors

    let result =
        triangleNumbers
        |> Seq.filter (fun i -> countFactors i > 500)
        |> Seq.head

    Console.WriteLine result
    ()