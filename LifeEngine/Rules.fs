namespace LifeEngine

module Rules =

    type State = Dead|Alive

    let NextState currentState neighbourCount = 
        match neighbourCount with
        | 2 -> currentState
        | 3 -> Alive
        | _ -> Dead