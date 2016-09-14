module Diamond

open System

let private getLetter index =
    (65 + index) |> char

let private toString charSeq =
    charSeq
    |> Seq.fold (sprintf "%s%c") String.Empty

let private getSpaces n =
    match n with
    | 0 -> ""
    | _ ->
        [0 .. n - 1]
        |> List.fold (fun acc _ -> sprintf "%s " acc) String.Empty
        |> toString

let private getFirstRow letterIndex =
    Seq.init
        (letterIndex * 2 + 1)
        (fun i ->
            match i = letterIndex with
            | true  -> 'A'
            | false -> ' '
        )
    |> toString

let private getRow numLetters letterIndex =
    let letter = getLetter letterIndex
    let leadingAndTrailingSpace = getSpaces (numLetters - letterIndex - 1)
    let innerSpace = getSpaces (letterIndex * 2 - 1)

    sprintf "%s%c%s%c%s"
        leadingAndTrailingSpace
        letter
        innerSpace
        letter
        leadingAndTrailingSpace

let make letter =
    let numLetters = (int)letter - 65 + 1

    match letter with
    | 'A' -> getFirstRow 0
    | _ ->
        let firstRow = getFirstRow (numLetters - 1) |> List.singleton
        let rows = [1 .. numLetters - 1] |> List.map (getRow numLetters)
        let top = firstRow @ rows
        let bottom = top |> List.rev |> List.tail
        top @ bottom
        |> List.reduce (sprintf "%s\n%s")