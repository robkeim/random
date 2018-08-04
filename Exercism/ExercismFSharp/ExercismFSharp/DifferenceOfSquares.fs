module DifferenceOfSquares

let private square n =
    n * n

let squareOfSums num =
    [1..num]
    |> List.sum
    |> square

let sumOfSquares num =
    [1..num]
    |> List.sumBy square

let difference num =
    (squareOfSums num) - (sumOfSquares num)