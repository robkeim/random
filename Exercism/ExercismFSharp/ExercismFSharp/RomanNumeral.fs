module RomanNumeral

open System

let toRoman num =
    let thousands =
        match num / 1000 with
        | 0 -> ""
        | 1 -> "M"
        | 2 -> "MM"
        | 3 -> "MMM"
        | _ -> raise (ArgumentException "Invalid number")

    let hundreds =
        match (num % 1000) / 100 with
        | 0 -> ""
        | 1 -> "C"
        | 2 -> "CC"
        | 3 -> "CCC"
        | 4 -> "CD"
        | 5 -> "D"
        | 6 -> "DC"
        | 7 -> "DCC"
        | 8 -> "DCCC"
        | 9 -> "CM"
        | _ -> raise (Exception "unreachable code")

    let tens =
        match (num % 100) / 10 with
        | 0 -> ""
        | 1 -> "X"
        | 2 -> "XX"
        | 3 -> "XXX"
        | 4 -> "XL"
        | 5 -> "L"
        | 6 -> "LX"
        | 7 -> "LXX"
        | 8 -> "LXX"
        | 9 -> "XC"
        | _ -> raise (Exception "unreachable code")

    let ones =
        match num % 10 with
        | 0 -> ""
        | 1 -> "I"
        | 2 -> "II"
        | 3 -> "III"
        | 4 -> "IV"
        | 5 -> "V"
        | 6 -> "VI"
        | 7 -> "VII"
        | 8 -> "VIII"
        | 9 -> "IX"
        | _ -> raise (Exception "unreachable code")
    
    sprintf "%s%s%s%s" thousands hundreds tens ones
