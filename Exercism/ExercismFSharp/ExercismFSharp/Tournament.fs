module Tournament

type private Result =
    | Win
    | Draw
    | Loss

type private TeamRecord =
    {
        name : string
        matchesPlayed : int
        wins : int
        draws : int
        losses : int
        points : int
    }

let private parseLine (line : string) =
    let foo =
        match line.Split[|';'|] with
        | [|a; b; "win"|]  -> Some [(a, Result.Win); (b, Result.Loss)]
        | [|a; b; "draw"|] -> Some [(a, Result.Draw); (b, Result.Draw)]
        | [|a; b; "loss"|] -> Some [(a, Result.Loss); (b, Result.Win)]
        | _ -> None
    foo

let tally input =
    let results =
        input
        |> List.choose parseLine
        |> List.concat
        |> List.groupBy fst
        |> List.map (fun (key, value) -> (key, value |> List.map snd))
        |> List.map (fun (name, results) ->
            let mp = results |> List.length
            let w = results |> List.filter (fun r -> r = Result.Win) |> List.length
            let d = results |> List.filter (fun r -> r = Result.Draw) |> List.length
            let l = results |> List.filter (fun r -> r = Result.Loss) |> List.length
            let p = 3 * w + d
            { name = name; matchesPlayed = mp; wins = w; draws = d; losses = l; points = p })
        |> List.sortBy(fun record -> (-1 * record.points, record.name))
        |> List.map (fun record -> (sprintf "%-31s| %2i | %2i | %2i | %2i | %2i" record.name record.matchesPlayed record.wins record.draws record.losses record.points))

    let header = "Team                           | MP |  W |  D |  L |  P"
    
    header::results