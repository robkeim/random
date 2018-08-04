module SecretHandshake

open System

type private HandshakeMovement =
    | Wink              = 1
    | DoubleBlink       = 2
    | CloseYourEyes     = 4
    | Jump              = 8
    | ReverseOperations = 16

let handshake num =
    let isIncludedMovement mov =
        num &&& int mov <> 0

    let stringMovement mov =
        match mov with
        | HandshakeMovement.Wink          -> "wink"
        | HandshakeMovement.DoubleBlink   -> "double blink"
        | HandshakeMovement.CloseYourEyes -> "close your eyes"
        | HandshakeMovement.Jump          -> "jump"
        | _                               -> failwith "no string representation for reverse"

    let movements =
        Enum.GetValues(typeof<HandshakeMovement>)
        |> Seq.cast<HandshakeMovement>
        |> Seq.filter (fun x -> x <> HandshakeMovement.ReverseOperations)
        |> Seq.filter isIncludedMovement
        |> Seq.map stringMovement
        |> Seq.toList

    if isIncludedMovement HandshakeMovement.ReverseOperations then
        movements |> List.rev
    else
        movements