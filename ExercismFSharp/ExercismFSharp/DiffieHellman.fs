module DiffieHellman

open System
open System.Numerics

let privateKey primeP =
    let random = Random (Guid.NewGuid().GetHashCode())
    let primeString = primeP |> string

    // BigInt does not support a random, so I have to generate one myself. For simplification, I'm ensuring the number
    // is smaller by ensuring the first digit is smaller which does not use the entire range of possible values
    // (ex: for 25, I'll only generate numbers through 19 eliminating 20-24 which are also valid)
    let result =
        Seq.append
            ((random.Next (primeString |> Seq.head |> string |> Int32.Parse)) |> Seq.singleton)
            (Seq.init ((primeString |> Seq.length) - 1) (fun _ -> random.Next 10))
        |> Seq.fold (sprintf "%s%i") String.Empty
        |> BigInteger.Parse

    // Ensure we never return 0I
    result + 1I

let publicKey primeP primeG privateKey =
    BigInteger.ModPow(primeG, privateKey, primeP)

let secret primeP publicKey privateKey =
    BigInteger.ModPow(publicKey, privateKey, primeP)