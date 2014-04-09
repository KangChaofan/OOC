using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.Serialization;

namespace OOC.Util
{
    [DataContract]
    public class PipeCommand
    {
        [DataMember]
        public string Command { get; set; }

        [DataMember]
        public Dictionary<string, string> Parameters { get; set; }

        public PipeCommand() { }

        public PipeCommand(string command) : this(command, new Dictionary<string, string>()) { }

        public PipeCommand(string command, Dictionary<string, string> parameters)
        {
            Command = command;
            Parameters = parameters;
        }
    }

    public class PipeUtil
    {
        public static PipeCommand ReadCommand(BinaryReader br)
        {
            int len = br.ReadInt32();
            string serialized = Encoding.UTF8.GetString(br.ReadBytes(len));
            return SerializationUtil.Deserialize<PipeCommand>(serialized);
        }

        public static void WriteCommand(BinaryWriter bw, PipeCommand command)
        {
            string serialized = SerializationUtil.Serialize(command);
            byte[] bytes = Encoding.UTF8.GetBytes(serialized);
            bw.Write((int)bytes.Length);
            bw.Write(Encoding.UTF8.GetBytes(serialized));
        }
    }
}