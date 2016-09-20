module Problem31

open System

let rec private countChange value coins =
    match coins, value with
    | _, 0 -> 1
    | [], _ -> 0
    | _, _ when value < 0 -> 0
    | x::xs, _ -> (countChange (value - x) coins) + (countChange value xs)

let numCoins =
    let result = countChange 200 [ 1; 2; 5; 10; 20; 50; 100; 200 ]

    Console.WriteLine result
    ()