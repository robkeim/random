open System
open Algorithms
open Calculations

// The nouns in NounList.txt were taken from the following site
// http://www.dicollecte.org/download.php?prj=fr

// The nouns in NounListShort.txt were taken from the following site and represent only the most common nouns
// http://www.memrise.com/course/131111/5000-most-common-french-words-2/

[<EntryPoint>]
let main argv =
    runAlgorithm alwaysReturnMasculine "Always masculine"
    runAlgorithm alwaysReturnFeminine "Always feminine"
    runAlgorithm vowelsAndConsonants "Vowels and consonants"
    runAlgorithm perLetter "Per letter"
    runAlgorithm frenchTogether "French together"
    runAlgorithm fluentU "Fluent u"

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0