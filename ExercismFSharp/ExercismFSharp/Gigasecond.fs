module Gigasecond

open System

let gigasecond (time : DateTime) =
    let gigasecondBirthday = time.AddSeconds 1000000000.
    gigasecondBirthday.Date