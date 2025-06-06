﻿module ScrabbleScoreTest

open NUnit.Framework
open ScrabbleScore
  
[<Test>]
let ``Empty word scores zero`` () =
    Assert.That(score "", Is.EqualTo(0))

[<Test>]
let ``Whitespace scores zero`` () =
    Assert.That(score " \t\n", Is.EqualTo(0))

[<Test>]
let ``Scores very short word`` () =
    Assert.That(score "a", Is.EqualTo(1))

[<Test>]
let ``Scores other very short word`` () =
    Assert.That(score "f", Is.EqualTo(4))

[<Test>]
let ``Simple word scores the number of letters`` () =
    Assert.That(score "street", Is.EqualTo(6))

[<Test>]
let ``Complicated word scores more`` () =
    Assert.That(score "quirky", Is.EqualTo(22))

[<Test>]
let ``Scores are case insensitive`` () =
    Assert.That(score "OXYPHENBUTAZONE", Is.EqualTo(41))

[<Test>]
let ``Entire alphabet`` () =
    Assert.That(score "abcdefghijklmnopqrstuvwxyz", Is.EqualTo(87))
