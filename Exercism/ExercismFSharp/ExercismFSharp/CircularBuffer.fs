module CircularBuffer

open System

type CircularBuffer<'a> =
    {
        startIndex : int
        numItems : int
        capacity : int
        list : 'a option list
    }

let private replaceElementInList index value list =
    let length = list |> List.length
    let first = list.[0 .. index - 1]
    let elem = Some value |> List.singleton
    let last = list.[index + 1 .. length - 1]

    first @ elem @ last

let mkCircularBuffer size =
    {
        startIndex = 0
        numItems = 0
        capacity = size
        list = Array.create size None |> Array.toList
    }

let read buffer =
    match buffer.numItems with
    | 0 -> raise (InvalidOperationException "The buffer is empty")
    | _ ->
        let elementIndex = buffer.startIndex % buffer.capacity
        let element = buffer.list.[elementIndex].Value
        let newBuffer =
            {
                buffer with
                    startIndex = (buffer.startIndex + 1) % buffer.capacity
                    numItems = buffer.numItems - 1
            }
        (element, newBuffer)

let forceWrite value buffer =
    let insertIndex = (buffer.startIndex + buffer.numItems) % buffer.capacity

    match buffer.numItems = buffer.capacity with
    | true ->
        {
            buffer with
                startIndex = buffer.startIndex + 1
                list = replaceElementInList insertIndex value buffer.list
        }
    | false ->
        {
            buffer with
                numItems = buffer.numItems + 1
                list = replaceElementInList insertIndex value buffer.list
        }

let write value buffer =
    match buffer.numItems = buffer.capacity with
    | true -> raise (InvalidOperationException "Cannot insert into a full buffer")
    | false -> forceWrite value buffer

let clear buffer =
    mkCircularBuffer buffer.capacity
