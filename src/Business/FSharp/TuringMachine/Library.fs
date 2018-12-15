namespace TuringMachine

open System.Collections.Generic

type StateNumber = int32

type Symbol = char

type Direction = | Left = -1 | Pause = 0 | Right = 1

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Direction =
    [<Literal>]
    let Left = Direction.Left

    [<Literal>]
    let Pause = Direction.Pause

    [<Literal>]
    let Right = Direction.Right

type CommandState = {
    Number : StateNumber
    Symbol : Symbol
}

type CommandAction = {
    NewSymbol : Symbol
    Direction : Direction
    NextStateNumber : StateNumber
}

type Command = {
    State : CommandState
    Action : CommandAction
}

type MemoryIndex = int

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Memory =
    let defaultPolyfillChar = '#'

type Memory(?memoryChar) = 
    let dict : Dictionary<MemoryIndex, Symbol> = Dictionary<MemoryIndex, Symbol>()
    member val PolyfillChar = defaultArg memoryChar defaultPolyfillChar with get, set
    member this.Item
        with get index =
            let success, result = dict.TryGetValue(index)
            if success
                then result
                else this.PolyfillChar
        and set index value = dict.[index] <- value
    member __.Clear() = dict.Clear()
    new() = Memory(defaultPolyfillChar)

type Commands(?commands : IEnumerable<Command>) =
    let dict : Dictionary<CommandState, CommandAction> = Dictionary<CommandState, CommandAction>()
    member __.Item
        with get index = dict.[index]
        and set index value = dict.[index] <- value
    member __.Clear() = dict.Clear()
    member this.Update (commands : IEnumerable<Command>) =
        this.Clear()
        for command in commands do this.[command.State] <- command.Action
    member __.TryGetAction state = dict.TryGetValue(state)

[<AutoOpen>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module MachineModule =
    let defaultStartStateNumber = 0
    let defaultMemory = Memory()
    let defaultCommand = {
        State = {
            Number = 0
            Symbol = '1'
        }
        Action = {
            NewSymbol = '0'
            Direction = Direction.Right
            NextStateNumber = 0
        }
    }
    let defaultCommands = Commands([defaultCommand])

type Machine(?memory, ?stateNumber, ?commands) =
    member val StateNumber = defaultArg stateNumber defaultStartStateNumber with get, set
    member val MemoryIndex = 0 with get, set
    member val Memory = defaultArg memory defaultMemory with get, set
    member val Commands = defaultArg commands defaultCommands with get, set
    member this.Reset memory stateName : unit =
        this.StateNumber <- stateName
        this.Memory <- memory
    member this.Step() : bool =
        let success, action = this.Commands.TryGetAction { Number = this.StateNumber; Symbol = this.Memory.[this.MemoryIndex] }
        if success
            then
                this.StateNumber <- action.NextStateNumber
                this.Memory.[this.MemoryIndex] <- action.NewSymbol
                this.MemoryIndex <- this.MemoryIndex + int action.Direction
                true
            else
                false
    member this.Execute() : unit = while this.Step() do ()
    new(memory, stateNumber) = Machine(memory, stateNumber, defaultCommands)
