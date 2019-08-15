module Problem36

open System

let private reverse (str : string) =
    str
    |> Seq.rev
    |> Seq.fold (sprintf "%s%c") String.Empty

let private isPalindrome n =
    (n |> Seq.rev |> Seq.head <> '0') && (n = reverse n)

let rec private toBinary outputDigits number =
    match number with
    | 0 -> outputDigits |> List.fold (sprintf "%s%i") String.Empty
    | _ -> toBinary ((number % 2) :: outputDigits) (number / 2)

let doublePalendromes =
    let result =
        [1..999999]
        |> List.filter (fun n -> isPalindrome (n |> string))
        |> List.filter (fun n -> isPalindrome (toBinary [] n))
        |> List.sum

    Console.WriteLine result
    ()