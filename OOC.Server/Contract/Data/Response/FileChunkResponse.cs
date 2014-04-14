using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileChunkResponse
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public long Offset { get; set; }

        [DataMember]
        public long Length { get; set; }

        [DataMember]
        public byte[] Chunk { get; set; }

        public FileChunkResponse(string fileName, long offset, long length, byte[] chunk)
        {
            FileName = fileName;
            Offset = offset;
            Length = length;
            Chunk = chunk;
        }
    }
}