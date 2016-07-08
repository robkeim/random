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

// Assign all consonents to be masculine and all vowels to be feminine
let vowelsAndConsonents word =
    match word with
    | EndsWithRegexMatch "[bcdfghjklmnpqrstvwxyz]" _ -> Masculine
    | EndsWithRegexMatch "[aeéiouû]" _ -> Feminine
    | _ -> raise (System.ArgumentException("Unmatched word: " + word))

// Assign each given letter to be either masculine or feminine
let perLetter word =
    match word with
    | EndsWithRegexMatch "[abcdfghijklmopqrstuûvxyz]" _ -> Masculine
    | EndsWithRegexMatch "[eénw]" _ -> Feminine
    | _ -> raise (System.ArgumentException("Unmatched word: " + word))

// Rules for only feminine words from "A simplified list of endings" on this site:
// https://frenchtogether.com/french-nouns-gender/
// Feminine noun endings
// - The majority of words that end in -e or -ion.
// - Except words ending in -age, -ege, -é, or -isme (these endings often indicate masculine words).
// Masculine noun endings
// - Most words with other endings are masculine.
let frenchTogether word = 
    match word with
    | EndsWithRegexMatch "age" _ -> Masculine
    | EndsWithRegexMatch "ege" _ -> Masculine
    | EndsWithRegexMatch "é" _ -> Masculine
    | EndsWithRegexMatch "isme" _ -> Masculine
    | EndsWithRegexMatch "e" _ -> Feminine
    | EndsWithRegexMatch "ion" _ -> Feminine
    | _ -> Masculine

// List of roughly twenty five suffixes found on this site:
// http://www.fluentu.com/french/blog/french-gender-rules/
let fluentU word =
    match word with
    | EndsWithRegexMatch "age" _ -> Masculine
    | EndsWithRegexMatch "é" _ -> Masculine
    | EndsWithRegexMatch "ea?u" _ -> Masculine
    | EndsWithRegexMatch "isme" _ -> Masculine
    | EndsWithRegexMatch "ème" _ -> Masculine
    | EndsWithRegexMatch "ège" _ -> Masculine
    | EndsWithRegexMatch "[st]ion" _ -> Feminine
    | EndsWithRegexMatch "son" _ -> Feminine
    | EndsWithRegexMatch "[bcdfghjklmnpqrstvwxyz]" _ -> Masculine
    | _ -> Feminine