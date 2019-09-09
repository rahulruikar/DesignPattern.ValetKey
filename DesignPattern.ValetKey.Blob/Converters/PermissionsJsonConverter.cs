using DesignPattern.ValetKey.Blob.Models;
using System;
using Newtonsoft.Json;

namespace DesignPattern.ValetKey.Blob.Converters
{
    internal sealed class PermissionsJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var permission = (Permissions) value;

            switch (permission)
            {
                case Permissions.Create:
                    writer.WriteValue("Create");
                    break;
                case Permissions.Read:
                    writer.WriteValue("Read");
                    break;
                case Permissions.Write:
                    writer.WriteValue("Write");
                    break;
                case Permissions.Delete:
                    writer.WriteValue("Delete");
                    break;
                default:
                    return;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = (string) reader.Value;

            switch (value)
            {
                case "Create":
                    return Permissions.Create;
                case "Read":
                    return Permissions.Read;
                case "Write":
                    return Permissions.Write;
                case "Delete":
                    return Permissions.Delete;
                default:
                    return Permissions.Unknown;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
