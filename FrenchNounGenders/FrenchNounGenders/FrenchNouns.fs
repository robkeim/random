module FrenchNouns

open System.IO
open System.Text

let rawNounList =
    File.ReadAllText(".\NounList.txt", Encoding.UTF7)