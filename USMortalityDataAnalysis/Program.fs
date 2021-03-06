﻿// This data was originally download from here:
// https://www.kaggle.com/cdc/mortality

open System
open Parsing
open Stats

[<EntryPoint>]
let main argv =
    let deathRecords = GetDeathRecords
    GetStats deathRecords

    printfn "\ndone!"
    ignore (Console.ReadLine())
    0 // return an integer exit code
