﻿using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Doctrina.xAPI
{
    /// <summary>
    /// A collection of Statements can be retrieved by performing a query on the Statement Resource, see Statement Resource for details.
    /// </summary>
    public class StatementsResult : JsonModel, IStatementsResult
    {
        public StatementsResult() {}

        public StatementsResult(string jsonString) : this((JsonString)jsonString) {}
        public StatementsResult(JsonString jsonString) : this(jsonString.ToJToken()) {}
        public StatementsResult(JToken jobj) : this(jobj, ApiVersion.GetLatest()) { }
        public StatementsResult(JToken jobj, ApiVersion version)
        {
            if (!AllowObject(jobj))
            {
                return;
            }

            if (jobj["statements"] != null)
            {
                Statements = new StatementCollection(jobj.Value<JArray>("statements"), version);
            }

            if (jobj["more"] != null)
            {
                More = new Uri(jobj.Value<string>("more"));
            }
        }

        /// <summary>
        /// List of Statements. If the list returned has been limited (due to pagination), and there are more results, they will be located at the "statements" property within the container located at the IRL provided by the "more" property of this Statement result Object. Where no matching Statements are found, this property will contain an empty array.
        /// </summary>
        public StatementCollection Statements { get; set; }

        /// <summary>
        /// Relative IRL that can be used to fetch more results, including the full path and optionally a query string but excluding scheme, host, and port. Empty string if there are no more results to fetch.
        /// </summary>
        public Uri More { get; set; }

        public static async Task<StatementsResult> ReadAsMultipartAsync(string boundary, Stream stream)
        {
            var result = new StatementsResult();

            var multipartReader = new MultipartReader(boundary, stream);
            var section = await multipartReader.ReadNextSectionAsync();
            int sectionIxdex = 0;
            while (section != null)
            {
                if (sectionIxdex == 0)
                {
                    // StatementsResult
                    string jsonString = await section.ReadAsStringAsync();
                    result = new StatementsResult((JsonString)jsonString);
                }
                else
                {
                    // TODO: Read attachment
                }

                section = await multipartReader.ReadNextSectionAsync();
                sectionIxdex++;
            }

            return result;
        }

        public Attachment GetAttachmentByHash(string hash)
        {
            throw new NotImplementedException();
        }

        public Attachment GetStatementByHash(string hash)
        {
            throw new NotImplementedException();
        }

        public void SetAttachmentByHash(string hash, byte[] payload)
        {
            throw new NotImplementedException();
        }

        public override JObject ToJToken(ApiVersion version, ResultFormat format)
        {
            var obj = new JObject();
            obj["statements"] = Statements?.ToJToken(version, format);
            obj["more"] = More?.ToString();
            return obj;
        }
    }
}
