module Luhn

open System

let private toSeq num =
    num
    |> string
    |> Seq.map (fun c ->
        c
        |> string
        |> Int32.Parse
    )

let checkDigit num =
    num
    |> toSeq
    |> Seq.last

let private doubleDigit d =
    let result = d * 2
    match result with
    | _ when result < 10 -> result
    | _ -> result - 9

let addends num =
    num
    |> toSeq
    |> Seq.rev
    |> Seq.mapi (fun index digit ->
        match index % 2 = 0 with
        | true -> digit
        | false -> doubleDigit digit
    )
    |> Seq.rev
    |> Seq.toList

let checksum num =
    let result =
        num
        |> addends
        |> List.sum

    result % 10

let valid num =
    checksum num = 0

let create (num : int64) =
    let checkDigit =
        [0L..9L]
        |> List.choose (fun d ->
            let newNum = num * 10L + d
            match (valid newNum) with
            | true -> Some d
            | false -> None
        )
        |> List.head

    sprintf "%i%i" num checkDigit
    |> Int64.Parse