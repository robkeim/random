module Problem24

open System

// Generating the permutations came from here:
// http://stackoverflow.com/questions/1526046/f-permutations/4610704
let rec private distribute e =
    function
    | [] -> [[e]]
    | x::xs' as xs -> (e::xs)::[for xs in distribute e xs' -> x::xs]

let rec private permute =
    function
    | [] -> [[]]
    | e::xs -> List.collect (distribute e) (permute xs)

let millionthPermutation =
    let result =
        permute ("0123456789" |> Seq.toList)
        |> Seq.sort
        |> Seq.skip (1000000 - 1)
        |> Seq.head

    result
    |> Seq.iter Console.WriteLine
    ()