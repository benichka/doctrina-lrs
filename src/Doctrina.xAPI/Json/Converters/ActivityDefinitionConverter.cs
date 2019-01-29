﻿using Doctrina.xAPI.InteractionTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Doctrina.xAPI.Json.Converters
{
    public class ActivityDefinitionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ActivityDefinition).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jobj = JObject.Load(reader); // Crashed
            var target = new ActivityDefinition();

            JToken tokenInteractionType = jobj["interactionType"];
            if (tokenInteractionType != null)
            {
                if(tokenInteractionType.Type != JTokenType.String)
                {
                    throw new JsonSerializationException($"interactionType must be a string");
                }

                string strInteractionType = tokenInteractionType.Value<string>();
                if (strInteractionType.Any(x => char.IsUpper(x)))
                {
                    throw new JsonSerializationException($"interactionType '{strInteractionType}' contains uppercase charactors, which is not allowed.");
                }

                InteractionType? enumInteractionType = null;
                var members = typeof(InteractionType).GetEnumValues();
                foreach (var enumNameValue in members)
                {
                    var memberType = enumNameValue.GetType();
                    var memberInfo = memberType.GetMember(enumNameValue.ToString());
                    if (memberInfo == null)
                        continue;

                    var attribute = (EnumMemberAttribute)memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false).FirstOrDefault();
                    if (attribute == null)
                        continue;

                    if (attribute.Value == strInteractionType)
                    {
                        // Match
                        enumInteractionType = (InteractionType)enumNameValue;
                        break;
                    }
                }

                if (!enumInteractionType.HasValue)
                {
                    throw new JsonSerializationException($"'{strInteractionType}' is not a valid interactionType. Path: '{tokenInteractionType.Path}'");
                }

                switch (enumInteractionType.Value)
                {
                    case InteractionType.TrueFalse:
                        target = new TrueFalse();
                        break;
                    case InteractionType.Choice:
                        target = new Choice();
                        break;
                    case InteractionType.FillIn:
                        target = new FillIn();
                        break;
                    case InteractionType.LongFillIn:
                        target = new LongFillIn();
                        break;
                    case InteractionType.Matching:
                        target = new Matching();
                        break;
                    case InteractionType.Performance:
                        target = new Performance();
                        break;
                    case InteractionType.Sequencing:
                        target = new Sequencing();
                        break;
                    case InteractionType.Likert:
                        target = new Likert();
                        break;
                    case InteractionType.Numeric:
                        target = new Numeric();
                        break;
                    case InteractionType.Other:
                        target = new Other();
                        break;
                }

                serializer.Populate(jobj.CreateReader(), target);
                return target;
            }

            serializer.Populate(jobj.CreateReader(), target);
            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanWrite => false;
    }
}