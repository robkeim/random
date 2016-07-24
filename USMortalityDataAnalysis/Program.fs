// This data was originally download from here:
// https://www.kaggle.com/cdc/mortality

open System
open Parsing

[<EntryPoint>]
let main argv =
    let deathRecords = GetDeathRecords

    deathRecords
    |> Array.iter (fun p -> printfn "%A" p)

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0 // return an integer exit code
