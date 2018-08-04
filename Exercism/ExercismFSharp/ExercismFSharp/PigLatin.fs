module PigLatin

open System

let private translateWord (word : string) =
    let vowelPrefix =
        [ "xr"; "yt"; "a"; "e"; "i"; "o"; "u" ]
        |> List.tryFind (fun prefix -> word.StartsWith prefix)

    let consonentPrefix =
        [
            "sch"; "thr";
            "bqu"; "cqu"; "dqu"; "fqu"; "gqu"; "hqu"; "jqu"; "kqu"; "lqu"; "mqu"; "nqu"; "pqu"; "qqu"; "rqu"; "squ"; "tqu"; "vqu"; "wqu"; "xqu"; "yqu"; "zqu";
            "ch"; "qu"; "th";
            "b"; "c"; "d"; "f"; "g"; "h"; "j"; "k"; "l"; "m"; "n"; "p"; "q"; "r"; "s"; "t"; "v"; "w"; "x"; "y"; "z"
        ]
        |> List.tryFind (fun prefix -> word.StartsWith prefix)
    
    match vowelPrefix, consonentPrefix with
    | Some prefix, _ -> sprintf "%say" word
    | _, Some prefix ->
        let len = Seq.length prefix
        sprintf "%s%say" word.[len..] prefix
    | _, _ -> raise (Exception "unreachable code")

let translate (input : string) =
    input.Split [| ' ' |]
    |> Array.map translateWord
    |> Array.reduce (sprintf "%s %s")