module RobotSimulator

open System

type Bearing =
    | North
    | East
    | South
    | West

type Robot = { bearing : Bearing; location : int * int }

let createRobot bearing location =
    { bearing = bearing; location = location }

let turnRight robot =
    { robot with bearing = match robot.bearing with
                            | Bearing.North -> Bearing.East
                            | Bearing.East -> Bearing.South
                            | Bearing.South -> Bearing.West
                            | Bearing.West -> Bearing.North
    }

let turnLeft robot =
    { robot with bearing = match robot.bearing with
                            | Bearing.North -> Bearing.West
                            | Bearing.West -> Bearing.South
                            | Bearing.South -> Bearing.East
                            | Bearing.East -> Bearing.North
    }

let private advance robot =
    { robot with location = match robot.bearing with
                            | Bearing.North -> (fst robot.location, snd robot.location + 1)
                            | Bearing.West -> (fst robot.location - 1, snd robot.location)
                            | Bearing.South -> (fst robot.location, snd robot.location - 1)
                            | Bearing.East -> (fst robot.location + 1, snd robot.location)
    }   

let simulate robot movements =
    movements
    |> Seq.fold (fun r m ->
        match m with
        | 'L' -> turnLeft r
        | 'R' -> turnRight r
        | 'A' -> advance r
        | _ -> raise (Exception "unsupported movement")
        )
        robot