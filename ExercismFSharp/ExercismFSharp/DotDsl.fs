module DotDsl

type Attribute = string * string

type Element =
    | Attribute of Attribute
    | Node of string * Attribute list
    | Edge of string * string * Attribute list

type Graph = { elements: Element list }

let graph input =
    { elements = input |> List.sort }

let node id attributes =
    Node(id, attributes)

let edge source destination attributes =
    Edge(source, destination, attributes)

let attr id value =
    Attribute((id, value))

let nodes graph =
    graph.elements
    |> List.choose (fun e ->
        match e with
        | Node _ -> Some e
        | _ -> None)

let edges graph =
    graph.elements
    |> List.choose (fun e ->
        match e with
        | Edge _ -> Some e
        | _ -> None)

let attrs graph =
    graph.elements
    |> List.choose (fun e ->
        match e with
        | Attribute _ -> Some e
        | _ -> None)