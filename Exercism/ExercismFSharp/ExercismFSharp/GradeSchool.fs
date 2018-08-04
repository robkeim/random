module GradeSchool

type School = { roster : (int * string) list }

let empty = { roster = [] }

let add name grade school =
    { roster = (grade, name) :: school.roster }

let grade grade school =
    school.roster
    |> List.filter (fun (g, _) -> g = grade)
    |> List.map snd
    |> List.sort

let roster school =
    school.roster
    |> List.groupBy fst
    |> List.map (fun (grade, values) ->
        let namesInGrade =
            values
            |> List.map snd
            |> List.sort
        (grade, namesInGrade)
    )
    |> List.sort