module Sublist

open System
open System.Text

type Result =
    | Equal
    | Sublist
    | Superlist
    | Unequal

let private toString (list : 'a list) =
    let result =
        list
        |> List.fold
            (fun (sb : StringBuilder) s ->
                let tmp = sb.Append(";")
                tmp.Append(s))
            (new StringBuilder())
    result.ToString()

let sublist first second =
    let firstString = toString first
    let secondString = toString second

    let isEqual = firstString = secondString
    let isSublist = firstString.Length < secondString.Length && secondString.Contains firstString
    let isSuperlist = firstString.Length > secondString.Length && firstString.Contains secondString

    match isEqual, isSublist, isSuperlist with
    | true, _, _  -> Equal
    | _, true, _  -> Sublist
    | _, _, true  -> Superlist
    | _           -> Unequal