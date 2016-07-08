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
   match Regex.Match(input, "^" + pattern + "$").Success with
   | true -> Some true
   | _ -> None

let (|EndsWithRegexMatch|_|) pattern input =
   match Regex.Match(input, pattern + "$").Success with
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
    | EndsWithRegexMatch "age" _ -> { Word=word; Gender=Masculine }
    | EndsWithRegexMatch "ege" _ -> { Word=word; Gender=Masculine }
    | EndsWithRegexMatch "é" _ -> { Word=word; Gender=Masculine }
    | EndsWithRegexMatch "isme" _ -> { Word=word; Gender=Masculine }
    | EndsWithRegexMatch "e" _ -> { Word=word; Gender=Feminine }
    | EndsWithRegexMatch "ion" _ -> { Word=word; Gender=Feminine }
    | _ -> { Word=word; Gender=Masculine }