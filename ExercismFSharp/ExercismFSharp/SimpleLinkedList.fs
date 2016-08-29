module SimpleLinkedList

open System

type Node<'a> =
    {
        datum : 'a
        next : Option<Node<'a>>
    }

type LinkedList<'a> =
    {
        head : Option<Node<'a>>
    }

let nil =
    { head = None }

let isNil list =
    match list.head with
    | Some _ -> false
    | None -> true

let create datum list =
    { head = Some { datum = datum; next = list.head } }

let datum list =
    match list.head with
    | Some node -> node.datum
    | None -> raise (Exception "list in empty")

let next list =
    match list.head with
    | Some node -> { head = node.next }
    | None -> nil

let rec private toListHelper node state =
    let newState = node.datum :: state
    match node.next with
    | Some nextNode -> toListHelper nextNode newState
    | None -> newState

let toList list =
    match list.head with
    | Some node -> (toListHelper node []) |> List.rev
    | None -> []

let fromList list =
    list
    |> List.rev
    |> List.fold (fun acc elem -> create elem acc)
        nil

let reverse list =
    list
    |> toList
    |> List.rev
    |> fromList