module CryptoSquare

open System

let normalizePlaintext input =
    input
    |> Seq.filter Char.IsLetterOrDigit
    |> Seq.map Char.ToLowerInvariant
    |> Seq.fold (sprintf "%s%c") String.Empty

let size input =
    input
    |> normalizePlaintext
    |> Seq.length
    |> float
    |> Math.Sqrt
    |> Math.Ceiling
    |> int

let plaintextSegments input =
    let text = normalizePlaintext input
    let size = size text

    text
    |> Seq.chunkBySize size
    |> Seq.map (fun row -> row |> Array.fold (sprintf "%s%c") String.Empty)
    |> Seq.toList

let private safeArrayAccess (array : string list) i j =
        try
            array.[i].[j] |> string
        with
        | :? System.ArgumentException -> ""
        | :? System.IndexOutOfRangeException -> ""

let ciphertext input =
    let size = size input
    let segments = plaintextSegments input

    Seq.init size (fun i ->
        Seq.init size (fun j ->
            safeArrayAccess segments j i
        )
    )
    |> Seq.map Seq.toList
    |> Seq.map (Seq.reduce (sprintf "%s%s"))
    |> Seq.reduce (sprintf "%s%s")

let normalizeCiphertext input =
    let size = size input
    let ciphertext = ciphertext input

    let totalNumSpaces = size * size - Seq.length ciphertext
    let actualWidth = size - (totalNumSpaces / size)
    let numElementsInFullRows = actualWidth * (size - (totalNumSpaces % size))

    let fullRows =
        ciphertext
        |> Seq.take numElementsInFullRows
        |> Seq.chunkBySize actualWidth
        |> Seq.toList

    let nonFullRows =
        ciphertext
        |> Seq.skip numElementsInFullRows
        |> Seq.chunkBySize (actualWidth - 1)
        |> Seq.toList

    Seq.append
        fullRows
        nonFullRows
    |> Seq.map (fun row -> row |> Array.fold (sprintf "%s%c") String.Empty)
    |> Seq.reduce (sprintf "%s %s")