module Stats

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
        (fun label -> printfn "\t%A: %.2f%% (%i)" label (CalculatePercentage map.[label] numRecords) map.[label])
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

let GetStats deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "Total records: %i" numRecords
    PrintMonths deathRecords
    PrintMaritalStatuses deathRecords
    PrintSexes deathRecords
    PrintAgeOfDeaths deathRecords