[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module TuringMachine.Internal

    type NoWhiteSpaceString = | NoWhiteSpaceString of string

    let NoWhiteSpaceString (value: string) : NoWhiteSpaceString option =
        if value.Contains " "
            then None
            else NoWhiteSpaceString(value) |> Some
