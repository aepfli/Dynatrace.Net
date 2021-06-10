﻿using System.Collections.Generic;
using Dynatrace.Net.Common.Converters;
using Newtonsoft.Json;

namespace Dynatrace.Net.Environment.SyntheticV1.Models
{
	public class ThirdPartyEventOpenNotification
	{
		public string TestId { get; set; }
		public string EventId { get; set; }
		public string Name { get; set; }
		[JsonConverter(typeof(TestEventTypesConverter))]
		public TestEventTypes? EventType { get; set; }
		public string Reason { get; set; }
		public long? StartTimestamp { get; set; }
		public IEnumerable<string> LocationIds { get; set; }
	}
}
