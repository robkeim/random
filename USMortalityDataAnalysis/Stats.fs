﻿module Stats

open Types

let private CalculatePercentage (x : int) (y : int) =
    let xfloat = x |> float
    let yfloat = y |> float
    (xfloat / yfloat) * 100.

let private MapCounts fn deathRecords : Map<'a, int> =
    deathRecords
    |> Array.groupBy fn
    |> Array.map (fun (name, records) -> (name, Array.length records))
    |> Map.ofArray

let private PrintCounts map labels =
    let numRecords =
        Map.fold
            (fun state key value -> state + value)
            0
            map

    Array.iter
        (fun label ->
            let occurances =
                match map.ContainsKey label with
                | true -> map.[label]
                | false -> 0
            printfn "\t%A: %.2f%% (%i)" label (CalculatePercentage occurances numRecords) occurances)
        labels

// Months
let private PrintMonths deathRecords =
    let monthsMap =
        MapCounts
            (fun record -> record.MonthOfDeath)
            deathRecords

    printfn "\nBreakdown by month:"
    PrintCounts
        monthsMap
        [| 1 .. 12 |]

// Marital statuses
let private PrintMaritalStatuses deathRecords =
    let maritalStatusesMap =
        MapCounts
            (fun record -> record.MaritalStatus)
            deathRecords

    printfn "\nBreakdown by marital status:"
    PrintCounts
        maritalStatusesMap
        [| MaritalStatus.SingleNeverMarried; MaritalStatus.Married; MaritalStatus.Widowed; MaritalStatus.Divorced; MaritalStatus.Unknown |]

// Sexes
let private PrintSexes deathRecords =
    let sexesMap =
        MapCounts
            (fun record -> record.Sex)
            deathRecords

    printfn "\nBreakdown by sex:"
    PrintCounts
        sexesMap
        [| Sex.Male; Sex.Female |]

// Age of death (for adults)
let private PrintAgeOfDeaths deathRecords =
    let filteredDeathRecords =
        deathRecords
        |> Array.filter (fun record -> record.AgeType = AgeType.Years)

    let agesMap =
        MapCounts
            (fun record -> record.Age)
            filteredDeathRecords

    let ages =
        agesMap
        |> Map.toArray
        |> Array.map (fun (age, _) -> age)
        |> Array.sort

    printfn "\nBreakdown by age of death for adults:"
    PrintCounts
        agesMap
        ages

// Method of disposition
let private PrintMethodsOfDisposition deathRecords =
    let methodsOfDispositionMap =
        MapCounts
            (fun record -> record.MethodOfDisposition)
            deathRecords

    printfn "\nBreakdown by methods of disposition:"
    PrintCounts
        methodsOfDispositionMap
        [| MethodOfDisposition.Buriel; MethodOfDisposition.Cremation; MethodOfDisposition.Other; MethodOfDisposition.Unknown |]

let private PrintAutopsy deathRecords =
    let autopsyMap =
        MapCounts
            (fun record -> record.Autopsy)
            deathRecords

    printfn "\nBreakdown by autopsy:"
    PrintCounts
        autopsyMap
        [| Autopsy.Yes; Autopsy.No; Autopsy.Unknown |]

let private PrintInjuryAtWork deathRecords =
    let injuryAtWorkMap =
        MapCounts
            (fun record -> record.InjuryAtWork)
            deathRecords

    printfn "\nBreakdown by injury at work:"
    PrintCounts
        injuryAtWorkMap
        [| InjuryAtWork.Yes; InjuryAtWork.No; InjuryAtWork.Unknown |]

let private PrintMannerOfDeath deathRecords =
    let mannerOfDeathMap =
        MapCounts
            (fun record -> record.MannerOfDeath)
            deathRecords

    printfn "\nBreakdown by manner of death:"
    PrintCounts
        mannerOfDeathMap
        [|
            MannerOfDeath.Accident
            MannerOfDeath.Suicide
            MannerOfDeath.Homicide
            MannerOfDeath.PendingInvestigation
            MannerOfDeath.CouldNotDetermine
            MannerOfDeath.SelfInflicted
            MannerOfDeath.Natural
            MannerOfDeath.NotSpecified
        |]

let GetStats deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "Total records: %i" numRecords
    // PrintMonths deathRecords
    // PrintMaritalStatuses deathRecords
    // PrintSexes deathRecords
    // PrintAgeOfDeaths deathRecords
    // PrintMethodsOfDisposition deathRecords
    // PrintAutopsy deathRecords
    // PrintInjuryAtWork deathRecords
    PrintMannerOfDeath deathRecords