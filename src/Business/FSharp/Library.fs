[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TuringMachine

open System.Collections.Generic
open TuringMachine.Internal

type StateName = NoWhiteSpaceString

let StateName (value: string) : StateName option = NoWhiteSpaceString value

type Symbol = char

type Direction = | Left = -1 | Pause = 0 | Right = 1

[<Literal>]
let Left = Direction.Left

[<Literal>]
let Pause = Direction.Pause

[<Literal>]
let Right = Direction.Right

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

type MemoryIndex = int

let defaultMemoryChar = '#'

type Memory(?memoryChar) = 
    let dict : Dictionary<MemoryIndex, Symbol> = Dictionary<MemoryIndex, Symbol>()
    member val MemoryChar = defaultArg memoryChar defaultMemoryChar with get, set
    member this.Item
        with get index =
            let success, result = dict.TryGetValue(index)
            if success
                then result
                else this.MemoryChar
        and set index value = dict.[index] <- value
    member this.Clear() = dict.Clear()

type Commands = Dictionary<CommandState, CommandAction>

let defaultStartStateName = "q0" |> StateName |> Option.get
let defaultMemory = Memory()
let defaultCommand = {
    CommandState = {
        Name = "q0" |> StateName |> Option.get
        Symbol = '1'
    }
    CommandAction = {
        NewSymbol = '0'
        Direction = Direction.Right
        NextStateName = "q0" |> StateName |> Option.get
    }
}
let defaultCommands = Commands([defaultCommand.CommandState, defaultCommand.CommandAction] |> Map.ofList)

type Machine(?commands, ?memory, ?stateName) =
    member val StateName = defaultArg stateName defaultStartStateName with get, set
    member val MemoryIndex = 0 with get, set
    member val Memory = defaultArg memory defaultMemory with get, set
    member val Commands = defaultArg commands defaultCommands with get, set
    member this.Reset memory stateName : unit =
        this.StateName <- stateName
        this.Memory <- memory
    member this.Step() : bool =
        let success, action = this.Commands.TryGetValue { Name = this.StateName; Symbol = this.Memory.[this.MemoryIndex] }
        if success
            then
                this.StateName <- action.NextStateName
                this.Memory.[this.MemoryIndex] <- action.NewSymbol
                this.MemoryIndex <- this.MemoryIndex + int action.Direction
                true
            else
                false
    member this.Execute : unit = while this.Step() do ()
