module Atbash

open System

let encode phrase =
    phrase
    |> Seq.choose (fun c ->
        match Char.ToLowerInvariant c with
        | 'a' -> Some 'z'
        | 'b' -> Some 'y'
        | 'c' -> Some 'x'
        | 'd' -> Some 'w'
        | 'e' -> Some 'v'
        | 'f' -> Some 'u'
        | 'g' -> Some 't'
        | 'h' -> Some 's'
        | 'i' -> Some 'r'
        | 'j' -> Some 'q'
        | 'k' -> Some 'p'
        | 'l' -> Some 'o'
        | 'm' -> Some 'n'
        | 'n' -> Some 'm'
        | 'o' -> Some 'l'
        | 'p' -> Some 'k'
        | 'q' -> Some 'j'
        | 'r' -> Some 'i'
        | 's' -> Some 'h'
        | 't' -> Some 'g'
        | 'u' -> Some 'f'
        | 'v' -> Some 'e'
        | 'w' -> Some 'd'
        | 'x' -> Some 'c'
        | 'y' -> Some 'b'
        | 'z' -> Some 'a'
        | _ when c >= '0' && c <= '9' -> Some c
        | _ -> None
        )
    |> Seq.chunkBySize 5
    |> Seq.map System.String
    |> Seq.reduce (sprintf "%s %s")