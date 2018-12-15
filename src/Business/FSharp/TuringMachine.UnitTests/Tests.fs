module TuringMachine.UnitTests

open FParsec
open Xunit
open TuringMachine
open TuringMachine.Format

let prun rule str = run rule str |> unwrapParserResult

[<Fact>]
let parseSymbolWorks() : unit =
    Assert.Equal('0', prun symbolRule "0")
    Assert.NotEqual('0', prun symbolRule "1")
    Assert.Throws<UnwrapErrorException>(fun () -> prun symbolRule " " |> ignore) |> ignore

[<Fact>]
let parseNumberWorks() : unit =
    Assert.Equal(0, prun stateNumberRule "0")
    Assert.NotEqual(0, prun stateNumberRule "1")
    Assert.Throws<UnwrapErrorException>(fun () -> prun stateNumberRule "rofl" |> ignore) |> ignore

[<Fact>]
let parseStateWorks() : unit =
    Assert.Equal({ Number = 0; Symbol = '1' }, prun commandStateRule "0 1")
    Assert.NotEqual({ Number = 1; Symbol = '1' }, prun commandStateRule "0 1")
    Assert.Throws<UnwrapErrorException>(fun () -> prun commandStateRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseActionWorks() : unit =
    Assert.Equal({ NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }, prun commandActionRule "1 0 l")
    Assert.NotEqual({ NewSymbol = '1'; Direction = Left; NextStateNumber = 0 }, prun commandActionRule "2 0 p")
    Assert.Throws<UnwrapErrorException>(fun () -> prun commandActionRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseCommandWorks() : unit =
    Assert.Equal(
        { State = { Number = 1; Symbol = '2'; }; Action = { NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }},
        prun commandRule "1 2 1 0 l")
    Assert.NotEqual(
        { State = { Number = 1; Symbol = '2'; }; Action = { NewSymbol = '0'; Direction = Left; NextStateNumber = 1 }},
        prun commandRule "1 2 1 0 r")
    Assert.Throws<UnwrapErrorException>(fun () -> prun commandRule "abra kadabra" |> ignore) |> ignore

[<Fact>]
let parseCommandsWorks() : unit =
    Assert.Equal<Command list>(
        [
            { State = { Number = 0; Symbol = 'a'; }; Action = { NewSymbol = 'z'; Direction = Right; NextStateNumber = 1 }}
            { State = { Number = 1; Symbol = 'a'; }; Action = { NewSymbol = 'y'; Direction = Right; NextStateNumber = 0 }}
        ],
        prun commandsRule "0 a 1 z r \r\n 1 a 0 y r \r\n")
    Assert.NotEqual<Command list>(
        [
            { State = { Number = 0; Symbol = 'a'; }; Action = { NewSymbol = 'z'; Direction = Pause; NextStateNumber = 1 }}
            { State = { Number = 1; Symbol = 'a'; }; Action = { NewSymbol = 'y'; Direction = Pause; NextStateNumber = 0 }}
        ],
        prun commandsRule "0 a 1 z r \r\n 1 a 0 y r \r\n")
    Assert.Throws<UnwrapErrorException>(fun () -> prun commandsRule "abra kadabra" |> ignore) |> ignore
