module Algorithms

open System.Text.RegularExpressions
open Types

let (|RegexMatch|_|) pattern input =
   match Regex.Match(input, "^" + pattern + "$").Success with
   | true -> Some true
   | _ -> None

let (|EndsWithRegexMatch|_|) pattern input =
   match Regex.Match(input, pattern + "$").Success with
   | true -> Some true
   | _ -> None

// Always return masculine
let alwaysReturnMasculine word =
    Masculine

// Always return feminine
let alwaysReturnFeminine word =
    Feminine

// Rules for only feminine words from "A simplified list of endings" on this site:
// https://frenchtogether.com/french-nouns-gender/
// Feminine noun endings
// - The majority of words that end in -e or -ion.
// - Except words ending in -age, -ege, -é, or -isme (these endings often indicate masculine words).
// Masculine noun endings
// - Most words with other endings are masculine.
let simplifiedListOfEndings word = 
    match word with
    | EndsWithRegexMatch "age" _ -> Masculine
    | EndsWithRegexMatch "ege" _ -> Masculine
    | EndsWithRegexMatch "é" _ -> Masculine
    | EndsWithRegexMatch "isme" _ -> Masculine
    | EndsWithRegexMatch "e" _ -> Feminine
    | EndsWithRegexMatch "ion" _ -> Feminine
    | _ -> Masculine