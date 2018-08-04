module BinarySearchTree

open System

type BinarySearchTreeNode<'a> =
    {
        datum : 'a
        left : Option<BinarySearchTreeNode<'a>>
        right : Option<BinarySearchTreeNode<'a>>
    }

let singleton datum =
    { datum = datum; left = None; right = None }

let value tree =
    tree.datum

let rec insert datum tree =
    match tree.datum with
    | d when datum <= d ->
                            match tree.left with
                            | Some n -> { tree with left = Some (insert datum n) }
                            | None -> { tree with left = Some { datum = datum; left = None; right = None } }
    | _ ->
                            match tree.right with
                            | Some n -> { tree with right = Some (insert datum n) }
                            | None -> { tree with right = Some { datum = datum; left = None; right = None } }

let left tree =
    tree.left

let right tree =
    tree.right

let fromList list =
    match list with
    | [] -> raise (Exception "invalid operation on empty list")
    | [x] -> singleton x
    | x::xs -> Seq.fold (fun acc elem -> insert elem acc) (singleton x) xs

let rec toList tree =
    match (tree.left, tree.right) with
    | (Some l, Some r) -> List.append (toList l) (List.append (List.singleton tree.datum) (toList r))
    | (Some l, None) -> List.append (toList l) (List.singleton tree.datum)
    | (None, Some r) -> List.append (List.singleton tree.datum) (toList r)
    | (None, None) -> [tree.datum]