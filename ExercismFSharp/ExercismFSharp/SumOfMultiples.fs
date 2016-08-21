module SumOfMultiples

let sumOfMultiples multiples number =
    let isDivisible divisors num =
        divisors
        |> List.exists (fun divisor -> num % divisor = 0) 
    
    let isDivisibleOfMultiples = isDivisible multiples

    [1..number - 1]
    |> List.filter isDivisibleOfMultiples
    |> List.sum
