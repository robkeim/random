module BeerSong

let verse num =
    match num with
    | 0 -> "No more bottles of beer on the wall, no more bottles of beer.\nGo to the store and buy some more, 99 bottles of beer on the wall.\n"
    | 1 -> "1 bottle of beer on the wall, 1 bottle of beer.\nTake it down and pass it around, no more bottles of beer on the wall.\n"
    | 2 -> "2 bottles of beer on the wall, 2 bottles of beer.\nTake one down and pass it around, 1 bottle of beer on the wall.\n"
    | _ -> sprintf "%d bottles of beer on the wall, %d bottles of beer.\nTake one down and pass it around, %d bottles of beer on the wall.\n" num num (num - 1)

let verses start stop =
    [ start .. -1 .. stop ]
    |> List.map verse
    |> List.fold (sprintf "%s%s\n") ""

let sing =
    verses 99 0