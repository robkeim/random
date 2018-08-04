module Palindrome

let private getPalindromes lower upper =
    let makeSeq n =
        [n..upper]
        |> Seq.map (fun m -> ((n, m), n * m))

    let isPalindrome n =
        let nString =
            n
            |> string
        let reversedString =
            nString
            |> Seq.toArray
            |> Array.rev
            |> System.String
        nString = reversedString

    [lower..upper]
    |> Seq.map makeSeq
    |> Seq.concat
    |> Seq.filter (fun ((_, _), a) -> isPalindrome a)

let filterFactors factors =
    let head =
        factors
        |> Seq.head
        |> snd

    let matchingFactors =
        factors
        |> Seq.takeWhile (fun ((_, _), a) -> a = head)
        |> Seq.map (fun (a, _) -> a)
        |> Seq.toList

    (head, matchingFactors)

let largestPalindrome lower upper =
    let factors =
        getPalindromes lower upper
        |> Seq.sortByDescending (fun ((_, _), a) -> a)

    filterFactors factors

let smallestPalindrome lower upper =
    let factors =
        getPalindromes lower upper
        |> Seq.sortBy (fun ((_, _), a) -> a)

    filterFactors factors