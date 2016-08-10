module DifferenceOfSquares

let squareOfSums (num : int) =
    let sum = [1..num] |> List.sum
    sum * sum

let sumOfSquares (num : int) =
    [1..num]
    |> List.map(fun n -> n * n)
    |> List.sum

let difference (num : int) =
    (squareOfSums num) - (sumOfSquares num)