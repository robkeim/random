module WordProblem

open System

let private tryParseInt intString =
    match Int32.TryParse intString with
    | true, value -> Some value
    | _ -> None

let private getNewValue curValue op intString =
    match curValue, (tryParseInt intString) with
    | None, _ -> None
    | _, None -> None
    | Some cv, Some v ->
        match op with
        | "plus"       -> Some (cv + v)
        | "minus"      -> Some (cv - v)
        | "multiplied" -> Some (cv * v)
        | "divided"    -> Some (cv / v)
        | _            -> None

let rec private solveHelper curValue (operations : string list) =
    match operations with
    | [] -> curValue
    | x::y::xs -> solveHelper (getNewValue curValue x y) xs
    | _ -> None

let solve (input : string) =
    let words = "plus" :: (input.Replace("What is ", "").Replace(" by", "").Replace("?", "").Split [| ' ' |] |> Array.toList)
    solveHelper (Some 0) words