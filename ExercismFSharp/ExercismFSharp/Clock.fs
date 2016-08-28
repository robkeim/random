module Clock

type Clock = { minutes : int }

let rec private modulo n m =
    match n with
    | _ when n < 0 -> modulo (n + m) m
    | _ -> n % m

let mkClock hours minutes =
    { minutes = modulo (hours * 60 + minutes) 1440 }

let display clock =
    sprintf "%02i:%02i" (clock.minutes / 60) (clock.minutes % 60)

let private updateTime op minutes offset =
    modulo (op minutes offset) 1440

let add minutes clock =
    { minutes = updateTime (+) clock.minutes minutes }

let subtract minutes clock =
    { minutes = updateTime (-) clock.minutes minutes }