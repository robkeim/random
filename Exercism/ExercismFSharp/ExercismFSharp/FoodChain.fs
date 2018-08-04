module FoodChain

let animals =
    [
        ("fly", "I don't know why she swallowed the fly. Perhaps she'll die.")
        ("spider", "It wriggled and jiggled and tickled inside her.")
        ("bird", "How absurd to swallow a bird!")
        ("cat", "Imagine that, to swallow a cat!")
        ("dog", "What a hog, to swallow a dog!")
        ("goat", "Just opened her throat and swallowed a goat!")
        ("cow", "I don't know how she swallowed a cow!")
        ("horse", "She's dead, of course!")
    ]

let firstLine n =
    let (fst, snd) = animals.[n - 1]
    sprintf "I know an old lady who swallowed a %s.\n%s" fst snd

let middleLine n =
    let first = animals.[n] |> fst
    let second =
        match n with
        | 2 -> "spider that wriggled and jiggled and tickled inside her"
        | _ -> animals.[n - 1] |> fst
    sprintf "She swallowed the %s to catch the %s." first second

let verse n =
    match n with
    | 1 | 8 -> firstLine n
    | _ ->
        let middleLines =
            [ n - 1 .. -1 .. 1 ]
            |> List.map middleLine
            |> List.reduce (sprintf "%s\n%s")
        sprintf "%s\n%s\nI don't know why she swallowed the fly. Perhaps she'll die." (firstLine n) middleLines

let song =
    [1..8]
    |> List.map verse
    |> List.reduce (sprintf "%s\n\n%s")