using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Com.RePower.Ocv.Model.Extensions
{
    public class TypeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Type) == objectType;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            Type type;
            string typeStr = reader.Value?.ToString() ?? "";
            switch (typeStr)
            {
                case "Byte":
                    type = typeof(byte);
                    break;

                case "Byte[]":
                    type = typeof(byte[]);
                    break;

                case "Bool":
                    type = typeof(bool);
                    break;

                case "Int16":
                    type = typeof(short);
                    break;

                case "Int16[]":
                    type = typeof(short[]);
                    break;

                case "UInt16":
                    type = typeof(ushort);
                    break;

                case "UInt16[]":
                    type = typeof(ushort[]);
                    break;

                case "Int32":
                    type = typeof(int);
                    break;

                case "Int32[]":
                    type = typeof(int[]);
                    break;

                case "UInt32":
                    type = typeof(uint);
                    break;

                case "UInt32[]":
                    type = typeof(uint[]);
                    break;

                case "Int64":
                    type = typeof(long);
                    break;

                case "Int64[]":
                    type = typeof(long[]);
                    break;

                case "UInt64":
                    type = typeof(ulong);
                    break;

                case "UInt64[]":
                    type = typeof(ulong[]);
                    break;

                case "Single":
                    type = typeof(float);
                    break;

                case "Single[]":
                    type = typeof(float[]);
                    break;

                case "Double":
                    type = typeof(double);
                    break;

                case "Double[]":
                    type = typeof(double[]);
                    break;

                case "String":
                    type = typeof(string);
                    break;

                default:
                    type = typeof(short);
                    break;
            }
            return type;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            //new一个JObject对象,JObject可以像操作对象来操作json
            var jobj = new JObject();
            //value参数实际上是你要序列化的Model对象，所以此处直接强转
            var type = value as Type;
            writer.WriteValue(type!.Name);
        }
    }
}