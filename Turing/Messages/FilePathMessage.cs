using System;
using System.Collections.Generic;
using System.Text;

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
