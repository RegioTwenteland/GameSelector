using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WashLine
{
    public class Message
    {
        public Message(ControllerId sender, ControllerId recipient, string command, object data = null)
        {
            CreatedAt = DateTime.Now;
            Sender = sender;
            Recipient = recipient;
            Command = command;
            Data = data;
        }

        private Message(Guid responseTo, ControllerId sender, ControllerId recipient, string command, object data = null)
            : this(sender, recipient, command, data)
        {
            InResponseTo = responseTo;
        }

        /// <summary>
        /// Id of the message.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// If this message is in response to another, contains the ID of the original message.
        /// </summary>
        public Guid? InResponseTo { get; }

        /// <summary>
        /// When was the message sent.
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Who sent the message.
        /// </summary>
        public ControllerId Sender { get; }

        /// <summary>
        /// Who must receive the message.
        /// </summary>
        public ControllerId Recipient { get; }

        /// <summary>
        /// What to do.
        /// </summary>
        public string Command { get; }

        private byte[] _data;

        /// <summary>
        /// Data associated with the command.
        /// </summary>
        public object Data
        {
            get => Deserialize(_data);
            private set => _data = Serialize(value);
        }

        private bool _consumed = false;


        public void SetConsumed()
        {
            _consumed = true;
        }

        public void AssertConsumed()
        {
           Debug.Assert(_consumed, $"Message \"{Command}\" is not handled");
        }

        private const byte separator = 0x24; // '$'

        private static byte[] Serialize(object obj)
        {
            if (obj is null) return null;

            var type = obj.GetType();

            using (var memoryStream = new MemoryStream())
            using (var reader = new StreamReader(memoryStream))
            {
                var serializer = new DataContractSerializer(type);
                serializer.WriteObject(memoryStream, obj);
                memoryStream.Position = 0;
                var typeName = Encoding.UTF8.GetBytes(type.FullName);
                var data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
                return typeName.Concat(new byte[] { separator }).Concat(data).ToArray();
            }
        }

        private static Type FindFirstType(string name)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var theType = assembly.GetType(name);
                if (theType != null)
                {
                    return theType;
                }
            }

            return null;
        }

        private static object Deserialize(byte[] blob)
        {
            if (blob is null) return null;
            if (blob.Length == 0) return null;

            var typeBlob = blob.TakeWhile(x => x != separator).ToArray();
            var type = FindFirstType(Encoding.UTF8.GetString(typeBlob));

            blob = blob.Skip(typeBlob.Length + 1).ToArray();

            using (var stream = new MemoryStream())
            {
                byte[] data = System.Text.Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(blob));
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new DataContractSerializer(type);
                return deserializer.ReadObject(stream);
            }
        }

        public void CreateResponse(string key, object value = null) => new Message(Id, Recipient, Sender, key, value);
    }
}
