module Problem30

open System

let private fifthPowerForDigit d =
    match d with
    | '0' -> 0
    | '1' -> 1
    | '2' -> 32
    | '3' -> 243
    | '4' -> 1024
    | '5' -> 3125
    | '6' -> 7776
    | '7' -> 16807
    | '8' -> 32768
    | '9' -> 59049
    | _ -> raise (Exception "invalid digit")

let private isSumOfDigits num =
    let sum =
        num
        |> string
        |> Seq.sumBy fifthPowerForDigit
    sum = num

let digitFifthPowers =
    let result =
        [10..1000000]
        |> List.filter isSumOfDigits
        |> List.sum

    Console.WriteLine result
    ()