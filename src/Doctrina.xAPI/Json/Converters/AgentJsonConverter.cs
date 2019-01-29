﻿using Doctrina.xAPI.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Doctrina.xAPI.Json.Converters
{
    public class AgentJsonConverter : ApiJsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Agent).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            JObject jobj = JObject.Load(reader);

            // Default to agent
            ObjectType objType = ObjectType.Agent;
            var jObjectType = jobj["objectType"];
            if (jObjectType != null)
            {
                JsonValidator.IsString(jObjectType);

                string strObjectType = jObjectType.Value<string>();
                if (!Enum.TryParse(strObjectType, out objType))
                    throw new JsonSerializationException($"'{strObjectType}' is not valid. Path: '{jObjectType.Path}'");

                if(!Enum.IsDefined(typeof(ObjectType), objType))
                    throw new JsonSerializationException($"'{strObjectType}' is not valid. Path: '{jObjectType.Path}'");
            }

            if (objType == ObjectType.Agent)
            {
                var agent = new Agent();
                serializer.Populate(jobj.CreateReader(), agent);
                return agent;
            }

            if (objType == ObjectType.Group)
            {
                var group = new Group();
                serializer.Populate(jobj.CreateReader(), group);
                return group;
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            var xapiSerializer = (ApiJsonSerializer)serializer;
            var format = xapiSerializer.ResultFormat;
            writer.WriteStartObject();
            // TODO: Write Agent result based on result format.
            writer.WriteEndObject();
        }

        public override bool CanWrite => false;
    }
}