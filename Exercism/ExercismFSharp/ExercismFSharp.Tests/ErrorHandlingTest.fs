﻿module ErrorHandlingTest

open NUnit.Framework

open ErrorHandling

// Custom class that implements IDisposable
type Resource() = 
    let mutable disposed = false

    member this.Disposed() = disposed

    interface System.IDisposable with
        member this.Dispose() =
            disposed <- true

// Throwing exceptions is not the preferred approach to handling errors in F#, 
// but it becomes relevant when you use .NET framework methods from your F# code
[<Test>]
let ``Throwing exception`` () =    
    Assert.That((fun () -> handleErrorByThrowingException() |> ignore), Throws.Exception)

// A better approach than exceptions is to use the Option<'T> discriminated union. 
// If the function is successful, Some x is returned (with x being the value).
// Upon failure, None is returned. The caller can then pattern match on the
// returned value. As Option<'T> is a discriminated union, the user is forced to
// consider both possible outputs: success and failure.
[<Test>]
let ``Returning Option<'T>`` () =
    let successResult = handleErrorByReturningOption "1"
    Assert.That(successResult, Is.EqualTo(Some 1))
    
    let failureResult = handleErrorByReturningOption "a"
    Assert.That(failureResult, Is.EqualTo(None))

// If the caller is also interested what error occured, the Option<'T> type does not suffice.
// In that case, one usually defines a new discriminated union: Result<'T, 'TError>.
// This discriminated union has two possible cases: Ok and Error, which contain data
// of type 'T and 'TError respectively. Note that these types can be different, so
// you are free to return an integer upon success and a string upon failure.
//
// Note: F# 4.1 will include the Result<'T, 'TError> type.
// See: https://blogs.msdn.microsoft.com/dotnet/2016/07/25/a-peek-into-f-4-1/
[<Test>]
let ``Returning Result<'TSuccess, 'TFailure>`` () =
    let successResult = handleErrorByReturningResult "1"
    Assert.True((successResult = Ok 1))
    
    let failureResult = handleErrorByReturningResult "a"
    Assert.True((failureResult = Error "Could not convert input to integer"))

// In the previous test, we defined a Result<'T, 'TError> type. The next step is
// to be able to execute several validations in sequence. The problem that quickly
// becomes apparent when you try to do this, is that the output of one validation
// function (which is of type Result<'T, 'TError>), cannot be used as the input of
// another validation function (which expects a parameter of type 'T). 
//
// To solve this problem, you can use the railway-oriented programming model. In this
// model, functions are likened to railway switches that have one or two input tracks 
// and two outputs tracks. This maps perfectly to our validation functions, with one 
// output track being mapped to the success result and the other to the error result.
// In railway-oriented programming, the trick is that once you are on the error track,
// you can never return to success track.
//
// In this test, your task is to write a function "bind", that allows you to combine
// two functions that take a T' instance and return a Result<'T, 'TError> instance.
[<Test>]
let ``Using railway-oriented programming`` () =
    let validate1 x = if x > 5 then Ok x else Error "Input less than or equal to five"
    let validate2 x = if x < 10 then Ok x else Error "Input greater than or equal to ten"
    let validate3 x = if x % 2 <> 0 then Ok x else Error "Input is not odd"
    
    // Combine the validations. The result should be a function that takes an int parameter
    // and returns a Result<int, string> value
    let combinedValidation =
        validate1
        >> bind validate2
        >> bind validate3

    let firstValidationFailureResult = combinedValidation 1            
    Assert.True((firstValidationFailureResult = Error "Input less than or equal to five"))

    let secondValidationFailureResult = combinedValidation 23          
    Assert.True((secondValidationFailureResult = Error "Input greater than or equal to ten"))

    let thirdValidationFailureResult = combinedValidation 8        
    Assert.True((thirdValidationFailureResult = Error "Input is not odd"))

    let successResult = combinedValidation 7        
    Assert.True((successResult = Ok 7))
    
// If you are dealing with code that throws exceptions, you should ensure that any
// disposable resources that are used are being disposed of
[<Test>]
let ``Cleaning up disposables when throwing exception`` () =    
    let resource = new Resource()

    Assert.That((fun () -> cleanupDisposablesWhenThrowingException resource |> ignore), Throws.Exception)
    Assert.True(resource.Disposed())