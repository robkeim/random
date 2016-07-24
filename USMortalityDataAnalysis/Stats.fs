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
let private MapMonths deathRecords =
    MapCounts (fun record -> record.MonthOfDeath) deathRecords

let private PrintMonths deathRecords =
    printfn "\nBreakdown by month:"
    PrintCounts
        (MapMonths deathRecords)
        [| 1 .. 12 |]

// Marital statuses
let private MapMaritalStatuses deathRecords =
    MapCounts (fun record -> record.MaritalStatus) deathRecords

let private PrintMaritalStatuses deathRecords =
    printfn "\nBreakdown by marital status:"
    PrintCounts
        (MapMaritalStatuses deathRecords)
        [| MaritalStatus.SingleNeverMarried; MaritalStatus.Married; MaritalStatus.Widowed; MaritalStatus.Divorced; MaritalStatus.Unknown |]

// Sexes
let private MapSexes deathRecords =
    MapCounts (fun record -> record.Sex) deathRecords

let private PrintSexes deathRecords =
    printfn "\nBreakdown by sex:"
    PrintCounts
        (MapSexes deathRecords)
        [| Sex.Male; Sex.Female |]

let GetStats deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "Total records: %i" numRecords
    PrintMonths deathRecords
    PrintMaritalStatuses deathRecords
    PrintSexes deathRecords