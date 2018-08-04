module Meetup

open System

type Schedule =
    | First
    | Second
    | Third
    | Fourth
    | Last
    | Teenth

let private getDaysInMonth year month dayOfWeek =
    [1..31]
    |> List.choose (fun day ->
        try
            Some (DateTime(year, month, day))
        with
        | :? System.ArgumentOutOfRangeException -> None
    )
    |> List.filter (fun date -> date.DayOfWeek = dayOfWeek)

let meetupDay dayOfWeek schedule year month =
    let daysInMonth = getDaysInMonth year month dayOfWeek

    match schedule with
    | First  -> daysInMonth |> Seq.head
    | Second -> daysInMonth |> Seq.skip 1 |> Seq.head
    | Third  -> daysInMonth |> Seq.skip 2 |> Seq.head
    | Fourth -> daysInMonth |> Seq.skip 3 |> Seq.head
    | Last   -> daysInMonth |> Seq.last
    | Teenth -> daysInMonth |> Seq.filter (fun date -> date.Day >= 13 && date.Day <= 19) |> Seq.head