module RobotName

open System

type mkRobot() =
    static let random = Random ()
    
    static let randomName() =
        let randomChar() =
            // 65 corresponds to the ASCII letter 'A'
            65 + (random.Next 26) |> char

        let randomDigit() =
            random.Next 10

        sprintf "%c%c%d%d%d" (randomChar ()) (randomChar ()) (randomDigit ()) (randomDigit ()) (randomDigit ())

    let mutable _name = randomName

    member this.reset =
        this.name <- randomName()

    member val name = randomName() with get, set

//let name (robot : mkRobot) =
//    robot.name

let reset (robot : mkRobot) =
    robot.reset
    robot