module Calculations

open System
open Algorithms
open FParsec
open FrenchNouns
open Parsers
open Types

let nounList : Noun list =
    match run pInput rawNounList with
        | Success(result, _, _) -> result
        | Failure(errorMsg, _, _) -> raise (System.Exception("Invalid input list of words"))

let wordList : string list =
    nounList
        |> List.map (fun noun -> noun.Word)

// Calculate statistics
let numNouns list =
    List.length list

let numNounsPerGender list gender =
    list
        |> List.filter (fun noun -> noun.Gender = gender)
        |> List.length

let numMasculineNouns list =
    numNounsPerGender list Masculine

let numFeminineNouns list =
    numNounsPerGender list Feminine

let percentage num total =
    Math.Round((float)num / (float)total * 100., 2)

let printResult title num total =
    let percentage = percentage num total
    printfn "    %s %.2f%% (%i / %i)" title percentage num total

let getNounForWord word (algorithm : string -> Gender) =
    let gender = algorithm word
    { Word=word; Gender=gender }

let getMatches (algorithm : string -> Gender) = 
    let result : Noun list =
        wordList
            |> Seq.map (fun word -> (getNounForWord word algorithm))
            |> Seq.toList

    let resultMap =
        result
            |> List.map (fun noun -> (noun.Word, noun.Gender))
            |> dict

    List.filter
        (fun noun -> noun.Gender = resultMap.Item(noun.Word))
        nounList

// Run algorithm
let runAlgorithm algorithm title =
    let result = getMatches algorithm
    let numCorrect = Seq.length result
    let numMasculineCorrect = numMasculineNouns result
    let numFeminineCorrect = numFeminineNouns result

    printfn "\nAlgorithm: %s" title
    printResult "Overall  " numCorrect (numNouns nounList)
    printResult "Masculine" numMasculineCorrect (numMasculineNouns nounList)
    printResult "Feminine " numFeminineCorrect (numFeminineNouns nounList)

let runAlgorithmForOneword algorithm word =
    printfn "%s -> %A" word (algorithm word)