module Gigasecond

open System

let gigasecond (time : DateTime) =
    (time.AddSeconds 1000000000.).Date