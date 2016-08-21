module DifferenceOfSquares

let squareOfSums num =
    let sum = [1..num] |> List.sum
    sum * sum

let sumOfSquares num =
    [1..num]
    |> List.sumBy (fun n -> n * n)

let difference num =
    (squareOfSums num) - (sumOfSquares num)