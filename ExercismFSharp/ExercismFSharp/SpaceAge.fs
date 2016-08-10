module SpaceAge

open System

type Planet =
    | Earth
    | Mercury
    | Venus
    | Mars
    | Jupiter
    | Saturn
    | Uranus
    | Neptune

let spaceAge (planet : Planet) (seconds : decimal) =
    let planetYears planet years =
        match planet with
        | Mercury -> years / 0.2408467m
        | Venus -> years / 0.61519726m
        | Mars -> years / 1.8808158m
        | Jupiter -> years / 11.862615m
        | Saturn -> years / 29.447498m
        | Uranus -> years / 84.016846m
        | Neptune -> years / 164.79132m
        | _ -> years
    
    let earthYears = seconds / 31557600m
    let planetYears = planetYears planet earthYears
    Math.Round (planetYears, 2)
