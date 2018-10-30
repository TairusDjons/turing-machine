module TuringMachine

open System.Collections.Generic
open TuringMachine.Internal

type StateName = NoWhiteSpaceString

let StateName (value: string) : StateName option = NoWhiteSpaceString value

type Symbol = char option

type Direction = | Left = -1 | Pause = 0 | Right = 1

type CommandState = {
    Name : StateName
    Symbol : Symbol
}

type CommandAction = {
    NewSymbol : Symbol
    Direction : Direction
    NextStateName : StateName
}

type Command = {
    CommandState : CommandState
    CommandAction : CommandAction
}

type Memory = Dictionary<int, Symbol>
type Commands = Dictionary<CommandState, CommandAction>

let defaultStartStateName = "q0" |> StateName |> Option.get
let defaultMemory = Memory()
let defaultCommand = {
    CommandState = {
        Name = "q0" |> StateName |> Option.get
        Symbol = '1' |> Some
    }
    CommandAction = {
        NewSymbol = '0' |> Some
        Direction = Direction.Right
        NextStateName = "q0" |> StateName |> Option.get
    }
}
let defaultCommands = Commands([defaultCommand.CommandState, defaultCommand.CommandAction] |> Map.ofList)

type Machine(?commands, ?memory, ?stateName) =
    
    let mutable stateName = defaultArg stateName defaultStartStateName
    let mutable memoryIndex = 0
    let mutable memory = defaultArg memory defaultMemory
    let mutable commands = defaultArg commands defaultCommands

    member this.StateName
        with get() = stateName
        and set(value) = stateName <- value

    member this.MemoryIndex
        with get() = memoryIndex
        and set(value) = memoryIndex <- value

    member this.Memory
        with get() = memory
        and set(value) = memory <- value

    member this.Commands
        with get() = commands
        and set(value) = commands <- value
    
    member this.Reset memory stateName : unit =
        this.StateName <- stateName
        this.Memory <- memory
        
    member this.Step() : bool =
        let success, action = commands.TryGetValue { Name = this.StateName; Symbol = this.Memory.[this.MemoryIndex] }
        if success
            then
                this.StateName <- action.NextStateName
                this.Memory.[this.MemoryIndex] <- action.NewSymbol
                this.MemoryIndex <- this.MemoryIndex + int action.Direction
                true
            else
                false

    member this.Execute : unit = while this.Step() do ()
