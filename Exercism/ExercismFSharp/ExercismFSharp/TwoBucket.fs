module TwoBucket

open System

type Bucket =
    | One
    | Two

// Pour water from bucket A to bucket B
let private pour curA curB sizeB =
    match curA + curB > sizeB with
    | true  -> (curA - (sizeB - curB), sizeB)
    | false -> (0, curA + curB)

let private validateState (states : Set<int * int>) newState =
    match states.Contains newState with
        | true  -> None
        | false -> Some newState

let rec private movesHelper b1size b2size goal startBucket b1current b2current numMoves states =
    // Pour bucket 1 into bucket 2
    let pour1to2 =
        let (b1new, b2new) = pour b1current b2current b2size
        match startBucket = Bucket.One && b1new = 0 && b2new = b2size with
        | true  -> None
        | false -> validateState states (b1new, b2new)
    
    // Pour bucket 2 into bucket 1
    let pour2to1 =
        let (b2new, b1new) = pour b2current b1current b1size
        match startBucket = Bucket.Two && b2new = 0 && b1new = b1size with
        | true  -> None
        | false -> validateState states (b1new, b2new)
    
    // Empty bucket 1
    let empty1 =
        match startBucket = Bucket.One && b2current = b2size with
        | true  -> None
        | false -> validateState states (0, b2current)
    
    // Empty bucket 2
    let empty2 = 
        match startBucket = Bucket.Two && b1current = b1size with
        | true  -> None
        | false -> validateState states (b1current, 0)
    
    // Fill bucket 1
    let fill1 =
        match startBucket = Bucket.Two && b2current = 0 with
        | true  -> None
        | false -> validateState states (b1size, b2current)
    
    // Fill bucket 2
    let fill2 =
        match startBucket = Bucket.One && b1current = 0 with
        | true  -> None
        | false -> validateState states (b1current, b2size)

    match b1current = goal || b2current = goal with
    | true when b1current = goal -> Some (numMoves, Bucket.One, b2current) |> List.singleton
    | true when b2current = goal -> Some (numMoves, Bucket.Two, b1current) |> List.singleton
    | _ ->
        let possibleStates =
            [ pour1to2; pour2to1; empty1; empty2; fill1; fill2 ]
            |> List.choose id
        
        match possibleStates with
        | [] -> None |> List.singleton
        | _  -> possibleStates 
                |> List.map (fun (b1new, b2new) ->
                    movesHelper b1size b2size goal startBucket b1new b2new (numMoves + 1) (states.Add((b1new, b2new))))
                |> List.concat
    
let moves bucketOneSize bucketTwoSize goal startBucket =
    movesHelper bucketOneSize bucketTwoSize goal startBucket 0 0 0 (Set.empty.Add((0, 0)))
    |> List.choose id
    |> List.sortBy (fun (numMoves, _, _) -> numMoves)
    |> List.tryHead