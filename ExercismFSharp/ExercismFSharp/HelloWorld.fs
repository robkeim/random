// Problems from here:
// http://exercism.io/languages/fsharp

module HelloWorld

let hello name =
    match name with
    | Some name -> sprintf "Hello, %s!" name
    | None -> "Hello, World!"