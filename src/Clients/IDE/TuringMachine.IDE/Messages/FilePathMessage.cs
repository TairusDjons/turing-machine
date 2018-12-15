namespace TuringMachine.IDE.Messages
{
    public class FilePathMessage
    {
        public string FilePath { get; }

        public FilePathMessage(string filePath)
        {
            FilePath = filePath;
        }
    }
}
