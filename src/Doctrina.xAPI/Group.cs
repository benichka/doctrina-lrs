﻿using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System.Collections.Generic;
using System.Linq;

namespace Doctrina.xAPI
{
    //[JSchemaGenerationProvider(typeof(GroupSchema))]
    public class Group : Agent
    {
        public Group()
            : base()
        {
        }


        protected override ObjectType OBJECT_TYPE => ObjectType.Group;

        /// <summary>
        /// The members of this Group. This is an unordered list.
        /// </summary>
        [JsonProperty("member",
            NullValueHandling = NullValueHandling.Ignore,
            Required = Required.DisallowNull)]
        public Agent[] Member { get; set; }

        public bool HasMember()
        {
            return Member != null && Member.Count() > 0;
        }

        public override bool Equals(object obj)
        {
            var group = obj as Group;
            return group != null &&
                   base.Equals(obj) &&
                   EqualityComparer<Agent[]>.Default.Equals(Member, group.Member);
        }

        public override int GetHashCode()
        {
            var hashCode = 606588793;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Agent[]>.Default.GetHashCode(Member);
            return hashCode;
        }

        public static bool operator ==(Group group1, Group group2)
        {
            return EqualityComparer<Group>.Default.Equals(group1, group2);
        }

        public static bool operator !=(Group group1, Group group2)
        {
            return !(group1 == group2);
        }
    }

    //public class GroupSchema : JSchemaGenerationProvider
    //{
    //    public override JSchema GetSchema(JSchemaTypeGenerationContext context)
    //    {
    //        JSchema groupBaseSchemaJson = JSchema.Parse(@"{
    //            'id': '#group_base',
    //            'type': 'object',
    //            'required': ['objectType'],
    //            'properties': {
    //                'name': {'type': 'string'},
    //                'objectType': {'enum': ['Group']
    //            },
    //                'member': {
    //                    'type': 'array',
    //                    'items': {'$ref': '#agent'}
    //                }
    //            }
    //        }");

    //        string groupSchemaJson = @"{
    //            '$schema': 'http://json-schema.org/draft-04/schema#',
    //            'id': '#group',
    //            'oneOf': [
    //                {'$ref': '#anonymousgroup'},
    //                {'$ref': '#identifiedgroup'}
    //            ]
    //        }";

    //        string identifiedGroupSchemaJson = @"{
    //            '$schema': 'http://json-schema.org/draft-04/schema#',
    //                    'id': '#identifiedgroup',
    //            'allOf': [
    //                {'$ref': '#inversefunctional'},
    //                {'$ref': '#group_base'}
    //            ],
    //            'properties': {
    //                'name': {},
    //                'objectType': {},
    //                'member': {},
    //                'mbox': {},
    //                'mbox_sha1sum': {},
    //                'account': {},
    //                'openid': {}
    //            },
    //            'additionalProperties': false
    //        }";

    //        string anonGroupSchemaJson = @"{
    //            '$schema': 'http://json-schema.org/draft-04/schema#',
    //                    'id': '#anonymousgroup',
    //            'allOf': [{'$ref': '#group_base'}],
    //            'required': ['member'],
    //            'properties': {
    //                'member': {},
    //                'name': {},
    //                'objectType': {}
    //            },
    //            'additionalProperties': false
    //        }";

    //        return new JSchema();
    //    }
    //}
}