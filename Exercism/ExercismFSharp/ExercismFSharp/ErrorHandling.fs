module ErrorHandling

open System

let handleErrorByThrowingException() =
    raise (Exception "error")

let handleErrorByReturningOption input =
    let couldParse, parsedInt = Int32.TryParse input

    match couldParse with
    | true -> Some parsedInt
    | false -> None

type Result<'TSuccess, 'TError> =
    | Ok of 'TSuccess
    | Error of 'TError

let handleErrorByReturningResult input =
    let couldParse, parsedInt = Int32.TryParse input

    match couldParse with
    | true -> Ok parsedInt
    | false -> Error "Could not convert input to integer"

// More info on railway oriented programming can be found here
// http://fsharpforfunandprofit.com/posts/recipe-part2/
let bind switchFunction =
    fun twoTrackInput ->
        match twoTrackInput with
        | Ok ok -> switchFunction ok
        | Error error -> Error error

let cleanupDisposablesWhenThrowingException (resource : IDisposable) =
    use r = resource
    raise (Exception "error")