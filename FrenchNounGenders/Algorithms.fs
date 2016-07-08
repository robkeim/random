module Algorithms

open System.Text.RegularExpressions
open Types

let alwaysReturnOneGender word gender =
    { Word=word; Gender=gender }

// Always return masculine
let alwaysReturnMasculine word =
    alwaysReturnOneGender word Masculine

// Always return feminine
let alwaysReturnFeminine word =
    alwaysReturnOneGender word Feminine

let (|RegexMatch|_|) pattern input =
   let m = Regex.Match(input, "^" + pattern + "$")
   match Regex.Match(input, "^" + pattern + "$").Success with
   | true -> Some true
   | _ -> None

// Rules for only feminine words from "A simplified list of endings" on this site:
// https://frenchtogether.com/french-nouns-gender/
// Feminine noun endings
// - The majority of words that end in -e or -ion.
// - Except words ending in -age, -ege, -é, or -isme (these endings often indicate masculine words).
// Masculine noun endings
// - Most words with other endings are masculine.
let simplifiedListOfEndings word = 
    match word with
    | RegexMatch ".*age" _ -> { Word=word; Gender=Masculine }
    | RegexMatch ".*ege" _ -> { Word=word; Gender=Masculine }
    | RegexMatch ".*é" _ -> { Word=word; Gender=Masculine }
    | RegexMatch ".*isme" _ -> { Word=word; Gender=Masculine }
    | RegexMatch ".*e" _ -> { Word=word; Gender=Feminine }
    | RegexMatch ".*ion" _ -> { Word=word; Gender=Feminine }
    | _ -> { Word=word; Gender=Masculine }