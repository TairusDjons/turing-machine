module TuringMachine.Format

open FParsec

type UserState = unit
type Parser<'t> = Parser<'t, UserState>

[<Literal>]
let leftChar = 'l'
[<Literal>]
let rightChar = 'r'
[<Literal>]
let pauseChar = 'p'

let symbolRule : Parser<Symbol> = satisfy <| fun x -> x <> '\t' && x <> '\n' && x <> ' ' 
 
let stateNumberRule : Parser<StateNumber> = pint32

let directionLeftRule = charReturn leftChar Left

let directionPauseRule = charReturn pauseChar Pause

let directionRightRule = charReturn rightChar Right

let directionRule : Parser<Direction> = (directionLeftRule <|> directionPauseRule <|> directionRightRule)

let commandStateRule =
    stateNumberRule .>>. (spaces1 >>. symbolRule)
    |>> fun (number, symbol) -> {
        Number = number
        Symbol = symbol
    }

let commandActionRule =
    symbolRule .>>. (spaces1 >>. directionRule) .>>. (spaces1 >>. stateNumberRule)
    |>> fun ((symbol, direction), number) -> {
        NextStateNumber = number
        Direction = direction
        NewSymbol = symbol
    }

let commandRule = commandStateRule .>>. (spaces1 >>. commandActionRule) |>> fun (state, action) -> {
    CommandState = state
    CommandAction = action
}

let commandsRule = many (spaces >>. commandRule .>> spaces)

exception UnwrapErrorException of string

let unwrapParserResult = function
        | ParserResult.Success(result, _state, _position) -> result
        | ParserResult.Failure(str, _error, _state) -> raise <| UnwrapErrorException str

module IO =
    open System.Text

    let ParseCommands (path : string) (encoding : Encoding) =
        runParserOnFile commandsRule () path encoding |> unwrapParserResult
