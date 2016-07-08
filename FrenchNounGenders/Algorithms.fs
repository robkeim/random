module Algorithms

open Types

let alwaysReturnOneGender word gender =
    { Word=word; Gender=gender }

// Always return masculine
let alwaysReturnMasculine word =
    alwaysReturnOneGender word Masculine

// Always return feminine
let alwaysReturnFeminine word =
    alwaysReturnOneGender word Feminine
