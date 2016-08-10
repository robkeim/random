module Gigasecond

open System

let gigasecond time =
    let gigasecond = TimeSpan.FromSeconds 1000000000.
    let gigasecondBirthday = DateTime.op_Addition (time, gigasecond)
    DateTime (gigasecondBirthday.Year, gigasecondBirthday.Month, gigasecondBirthday.Day)