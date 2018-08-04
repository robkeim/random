module Grep

open System
open System.IO

let private hasFlag flag (flags : string) =
    flags.Contains (sprintf "-%c" flag)

let private printLineNumbers = hasFlag 'n'
let private printOnlyFileNames = hasFlag 'l'
let private caseInsensitveMatch = hasFlag 'i'
let private negativeMatch = hasFlag 'v'
let private matchEntireLine = hasFlag 'x'

type private Line =
    {
        Filename : string
        LineNum : int
        Text : string
    }

let private getLines files =
    files
    |> List.map (fun file ->
        Seq.zip
            (Seq.initInfinite (fun n -> n + 1))
            (file |> File.ReadAllLines)
        |> Seq.map (fun (lineNum, text) -> { Filename = file; LineNum = lineNum; Text = text })
    )
    |> Seq.concat

let private getMatches (pattern : string) (flags : string) lines =
    lines
    |> Seq.filter (fun line ->
        match caseInsensitveMatch flags, negativeMatch flags, matchEntireLine flags with
        | true, _, _ -> line.Text.ToLowerInvariant().Contains (pattern.ToLowerInvariant())
        | _, true, _ -> not (line.Text.Contains pattern)
        | _, _, true -> line.Text = pattern
        | _          -> line.Text.Contains pattern
    )

let private outputLines (flags : string) multipleFiles lines =
    let filteredLines =
        match printOnlyFileNames flags with
        | true  -> lines |> Seq.distinctBy(fun line -> line.Filename)
        | false -> lines

    filteredLines
    |> Seq.map (fun line ->
        match printOnlyFileNames flags, multipleFiles, printLineNumbers flags with
        | true, _, _     -> line.Filename
        | _, true, true  -> sprintf "%s:%i:%s" line.Filename line.LineNum line.Text
        | _, false, true -> sprintf "%i:%s" line.LineNum line.Text
        | _, true, _     -> sprintf "%s:%s" line.Filename line.Text
        | _, false, _    -> line.Text
    )
    |> Seq.fold (sprintf "%s%s\n") String.Empty

let grep pattern flags files =
    files
    |> getLines
    |> getMatches pattern flags
    |> outputLines flags (files |> Seq.length > 1)