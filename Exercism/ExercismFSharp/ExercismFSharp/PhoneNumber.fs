module PhoneNumber

open System

let parsePhoneNumber input =
    let phoneNumber =
        input
        |> Seq.filter Char.IsDigit
        |> Seq.toList

    let length = phoneNumber |> List.length

    match phoneNumber with
    | _ when length = 10       -> Some (phoneNumber |> String.Concat)
    | '1'::pn when length = 11 -> Some (pn |> String.Concat)
    | _                        -> None