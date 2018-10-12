namespace Turing.Messages
{
    public class FilePathMessage
    {
        public string FilePath { get; private set; }

        public FilePathMessage(string filePath)
        {
            FilePath = filePath;
        }
    }
}
