module KinderGartenGarden

open System

type Plant =
    | Clover
    | Grass
    | Radishes
    | Violets

type Garden = { garden : (string * Plant list) list }

type private Student =
    | Alice = 0
    | Bob = 1
    | Charlie = 2
    | David = 3
    | Eve = 4
    | Fred = 5
    | Ginny = 6
    | Harriet = 7
    | Ileana = 8
    | Joseph = 9
    | Kincaid = 10
    | Larry = 11

let safeArrayAccess offset (line : string) =
    try
        let lowerBound = offset * 2
        line.[lowerBound .. lowerBound + 1]
        |> Seq.map (fun value ->
            match value with
            | 'C' -> Some Plant.Clover
            | 'G' -> Some Plant.Grass
            | 'R' -> Some Plant.Radishes
            | 'V' -> Some Plant.Violets
            | _ -> None
        )
    with
    | :? ArgumentOutOfRangeException -> None |> Seq.singleton
        
let private generateGarden students (input : string) =
    let rows = input.Split [| '\n' |]

    let garden =
        students
        |> Seq.sort
        |> Seq.mapi (fun i student ->
            let plants =
                rows
                |> Seq.map (safeArrayAccess i)
                |> Seq.concat
                |> Seq.choose id
                |> Seq.toList

            (student, plants)
        )
        |> Seq.filter (fun (_, plants) -> not (List.isEmpty plants))
        |> Seq.toList
        
    { garden = garden }

let defaultGarden input =
    let students =
        Seq.cast (Enum.GetValues(typeof<Student>))
        |> Seq.map (fun enum ->
            let student = unbox<Student> enum
            string student
        )

    generateGarden students input

let lookupPlants person garden =
    let personAndPlants =
        garden.garden
        |> List.tryFind (fun (p, _) -> p = person)

    match personAndPlants with
    | Some p -> snd p
    | none -> []

let garden students input =
    generateGarden students input