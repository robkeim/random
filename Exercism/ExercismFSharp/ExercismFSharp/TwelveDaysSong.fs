module TwelveDaysSong

open System

// This was my first approach which results in code that is a lot more simple, but there is duplication in the text which I eliminated in the version below
//let verse num =
//    let verses =
//        [
//            "On the first day of Christmas my true love gave to me, a Partridge in a Pear Tree.\n";
//            "On the second day of Christmas my true love gave to me, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the third day of Christmas my true love gave to me, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the fourth day of Christmas my true love gave to me, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the fifth day of Christmas my true love gave to me, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the sixth day of Christmas my true love gave to me, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the seventh day of Christmas my true love gave to me, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the eighth day of Christmas my true love gave to me, eight Maids-a-Milking, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the ninth day of Christmas my true love gave to me, nine Ladies Dancing, eight Maids-a-Milking, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the tenth day of Christmas my true love gave to me, ten Lords-a-Leaping, nine Ladies Dancing, eight Maids-a-Milking, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the eleventh day of Christmas my true love gave to me, eleven Pipers Piping, ten Lords-a-Leaping, nine Ladies Dancing, eight Maids-a-Milking, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n";
//            "On the twelfth day of Christmas my true love gave to me, twelve Drummers Drumming, eleven Pipers Piping, ten Lords-a-Leaping, nine Ladies Dancing, eight Maids-a-Milking, seven Swans-a-Swimming, six Geese-a-Laying, five Gold Rings, four Calling Birds, three French Hens, two Turtle Doves, and a Partridge in a Pear Tree.\n"
//        ]
//
//    verses.[num - 1]

let verse num =
    let cardinal num =
        match num with
        | 1 -> "first"
        | 2 -> "second"
        | 3 -> "third"
        | 4 -> "fourth"
        | 5 -> "fifth"
        | 6 -> "sixth"
        | 7 -> "seventh"
        | 8 -> "eighth"
        | 9 -> "ninth"
        | 10 -> "tenth"
        | 11 -> "eleventh"
        | 12 -> "twelfth"
        | _ -> raise (ArgumentOutOfRangeException "Invalid value")

    let nthGift =
        function
        | 1  -> "and a Partridge in a Pear Tree"
        | 2  -> "two Turtle Doves"
        | 3  -> "three French Hens"
        | 4  -> "four Calling Birds"
        | 5  -> "five Gold Rings"
        | 6  -> "six Geese-a-Laying"
        | 7  -> "seven Swans-a-Swimming"
        | 8  -> "eight Maids-a-Milking"
        | 9  -> "nine Ladies Dancing"
        | 10 -> "ten Lords-a-Leaping"
        | 11 -> "eleven Pipers Piping"
        | 12 -> "twelve Drummers Drumming"
        | _ -> failwith "invalid day of Christmas"

    let head = sprintf "On the %s day of Christmas my true love gave to me" (cardinal num)

    let tail =
        match num with
        | 1 -> ", a Partridge in a Pear Tree"
        | _ -> [num .. -1 .. 1] |> List.fold (fun acc elem -> sprintf "%s, %s" acc (nthGift elem)) ""

    sprintf "%s%s.\n" head tail

let verses first last =
    [first..last]
    |> Seq.map (fun n -> sprintf "%s\n" (verse n))
    |> String.Concat