module SimpleCipher

open System

let private getRandomSequence =
    let r = Random()
    Seq.initInfinite (fun _ -> r.Next(0, 26) + 97 |> char)

let private validateIsLowerCase input =
    let isValid =
        input
        |> Seq.forall (fun c ->
            let value = c |> int
            value >=  97 && value <= 122
        )

    match isValid && input |> Seq.length > 0 with
    | true -> ()
    | false -> raise (ArgumentException "invalid characters")

let private treatLetter op key input =
    let convertLetterToZero letter =
        (letter |> int) - 97

    let result = (((op (convertLetterToZero input) (convertLetterToZero key)) + 26) % 26) + 97 |> char 
    result

let private treatInput op key input =
    validateIsLowerCase key
    validateIsLowerCase input

    Seq.zip key input
    |> Seq.map (fun (key, value) -> treatLetter op key value)
    |> Seq.fold (sprintf "%s%c") String.Empty

let encode key input =
    treatInput (+) key input

let decode key input =
    treatInput (-) key input

// TODO rkeim does input need to be strongly typed here?
let encodeRandom (input : string) =
    let randomKey =
        getRandomSequence
        |> Seq.take 100
        |> Seq.fold (sprintf "%s%c") String.Empty

    (randomKey, encode randomKey input)