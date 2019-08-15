module Problem25

open System

let thousandDigitFibonacci =
    let fibs =  
        let rec f a b = seq { yield a
                              yield! f b (a+b) }
        f 1I 1I
        |> Seq.cache

    let num =
        [2..1000]
        |> List.fold (fun acc _ -> acc * 10I) 1I

    let result =
        fibs
        |> Seq.takeWhile (fun value -> value < num)
        |> Seq.length

    Console.WriteLine (result + 1)
    ()