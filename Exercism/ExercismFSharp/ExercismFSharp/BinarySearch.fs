module BinarySearch

let rec private binarySearchHelper array value index =
    match array with
    | [] -> None
    | _ ->
            let middleIndex = Seq.length array / 2
            let middleElement = array.[middleIndex]

            match middleElement with
            | _ when middleElement < value -> binarySearchHelper array.[middleIndex + 1 ..] value (index + middleIndex + 1)
            | _ when middleElement = value -> Some (index + middleIndex)
            | _ when middleElement > value -> binarySearchHelper array.[0 .. middleIndex - 1] value index
            | _                            -> failwith "unreachable code"

let binarySearch array value =
    binarySearchHelper array value 0