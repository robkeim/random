module Algorithms

open Types

// Always return one gender
let alwaysReturnOneGender list gender =
    list
        |> List.map (fun word -> {Word=word; Gender=gender})

// Always return masculine
let alwaysReturnMasculine list =
    alwaysReturnOneGender list Masculine

// Always return feminine
let alwaysReturnFeminine list =
    alwaysReturnOneGender list Feminine