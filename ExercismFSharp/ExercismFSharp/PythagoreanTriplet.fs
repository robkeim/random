module PythagoreanTriplet

let triplet x y z =
    (x, y, z)

let isPythagorean triplet =
    let x, y, z = triplet

    let a, b, c =
        match triplet with
        | _ when x >= y && x >= z -> (y, z, x)
        | _ when y >= x && y >= z -> (x, z, y)
        | _ -> (x, y, z)

    a*a + b*b = c*c

let pythagoreanTriplets lower upper =
    [lower..upper]
    |> List.map (fun c -> [lower..c] |> List.map (fun b -> [lower..b] |> List.map (fun a -> (a, b, c))))
    |> List.concat
    |> List.concat
    |> List.filter isPythagorean
    |> List.sort