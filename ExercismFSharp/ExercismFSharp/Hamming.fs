module Hamming

let compute strand1 strand2 =
    Seq.map2
        (fun s1 s2 -> s1 = s2)
        strand1 strand2
    |> Seq.filter (fun value -> value = false)
    |> Seq.length