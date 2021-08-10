using Mediinfo.DTO.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediinfo.Infrastructure.JCJG.Controller
{
    public class DTOJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(DTOBase).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None)
            {
                return null;
            }
            if (reader.TokenType == JsonToken.StartObject)
            {
                DTOBase dto = existingValue as DTOBase;
                if (dto == null)
                {
                    dto = ((DTOBase)Activator.CreateInstance(objectType));
                }

                dto.SetTraceChange(true);
                serializer.Populate(reader, dto);
                return dto;
            }
            else
            {
                throw new NotSupportedException("不支持的类型");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException("");
        }
    }
}
