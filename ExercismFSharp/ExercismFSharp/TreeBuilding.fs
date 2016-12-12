module TreeBuilding

type Record = { RecordId: int; ParentId: int }
type Tree = 
    | Branch of int * Tree list
    | Leaf of int

let buildTree records =
    let validateInput (records : Record list) =
        if List.isEmpty records then failwith "Empty input"

        let root = records.[0]
        if root.ParentId <> -1 || root.RecordId <> 0 then failwith "Root node is invalid"

        records
        |> List.iteri (fun i r ->
            if r.ParentId >= r.RecordId then failwith "Nodes with invalid parent"
            if r.RecordId <> i then failwith "Non-continuous list")
        records

    let map =
        records
        |> List.sortBy (fun x -> x.RecordId)
        |> validateInput
        |> List.map (fun elem -> (elem.ParentId, elem.RecordId))
        |> List.groupBy fst
        |> List.map (fun (x, y) -> (x, List.map snd y))
        |> Map.ofSeq

    let rec helper key =
        match Map.tryFind key map with
        | Some value  -> Branch (key, List.map helper value)
        | None        -> Leaf key

    helper 0