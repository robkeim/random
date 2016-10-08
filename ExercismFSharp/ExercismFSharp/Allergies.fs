module Allergies

type Allergen =
    | Cats
    | Chocolate
    | Eggs
    | Peanuts
    | Pollen
    | Shellfish
    | Strawberries
    | Tomatoes
    
let allergicTo allergen score =
    let value =
        match allergen with
        | Allergen.Eggs         -> 1
        | Allergen.Peanuts      -> 2
        | Allergen.Shellfish    -> 4
        | Allergen.Strawberries -> 8
        | Allergen.Tomatoes     -> 16
        | Allergen.Chocolate    -> 32
        | Allergen.Pollen       -> 64
        | Allergen.Cats         -> 128
    score &&& value = value 

let allergies score =
    [
        Allergen.Eggs;
        Allergen.Peanuts;
        Allergen.Shellfish;
        Allergen.Strawberries;
        Allergen.Tomatoes;
        Allergen.Chocolate;
        Allergen.Pollen;
        Allergen.Cats
     ]
     |> List.filter
        (fun a -> allergicTo a score)