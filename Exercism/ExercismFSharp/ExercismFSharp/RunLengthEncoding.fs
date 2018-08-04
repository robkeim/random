module RunLengthEncoding

open System.Text.RegularExpressions

let encode input =
    Regex.Matches(input, "(.)\1*")
    |> Seq.cast
    |> Seq.map (fun (m : Match) ->
        let result = m.Groups.[0].ToString()
        let length = result |> Seq.length
        let char = result.[0]

        match length with
        | 1 -> sprintf "%c" char
        | _ -> sprintf "%i%c" length char
    )
    |> Seq.reduce (sprintf "%s%s")

let decode input =
    Regex.Matches(input, "(\d*)(.)")
    |> Seq.cast
    |> Seq.map (fun (m : Match) ->
        let numOcc = m.Groups.[1].ToString()
        let letter = m.Groups.[2].ToString()

        match numOcc with
        | "" -> letter
        | _  ->
            Seq.init (numOcc |> int) (fun _ -> letter)
            |> Seq.reduce (sprintf "%s%s")
    )
    |> Seq.reduce (sprintf "%s%s")