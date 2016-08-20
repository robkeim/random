module PhoneNumber

open System

let parsePhoneNumber input =
    let phoneNumber =
        input
        |> Seq.filter Char.IsDigit
        |> Seq.toList

    let length = phoneNumber |> List.length

    let toString phoneNumber =
        phoneNumber
        |> List.fold (sprintf "%s%c") ""

    match phoneNumber with
    | _ when length = 10 -> Some (phoneNumber |> toString)
    | '1'::pn when length = 11 -> Some (pn |> toString)
    | _ -> None