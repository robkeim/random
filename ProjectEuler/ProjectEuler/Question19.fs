module Question19

open System

let numSundays =
    let result =
        Seq.initInfinite (fun i -> DateTime(1901, 1, 1).AddDays (i |> float))
        |> Seq.takeWhile (fun d -> d < DateTime(2001, 1, 1))
        |> Seq.filter (fun d -> d.Day = 1 && d.DayOfWeek = DayOfWeek.Sunday)
        |> Seq.length

    Console.WriteLine result

    ()