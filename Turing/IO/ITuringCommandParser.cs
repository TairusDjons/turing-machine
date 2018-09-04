namespace Turing.IO
{
    public interface ITuringCommandParser
    {
        TuringCommand[] ParseFile(string path);
    }
}
