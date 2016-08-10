module SumOfMultiples

let sumOfMultiples (multiples : int list) (number : int) =
    let isDivisble num divisor =
        num % divisor = 0

    let isDivisible num divisors =
        divisors
        |> List.exists (fun divisor -> isDivisble num divisor) 
    
    [1..number - 1]
    |> List.filter (fun n -> isDivisible n multiples)
    |> List.sum
