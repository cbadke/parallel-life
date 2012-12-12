namespace LifeEngine

module Rules =

    type State = Dead|Alive

    let NextState currentState count = 
        match count with
        | 2 -> currentState
        | 3 -> Alive
        | _ -> Dead