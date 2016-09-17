module Deque

type Node<'a> =
    {
        mutable prev : Node<'a> option
        mutable next : Node<'a> option
        value : 'a
    }

type Deque<'a> =
    {
        head : Node<'a> option
        tail : Node<'a> option
    }

let mkDeque =
    { head = None; tail = None }

// Insert value at back
let push value deque =
    match deque.tail with
    | Some tail ->
        let node = Some { prev = Some tail; next = None; value = value }
        tail.next <- node
        { head = deque.head; tail = node }
    | None ->
        let node = Some { prev = None; next = None; value = value }
        { head = node; tail = node }

// Remove value at back
let pop deque =
    (deque.tail.Value.value, { head = deque.head; tail = deque.tail.Value.prev })

// Remove value at front
let shift deque =
    (deque.head.Value.value, { head = deque.head.Value.next; tail = deque.tail })

// Insert value at front
let unshift value deque =
    match deque.head with
    | Some head ->
        let node = Some { prev = None; next = Some head; value = value }
        head.prev <- node
        { head = node; tail = deque.tail }
    | None ->
        let node = Some { prev = None; next = None; value = value }
        { head = node; tail = node }