module Raindrops

// This problem seems to be a slightly more complex version of FizzBuzz
// https://en.wikipedia.org/wiki/Fizz_buzz

// This probably isn't the easiest to read solution, but it's a fun use of pattern matching :)
let convert num =
    match num % 3, num % 5, num % 7 with
    | 0, 0, 0 -> "PlingPlangPlong"

    | 0, 0, _ -> "PlingPlang"
    | 0, _, 0 -> "PlingPlong"
    | _, 0, 0 -> "PlangPlong"
    
    | 0, _, _ -> "Pling"
    | _, 0, _ -> "Plang"
    | _, _, 0 -> "Plong"

    | _, _, _ -> num |> sprintf "%A"