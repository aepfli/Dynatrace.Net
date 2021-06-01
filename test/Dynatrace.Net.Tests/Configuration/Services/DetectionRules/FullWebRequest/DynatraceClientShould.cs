﻿using System.Linq;
using System.Threading.Tasks;
using Xunit;

// ReSharper disable once CheckNamespace
namespace Dynatrace.Net.Tests
{
	public partial class DynatraceClientShould
	{
		[Fact]
		public async Task GetAllDetectionRulesFullWebRequestAsync()
		{
			var result = await _client.GetAllDetectionRulesFullWebRequestAsync().ConfigureAwait(false);
			Assert.NotNull(result);
		}

		[Fact]
		public async Task GetDetectionRuleFullWebRequestAsync()
		{
			var results = await _client.GetAllDetectionRulesFullWebRequestAsync().ConfigureAwait(false);
			var firstResult = results.Values.FirstOrDefault();
			if (firstResult is null)
			{
				return;
			}

			var result = await _client.GetDetectionRuleFullWebRequestAsync(firstResult?.Id).ConfigureAwait(false);
			Assert.NotNull(result);
		}
	}
}
