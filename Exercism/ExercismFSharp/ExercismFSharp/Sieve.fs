module Sieve

let rec private listToFilter res =
    function
    | [] -> res
    | x::xs ->
        let filtered =
            xs |> List.filter (fun elem -> elem % x <> 0)
        listToFilter (x :: res) filtered

let primesUpTo num =
    listToFilter [] [2..num]
    |> List.rev