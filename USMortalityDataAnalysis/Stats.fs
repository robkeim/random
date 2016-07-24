module Stats

open Types

let private CalculatePercentage (x : int) (y : int) =
    let xfloat = x |> float
    let yfloat = y |> float
    (xfloat / yfloat) * 100.

// TODO rkeim refactor these functions
let private NumDeathsForMonth deathRecords month =
    deathRecords
        |> Array.filter (fun r -> r.MonthOfDeath = month)
        |> Array.length

let private PrintMonthStats deathRecords : unit =
    let numRecords = Array.length deathRecords

    printfn "\nBreakdown by month:"
    let deathsPerMonth =
        [|1..12|]
        |> Array.map (fun i -> NumDeathsForMonth deathRecords i)

    let months =
        [| "January"; "February"; "March"; "April"; "May"; "June"; "July"; "August"; "September"; "October"; "November"; "December" |]

    Array.iter2
        (fun month deaths -> printfn "\t%s: %.2f%% (%i)" month (CalculatePercentage deaths numRecords) deaths)
        months
        deathsPerMonth

let private NumDeathsForMaritalStatus deathRecords maritalStatus =
    deathRecords
        |> Array.filter (fun r -> r.MaritalStatus = maritalStatus)
        |> Array.length

let private PrintMaritalStatuses deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "\nBreakdown by marital status:"
    let deathsPerMaritalStatus =
        [| MaritalStatus.NeverMarriedSingle; MaritalStatus.Married; MaritalStatus.Widowed; MaritalStatus.Divorced; MaritalStatus.MaritalStatusUnknown |]
        |> Array.map (fun s -> NumDeathsForMaritalStatus deathRecords s)
    
    let maritalStatuses =
        [| "SingleAndNeverMarried"; "Married"; "Widowed"; "Divorced"; "Unknown" |]
    
    Array.iter2
        (fun month deaths -> printfn "\t%s: %.2f%% (%i)" month (CalculatePercentage deaths numRecords) deaths)
        maritalStatuses
        deathsPerMaritalStatus

let private NumDeathsForSex deathRecords sex =
    deathRecords
        |> Array.filter (fun r -> r.Sex = sex)
        |> Array.length

let private PrintSexes deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "\nBreakdown by sex:"

    let deathsPerSex = 
        [| Sex.Male; Sex.Female |]
        |> Array.map (fun s -> NumDeathsForSex deathRecords s)

    let sexes =
        [| "Male"; "Female" |]

    Array.iter2
        (fun month deaths -> printfn "\t%s: %.2f%% (%i)" month (CalculatePercentage deaths numRecords) deaths)
        sexes
        deathsPerSex

let GetStats deathRecords : unit =
    let numRecords = Array.length deathRecords
    printfn "Total records: %i" numRecords
    PrintMonthStats deathRecords
    PrintMaritalStatuses deathRecords
    PrintSexes deathRecords