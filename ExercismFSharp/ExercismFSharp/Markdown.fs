module Markdown

open System
open System.Text.RegularExpressions

let private encloseInTag tag value =
    sprintf "<%s>%s</%s>" tag value tag

let private replace deliminter replacementTag input =
    let pattern = sprintf "%s(.+?)%s" deliminter deliminter
    let replacement = sprintf "<%s>$1</%s>" replacementTag replacementTag
    Regex.Replace(input, pattern, replacement)

let private replaceBold line =
    replace "__" "em" line

let private replaceItalics line =
    replace "_" "i" line

let private processList (line : string) =
    let replaced =
        line
        |> replaceBold   
        |> replaceItalics

    match line.StartsWith "_" with
    | true  -> replaced |> encloseInTag "li"
    | false -> replaced |> encloseInTag "p" |> encloseInTag "li"

let private processHeading (line : string) =
    match line with
    | _ when line.StartsWith("###### ") -> line.[7..] |> encloseInTag "h6"
    | _ when line.StartsWith("##### ")  -> line.[6..] |> encloseInTag "h5"
    | _ when line.StartsWith("#### ")   -> line.[5..] |> encloseInTag "h4"
    | _ when line.StartsWith("### ")    -> line.[4..] |> encloseInTag "h3"
    | _ when line.StartsWith("## ")     -> line.[3..] |> encloseInTag "h2"
    | _ when line.StartsWith("# ")      -> line.[2..] |> encloseInTag "h1"
    | _ -> raise (Exception "Invalid syntax for header")

let private processLine line =
    line
    |> replaceBold
    |> replaceItalics
    |> encloseInTag "p"

let parse (markdown: string) =
    let mutable wasList = false

    let html =
        markdown.Split('\n')
        |> Array.map(fun line ->
            let resultLine =
                match line.[0] with
                | '*' -> processList line.[2..]
                | '#' -> processHeading line
                | _   -> processLine line

            match (wasList, line.[0] = '*') with
            | (true, false) -> wasList <- false
                               resultLine + "</ul>"
            | (false, true) -> wasList <- true
                               "<ul>" + resultLine
            | _             -> resultLine
        )
        |> Array.reduce (sprintf "%s%s")

    match wasList with
    | true  -> html + "</ul>"
    | false -> html