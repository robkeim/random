module WordSearch

let private safeArrayAccess (array : 'a list list) coordinate =
    let (i, j) = coordinate
    try
        Some array.[i].[j]
    with
    | :? System.ArgumentException -> None
    | :? System.IndexOutOfRangeException -> None

type private Direction =
    | LeftToRight
    | RightToLeft
    | TopToBottom
    | BottomToTop
    | TopLeftToBottomRight
    | BottomRightToTopLeft
    | BottomLeftToTopRight
    | TopRightToBottomLeft

let private directions =
    [
        Direction.LeftToRight;
        Direction.RightToLeft;
        Direction.TopToBottom;
        Direction.BottomToTop;
        Direction.TopLeftToBottomRight;
        Direction.BottomRightToTopLeft;
        Direction.BottomLeftToTopRight;
        Direction.TopRightToBottomLeft;
    ]

let private getNextCoordinate coordinate dir =
    let (i, j) = coordinate
    match dir with
    | Direction.LeftToRight          -> (i, j + 1)
    | Direction.RightToLeft          -> (i, j - 1)
    | Direction.TopToBottom          -> (i + 1, j)
    | Direction.BottomToTop          -> (i - 1, j)
    | Direction.TopLeftToBottomRight -> (i + 1, j + 1)
    | Direction.BottomRightToTopLeft -> (i - 1, j - 1)
    | Direction.BottomLeftToTopRight -> (i - 1, j + 1)
    | Direction.TopRightToBottomLeft -> (i + 1, j - 1)

let rec private isMatch puzzle word coordinate dir =
    match word with
    | []    -> true
    | x::xs ->  match safeArrayAccess puzzle coordinate = Some x with
                | true  -> isMatch puzzle xs (getNextCoordinate coordinate dir) dir
                | false -> false
       
let private formatOutput coordinate length dir =
    let (i, j) = coordinate
    let (stopi, stopj) =
        [0 .. length - 1]
        |> List.fold (fun state _ -> getNextCoordinate state dir) coordinate

    Some ((j + 1, i + 1), (stopj + 1, stopi + 1))

let find puzzle word =
    let normalizedPuzzle =
        puzzle
        |> List.map Seq.toList

    let height = normalizedPuzzle |> List.length
    let width = normalizedPuzzle.[0] |> List.length

    [0 .. height - 1]
    |> List.map (fun i ->
        [0 .. width - 1]
        |> List.map (fun j ->
            directions
            |> List.map (fun dir ->
                match isMatch normalizedPuzzle (word |> Seq.toList) (i, j) dir with
                | true  -> formatOutput (i, j) ((word |> Seq.length) - 1) dir
                | false -> None
            )
        )
    )
    |> List.concat
    |> List.concat
    |> List.choose id
    |> List.tryHead