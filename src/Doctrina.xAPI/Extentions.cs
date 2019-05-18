﻿using Doctrina.xAPI.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Doctrina.xAPI
{
    [JsonConverter(typeof(ExtensionsConverter))]
    public class Extensions : JsonModel, IDictionary<Uri, JToken>
    {
        private IDictionary<Uri, JToken> _values = new Dictionary<Uri, JToken>();

        public Extensions() { }
        public Extensions(IEnumerable<KeyValuePair<Uri, JToken>> values)
        {
            foreach(var value in values)
            {
                _values.Add(value);
            }
        }

        public Extensions(JObject jobj) : this(jobj, ApiVersion.GetLatest()) { }

        public Extensions(JObject jobj, ApiVersion version)
        {
            foreach (var token in jobj)
            {
                Add(new Uri(token.Key), token.Value);
            }
        }

        public int Count => _values.Count;

        public bool IsReadOnly => _values.IsReadOnly;

        public ICollection<Uri> Keys => _values.Keys;

        public ICollection<JToken> Values => _values.Values;

        public JToken this[Uri key] { get => _values[key]; set => _values[key] = value; }

        public void Add(Uri key, JToken value)
        {
            _values.Add(new KeyValuePair<Uri, JToken>(key, value));
        }

        public void Add(KeyValuePair<Uri, JToken> item)
        {
            _values.Add(item);
        }

        public void Clear()
        {
            _values.Clear();
        }

        public bool Contains(KeyValuePair<Uri, JToken> item)
        {
            return _values.Contains(item);
        }

        public void CopyTo(KeyValuePair<Uri, JToken>[] array, int arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<Uri, JToken> item)
        {
            return _values.Remove(item);
        }

        public IEnumerator<KeyValuePair<Uri, JToken>> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        public override JObject ToJToken(ApiVersion version, ResultFormat format)
        {
            var obj = new JObject();
            foreach (var val in _values)
            {
                obj[val.Key] = val.Value;
            }
            return obj;
        }

        public bool ContainsKey(Uri key)
        {
            return _values.ContainsKey(key);
        }

        public bool Remove(Uri key)
        {
            return _values.Remove(key);
        }

        public bool TryGetValue(Uri key, out JToken value)
        {
            return _values.TryGetValue(key, out value);
        }

        public static implicit operator Extensions(JObject jobj)
        {
            return new Extensions(jobj);
        }
    }
}
