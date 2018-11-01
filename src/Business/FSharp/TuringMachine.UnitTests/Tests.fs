module TuringMachine.UnitTests

open System
open FParsec
open Xunit
open TuringMachine
open TuringMachine.Format

let exampleString =
    """
    
0 1 w r 1

1 1 z r 0

    """

let simpleString = "0 1 w r 0"
let simpleCommand : Command = {
    CommandState = {
        Number = 0
        Symbol = '1'
    }
    CommandAction = {
        NewSymbol = 'w'
        Direction = Right
        NextStateNumber = 0
    }
}

let prun rule str = run rule str |> unwrapParserResult

[<Fact>]
let parseSymbolWorks() : unit =
    Assert.Equal('0', prun symbolRule "0")
    Assert.NotEqual('0', prun symbolRule "1")
    Assert.Throws(fun () -> prun symbolRule " " |> ignore) |> ignore

[<Fact>]
let parseNumberWorks() : unit =
    Assert.Equal(0, prun stateNumberRule "0")
    Assert.NotEqual(0, prun stateNumberRule "1")
    Assert.Throws(fun () -> prun stateNumberRule "rofl" |> ignore) |> ignore

[<Fact>]
let parseStateWorks() : unit =
    Assert.Equal({ Number = 0; Symbol = '1' }, prun commandStateRule "0 1")
    Assert.NotEqual({ Number = 1; Symbol = '1' }, prun commandStateRule "0 1")
    Assert.Throws(fun () -> prun commandStateRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseActionWorks() : unit =
    Assert.Equal({ NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }, prun commandActionRule "0 l 1")
    Assert.NotEqual({ NewSymbol = '1'; Direction = Left; NextStateNumber = 0 }, prun commandActionRule "0 p 2")
    Assert.Throws(fun () -> prun commandActionRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseCommandWorks() : unit =
    Assert.Equal(
        { CommandState = { Number = 1; Symbol = '2'; }; CommandAction = { NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }},
        prun commandRule "1 2 0 l 1")
    Assert.NotEqual(
        { CommandState = { Number = 1; Symbol = '2'; }; CommandAction = { NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }},
        prun commandRule "1 2 0 r 1")
    Assert.Throws(fun () -> prun commandRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseCommandsWorks() : unit =
    Assert.Equal<Command list>(
        [
            { CommandState = { Number = 0; Symbol = 'a'; }; CommandAction = { NewSymbol = 'z'; Direction = Right; NextStateNumber = 1 }}
            { CommandState = { Number = 1; Symbol = 'a'; }; CommandAction = { NewSymbol = 'y'; Direction = Right; NextStateNumber = 0 }}
        ],
        prun commandsRule "0 a z r 1 \r\n 1 a y r 0 \r\n")
    Assert.NotEqual<Command list>(
        [
            { CommandState = { Number = 0; Symbol = 'a'; }; CommandAction = { NewSymbol = 'z'; Direction = Pause; NextStateNumber = 1 }}
            { CommandState = { Number = 1; Symbol = 'a'; }; CommandAction = { NewSymbol = 'y'; Direction = Pause; NextStateNumber = 0 }}
        ],
        prun commandsRule "0 a z r 1 \r\n 1 a y r 0 \r\n")
    Assert.Throws(fun () -> prun commandRule "abra kadabra" |> ignore) |> ignore
