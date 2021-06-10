﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dynatrace.Net.Common.Converters
{
    public abstract class JsonEnumConverter<TEnum> : JsonConverter
        where TEnum : struct, IConvertible
    {
	    protected abstract Dictionary<TEnum, string> Pairs { get; }

	    protected abstract string EntityString { get; }

	    public virtual string ConvertToString(TEnum value) => Pairs[value];

        public string ConvertToString(TEnum? value)
        {
	        return value.HasValue
		        ? ConvertToString(value.Value)
		        : null;
        }

        public virtual TEnum ConvertFromString(string s)
        {
	        var pair = Pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
	        // ReSharper disable once SuspiciousTypeConversion.Global
	        if (EqualityComparer<KeyValuePair<TEnum, string>>.Default.Equals(pair))
	        {
		        throw new ArgumentException($"Unknown {EntityString}: {s}");
	        }

	        return pair.Key;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is IEnumerable<TEnum> items)
            {
                writer.WriteStartArray();
                foreach (var item in items)
                {
	                WriteJson(writer, item, serializer);
                }
                writer.WriteEndArray();
            }
            else if (Enum.TryParse<TEnum>(value?.ToString(), out var enumerationValue))
            {
	            writer.WriteValue(ConvertToString(enumerationValue));
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                var items = new List<TEnum>();
                var array = JArray.Load(reader);
                items.AddRange(array.Select(x => ConvertFromString(x.ToString())));

                return items;
            }

            string s = (string)reader.Value;
            return ConvertFromString(s);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
