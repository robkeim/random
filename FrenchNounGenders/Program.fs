open System
open Algorithms
open Calculations

[<EntryPoint>]
let main argv =
    runAlgorithm alwaysReturnMasculine "Always masculine"
    runAlgorithm alwaysReturnFeminine "Always feminine"

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0