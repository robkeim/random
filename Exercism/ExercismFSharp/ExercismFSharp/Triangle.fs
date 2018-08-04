module Triangle

open System

type TriangleKind =
    | Equilateral
    | Isosceles
    | Scalene

let kind s1 s2 s3 =
    // From the hint:
    // The sum of the lengths of any two sides of a triangle always exceeds or is equal to the length of the third side,
    // a principle known as the triangle inequality
    let isImpossible s1 s2 s3 =
        s1 <= 0m ||
        s2 <= 0m ||
        s3 <= 0m ||
        s1 + s2 <= s3 ||
        s1 + s3 <= s2 ||
        s2 + s3 <= s1

    let isEquilaterial s1 s2 s3 =
        s1 = s2 && s2 = s3

    let isIsoceles s1 s2 s3 =
        s1 = s2 || s1 = s3 || s2 = s3

    match (s1, s2, s3) with
    | _ when isImpossible s1 s2 s3   -> raise (InvalidOperationException "Invalid triangle sides")
    | _ when isEquilaterial s1 s2 s3 -> TriangleKind.Equilateral
    | _ when isIsoceles     s1 s2 s3 -> TriangleKind.Isosceles
    | _                              -> TriangleKind.Scalene