open System
open Algorithms
open Calculations

[<EntryPoint>]
let main argv =
    runAlgorithm alwaysReturnMasculine "Always masculine"
    runAlgorithm alwaysReturnFeminine "Always feminine"
    runAlgorithm simplifiedListOfEndings "Simplified list of endings"

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0