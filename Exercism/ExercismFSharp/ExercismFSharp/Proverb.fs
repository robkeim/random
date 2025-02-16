﻿module Proverb

let line num =
    [
        "For want of a nail the shoe was lost.";
        "For want of a shoe the horse was lost.";
        "For want of a horse the rider was lost.";
        "For want of a rider the message was lost.";
        "For want of a message the battle was lost.";
        "For want of a battle the kingdom was lost.";
        "And all for the want of a horseshoe nail."
    ].[num - 1]

let proverb =
    [1..7]
    |> List.map line
    |> List.reduce (sprintf "%s\n%s")