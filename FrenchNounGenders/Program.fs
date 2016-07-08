open System
open Algorithms
open Calculations

[<EntryPoint>]
let main argv =
    runAlgorithm alwaysReturnMasculine "Always masculine"
    runAlgorithm alwaysReturnFeminine "Always feminine"
    runAlgorithm vowelsAndConsonents "Vowels and consonents"
    runAlgorithm perLetter "Per letter"
    runAlgorithm frenchTogether "French together"
    runAlgorithm fluentU "Fluent u"

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0