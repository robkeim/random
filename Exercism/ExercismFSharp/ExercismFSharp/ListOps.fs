module ListOps

// Length
let rec private lengthHelper list state =
    match list with
    | [] -> state
    | _::xs -> lengthHelper xs (state + 1)

let length list =
    lengthHelper list 0

// Reverse
let rec private reverseHelper list state =
    match list with
    | [] -> state
    | x::xs -> reverseHelper xs (x::state)

let reverse list =
    reverseHelper list []

// Map
let rec private mapHelper f list state =
    match list with
    | [] -> state |> reverse
    | x::xs -> mapHelper f xs ((f x)::state)

let map f list =
    mapHelper f list []

// Filter
let rec private filterHelper pred list state =
    match list with
    | [] -> state |> reverse
    | x::xs -> match pred x with
                | true -> filterHelper pred xs (x::state)
                | false -> filterHelper pred xs state

let filter pred list =
    filterHelper pred list []

// Fold L
let rec private foldlHelper f state list =
    match list with
    | [] -> state
    | x::xs -> foldlHelper f (f state x) xs

let foldl f state list =
    foldlHelper f state list

// Fold R
let rec private foldrHelper f state list =
    match list with
    | [] -> state
    | x::xs -> foldrHelper f (f x state) xs

let foldr f state list =
    foldrHelper f state (reverse list)

// Append
let append list1 list2 =
    foldr (fun item acc -> item :: acc) list2 list1

// Concat
let concat listOfLists =
    foldr append [] listOfLists