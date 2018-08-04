module OcrNumbers

open System

let private parseNumber number =
    let zero =
        " _ " +
        "| |" +
        "|_|" +
        "   "

    let one =
        "   " +
        "  |" +
        "  |" +
        "   "

    let two =
        " _ " +
        " _|" +
        "|_ " +
        "   "

    let three =
        " _ " +
        " _|" +
        " _|" +
        "   "

    let four =
        "   " +
        "|_|" +
        "  |" +
        "   "

    let five =
        " _ " +
        "|_ " +
        " _|" +
        "   "

    let six =
        " _ " +
        "|_ " +
        "|_|" +
        "   "

    let seven =
        " _ " +
        "  |" +
        "  |" +
        "   "

    let eight =
        " _ " +
        "|_|" +
        "|_|" +
        "   "

    let nine =
        " _ " +
        "|_|" +
        " _|" +
        "   "

    match number with
    | _ when number = zero  -> '0'
    | _ when number = one   -> '1'
    | _ when number = two   -> '2'
    | _ when number = three -> '3'
    | _ when number = four  -> '4'
    | _ when number = five  -> '5'
    | _ when number = six   -> '6'
    | _ when number = seven -> '7'
    | _ when number = eight -> '8'
    | _ when number = nine  -> '9'
    | _ -> '?'

let private breakLineIntoDigits (input : string[]) =
    let chunkLine line =
        line
        |> Seq.chunkBySize 3
        |> Seq.map (Seq.fold (sprintf "%s%c") String.Empty)

    Seq.map3
        (sprintf "%s%s%s   ")
        (chunkLine input.[0])
        (chunkLine input.[1])
        (chunkLine input.[2])

let private breakInputIntoDigits (input : string) =
    input.Split [| '\n' |]
    |> Array.chunkBySize 4
    |> Seq.map breakLineIntoDigits

let convert input =
    breakInputIntoDigits input
    |> Seq.map (Seq.map parseNumber)
    |> Seq.map (Seq.fold (sprintf "%s%c") String.Empty)
    |> Seq.reduce (sprintf "%s,%s")