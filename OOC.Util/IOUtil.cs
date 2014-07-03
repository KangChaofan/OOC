using System;
using System.IO;

namespace OOC.Util
{
    public class IOUtil
    {
        public static void WriteAllBytes(string path, byte[] content)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                fs.Write(content, 0, content.Length);
            }
        }

        public static void AppendAllBytes(string path, byte[] chunk)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.End);
                fs.Write(chunk, 0, chunk.Length);
            }
        }

        public static byte[] ReadAllBytes(string path)
        {
            byte[] bytes;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                int index = 0;
                long fileLength = fs.Length;
                if (fileLength > Int32.MaxValue)
                    throw new IOException("File too large");
                int count = (int)fileLength;
                bytes = new byte[count];
                while (count > 0)
                {
                    int n = fs.Read(bytes, index, count);
                    if (n == 0)
                        throw new InvalidOperationException("End of file reached before expected");
                    index += n;
                    count -= n;
                }
            }
            return bytes;
        }

        public static string ReadLines(string path, int count)
        {
            string lines = "";
            using (StreamReader sr = new StreamReader(path))
            {
                for (int i = 0; i < count; i++)
                {
                    if (sr.EndOfStream) break;
                    lines += sr.ReadLine() + "\r\n";
                }
            }
            return lines;
        }

        public static byte[] ReadChunk(string path, long offset, long len)
        {
            byte[] chunk;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                int index = 0;
                long fileLength = fs.Length;
                if (fileLength > Int32.MaxValue)
                    throw new IOException("File too large");
                if (offset >= fileLength)
                    throw new IOException("Chunk offset out of range");
                fs.Seek(offset, SeekOrigin.Begin);
                int chunkLen = (int)len;
                if (fileLength - offset < len) chunkLen = (int)(fileLength - offset);
                if (chunkLen < 0) chunkLen = 0;
                chunk = new byte[chunkLen];
                while (chunkLen > 0)
                {
                    int n = fs.Read(chunk, index, chunkLen);
                    if (n == 0)
                        throw new InvalidOperationException("End of file reached before expected");
                    index += n;
                    chunkLen -= n;
                }
            }
            return chunk;
        }
    }
}
