module House

open System

let private lines =
    [
        ("", "the horse and the hound and the horn");
        ("that belonged to", "the farmer sowing his corn");
        ("that kept", "the rooster that crowed in the morn");
        ("that woke", "the priest all shaven and shorn");
        ("that married", "the man all tattered and torn");
        ("that kissed", "the maiden all forlorn");
        ("that milked", "the cow with the crumpled horn");
        ("that tossed", "the dog");
        ("that worried", "the cat");
        ("that killed", "the rat");
        ("that ate", "the malt");
        ("that lay in", "the house that Jack built.")
    ]
    |> List.rev

let verse n =
    let linesInVerse = lines |> List.take (n + 1) |> List.rev
    let first = sprintf "This is %s" (linesInVerse |> List.head |> snd)
    let rest =
        linesInVerse
        |> List.tail
        |> List.map (fun (first, second) -> sprintf "%s %s" first second)

    List.append
        (first |> List.singleton)
        rest
    |> List.reduce (sprintf "%s\n%s")

let rhyme =
    [0 .. (lines |> Seq.length) - 1]
    |> List.map verse
    |> Seq.reduce (sprintf "%s\n\n%s")