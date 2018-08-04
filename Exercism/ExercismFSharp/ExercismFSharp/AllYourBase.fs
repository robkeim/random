module AllYourBase

let private toBaseTen inputBase inputDigits =
    inputDigits
    |> List.rev
    |> List.fold (fun (total, valueForDigit) elem ->
        (total + (elem * valueForDigit), valueForDigit * inputBase)
        )
        (0, 1)
    |> fst

let rec private fromBaseTen number outputBase outputDigits =
    match number with
    | 0 -> outputDigits
    | _ -> fromBaseTen (number / outputBase) outputBase ((number % outputBase) :: outputDigits)

let private validateInput inputBase inputDigits outputBase =
    let areBasesValid = inputBase >= 2 && outputBase >= 2

    let isInputSequenceValid =
        match inputDigits with
        | [] -> false
        | 0::_ -> false
        | _ -> true

    let areInputDigitsValid =
        inputDigits |> Seq.forall (fun digit -> digit >= 0 && digit < inputBase)

    areBasesValid && isInputSequenceValid && areInputDigitsValid

let rebase inputBase inputDigits outputBase =
    match validateInput inputBase inputDigits outputBase with
    | false -> None
    | true ->
            let baseTenNumber = toBaseTen inputBase inputDigits
            Some (fromBaseTen baseTenNumber outputBase [])