module DifferenceOfSquares

let squareOfSums num =
    let sum = [1..num] |> List.sum
    sum * sum

let sumOfSquares num =
    [1..num]
    |> List.map (fun n -> n * n)
    |> List.sum

let difference num =
    (squareOfSums num) - (sumOfSquares num)