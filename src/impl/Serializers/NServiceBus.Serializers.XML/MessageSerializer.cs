using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using NServiceBus.Serialization;

namespace NServiceBus.Serializers.XML
{
    public class MessageSerializer : IMessageSerializer
    {
        public void Initialize(params Type[] types)
        {
            List<Type> known = new List<Type>();
            foreach (Type t in types)
            {
                if (t.IsInterface || t.IsAbstract)
                    continue;

                if (t.IsArray)
                {
                    Type arr = t.GetElementType();
                    if (arr.IsInterface || arr.IsAbstract)
                        continue;
                }

                known.Add(t);
            }

            this.xmlSerializer = new XmlSerializer(typeof(List<object>), known.ToArray());
        }

        public void Serialize(IMessage[] messages, Stream stream)
        {
            this.xmlSerializer.Serialize(stream, new List<object>(messages));
        }

        public IMessage[] Deserialize(Stream stream)
        {
            List<object> body = this.xmlSerializer.Deserialize(stream) as List<object>;

            if (body == null)
                return null;

            IMessage[] result = new IMessage[body.Count];

            int i = 0;
            foreach (IMessage m in body)
                result[i++] = m;

            return result;
        }

        private XmlSerializer xmlSerializer = null;
    }
}
