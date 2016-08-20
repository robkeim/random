module Say

open System

let private oneToNinetyNine n =
    match n % 100L with
    | 1L -> "one"
    | 2L -> "two"
    | 3L -> "three"
    | 4L -> "four"
    | 5L -> "five"
    | 6L -> "six"
    | 7L -> "seven"
    | 8L -> "eight"
    | 9L -> "nine"
    | 10L -> "ten"
    | 11L -> "eleven"
    | 12L -> "twelve"
    | 13L -> "thirteen"
    | 14L -> "fourteen"
    | 15L -> "fifteen"
    | 16L -> "sixteen"
    | 17L -> "seventeen"
    | 18L -> "eighteen"
    | 19L -> "nineteen"
    | 20L -> "twenty"
    | 21L -> "twenty-one"
    | 22L -> "twenty-two"
    | 23L -> "twenty-three"
    | 24L -> "twenty-four"
    | 25L -> "twenty-five"
    | 26L -> "twenty-six"
    | 27L -> "twenty-seven"
    | 28L -> "twenty-eight"
    | 29L -> "twenty-nine"
    | 30L -> "thirty"
    | 31L -> "thirty-one"
    | 32L -> "thirty-two"
    | 33L -> "thirty-three"
    | 34L -> "thirty-four"
    | 35L -> "thirty-five"
    | 36L -> "thirty-six"
    | 37L -> "thirty-seven"
    | 38L -> "thirty-eight"
    | 39L -> "thirty-nine"
    | 40L -> "forty"
    | 41L -> "forty-one"
    | 42L -> "forty-two"
    | 43L -> "forty-three"
    | 44L -> "forty-four"
    | 45L -> "forty-five"
    | 46L -> "forty-six"
    | 47L -> "forty-seven"
    | 48L -> "forty-eight"
    | 49L -> "forty-nine"
    | 50L -> "fifty"
    | 51L -> "fifty-one"
    | 52L -> "fifty-two"
    | 53L -> "fifty-three"
    | 54L -> "fifty-four"
    | 55L -> "fifty-five"
    | 56L -> "fifty-six"
    | 57L -> "fifty-seven"
    | 58L -> "fifty-eight"
    | 59L -> "fifty-nine"
    | 60L -> "sixty"
    | 61L -> "sixty-one"
    | 62L -> "sixty-two"
    | 63L -> "sixty-three"
    | 64L -> "sixty-four"
    | 65L -> "sixty-five"
    | 66L -> "sixty-six"
    | 67L -> "sixty-seven"
    | 68L -> "sixty-eight"
    | 69L -> "sixty-nine"
    | 70L -> "seventy"
    | 71L -> "seventy-one"
    | 72L -> "seventy-two"
    | 73L -> "seventy-three"
    | 74L -> "seventy-four"
    | 75L -> "seventy-five"
    | 76L -> "seventy-six"
    | 77L -> "seventy-seven"
    | 78L -> "seventy-eight"
    | 79L -> "seventy-nine"
    | 80L -> "eighty"
    | 81L -> "eighty-one"
    | 82L -> "eighty-two"
    | 83L -> "eighty-three"
    | 84L -> "eighty-four"
    | 85L -> "eighty-five"
    | 86L -> "eighty-six"
    | 87L -> "eighty-seven"
    | 88L -> "eighty-eight"
    | 89L -> "eighty-nine"
    | 90L -> "ninety"
    | 91L -> "ninety-one"
    | 92L -> "ninety-two"
    | 93L -> "ninety-three"
    | 94L -> "ninety-four"
    | 95L -> "ninety-five"
    | 96L -> "ninety-six"
    | 97L -> "ninety-seven"
    | 98L -> "ninety-eight"
    | 99L -> "ninety-nine"
    | _ -> raise (Exception "unreachable code")

let private oneHundredToOneThousand n =
    let hundreds = oneToNinetyNine (n / 100L)
    let remainder = n % 100L

    match remainder with
    | 0L -> sprintf "%s hundred" hundreds
    | _ -> sprintf "%s hundred %s" hundreds (oneToNinetyNine remainder)

let private getValue n place =
    match n with
    | 0L -> None
    | _ when n < 100L -> Some (sprintf "%s%s" (oneToNinetyNine n) place)
    | _ when n < 1000L -> Some (sprintf "%s%s" (oneHundredToOneThousand n) place)
    | _ -> raise (Exception "unreachable code")

let getString n =
    [
        getValue ((n / 1000000000L) % 1000L) " billion";
        getValue ((n / 1000000L) % 1000L) " million";
        getValue ((n / 1000L) % 1000L) " thousand";
        getValue ((n / 100L) % 10L) " hundred";
        getValue (n % 100L) "";
    ]
    |> List.choose id
    |> Seq.reduce (sprintf "%s %s")

let inEnglish n =
    match n with
    | _ when n < 0L -> None
    | _ when n >= 1000000000000L -> None
    | _ when n = 0L -> Some "zero"
    | _ -> Some (getString n)