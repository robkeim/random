module Poker

open System

type private Suit =
    | Clubs
    | Diamonds
    | Hearts
    | Spades

type private Value =
    | Two = 2
    | Three = 3
    | Four = 4
    | Five = 5
    | Six = 6
    | Seven = 7
    | Eight = 8
    | Nine = 9
    | Ten = 10
    | Jack = 11
    | Queen = 12
    | King = 13
    | Ace = 14

let private parseCard (card : string) =
    let value =
        match card.[0] with
        | '2' -> Value.Two
        | '3' -> Value.Three
        | '4' -> Value.Four
        | '5' -> Value.Five
        | '6' -> Value.Six
        | '7' -> Value.Seven
        | '8' -> Value.Eight
        | '9' -> Value.Nine
        | 'T' -> Value.Ten
        | 'J' -> Value.Jack
        | 'Q' -> Value.Queen
        | 'K' -> Value.King
        | 'A' -> Value.Ace
        | _ -> raise (ArgumentException "invalid card value")

    let suit =
        match card.[1] with
        | 'C' -> Suit.Clubs
        | 'D' -> Suit.Diamonds
        | 'H' -> Suit.Hearts
        | 'S' -> Suit.Spades
        | _ -> raise (ArgumentException "invalid card suit")

    (value, suit)

type private HandType =
    | HighCard = 0
    | Pair = 1
    | TwoPair = 2
    | ThreeOfAKind = 3
    | Straight = 4
    | Flush = 5
    | FourOfAKind = 6
    | StraightFlush = 7

type private Hand =
    | HighCard of Value
    | Pair of Value
    | TwoPair of Value * Value
    | ThreeOfAKind of Value
    | Straight of Value
    | Flush of Value
    | FullHouse of Value * Value
    | FourOfAKind of Value
    | StraightFlush of Value

let private highCard cards =
    let high =
        cards
        |> Seq.map fst
        |> Seq.sortDescending
        |> Seq.head

    HighCard(high) |> Some

let private getGroup size cards =
    cards
    |> Seq.map fst
    |> Seq.countBy id
    |> Seq.filter (fun (_, n) -> n = size)
    |> Seq.map fst
    |> Seq.tryHead

let private pair cards =
    match getGroup 2 cards with
    | Some value -> Pair(value) |> Some
    | None -> None

let private twoPair cards =
    let pairs =
        cards
        |> Seq.map fst
        |> Seq.countBy id
        |> Seq.filter (fun (_, n) -> n = 2)
        |> Seq.map fst
        |> Seq.sortDescending
        |> Seq.toList

    match Seq.length pairs = 2 with
    | true  -> TwoPair(pairs.[0], pairs.[1]) |> Some
    | false -> None

let private threeOfAKind cards =
    match getGroup 3 cards with
    | Some value -> ThreeOfAKind(value) |> Some
    | None -> None

let private straight cards =
    let values =
        cards
        |> Seq.map fst
        |> Seq.sortDescending

    let last = values |> Seq.head

    let isStraight =
        values
        |> Seq.mapi (fun i value -> (value |> int) = ((last |> int) - i))
        |> Seq.forall id

    match isStraight with
    | true  -> Straight(last) |> Some
    | false -> None

let private flush cards =
    let pair =
        cards
        |> Seq.countBy snd
        |> Seq.filter (fun (_, n) -> n = 5)
        |> Seq.tryHead

    let highCard = highCard cards

    match pair, highCard with
    | Some _, Some (HighCard(value)) -> Flush(value) |> Some
    | _ -> None

let private fullHouse cards =
    match (pair cards), (threeOfAKind cards) with
    | Some (Pair(p)), Some (ThreeOfAKind(t)) -> FullHouse(t, p) |> Some
    | _ -> None

let private fourOfAKind cards =
    match getGroup 4 cards with
    | Some value -> FourOfAKind(value) |> Some
    | None -> None

let private straightFlush cards =
    match (straight cards), (flush cards) with
    | Some (Straight(s)), Some _ -> StraightFlush(s) |> Some
    | _ -> None

let private getHandValue (hand : string) =
    let cards =
        hand.Split [| ' ' |]
        |> Array.map parseCard
    
    [
        straightFlush;
        fourOfAKind;
        fullHouse;
        flush;
        straight;
        threeOfAKind;
        twoPair;
        pair;
        highCard;
    ]
    |> List.choose (fun f -> f cards)
    |> List.head

let bestHands hands =
    let zip =
        Seq.zip
            (hands |> Seq.map getHandValue)
            hands
        |> Seq.sortDescending

    let first =
        zip
        |> Seq.head
        |> fst

    zip
    |> Seq.takeWhile (fun (h, _) -> h = first)
    |> Seq.map snd