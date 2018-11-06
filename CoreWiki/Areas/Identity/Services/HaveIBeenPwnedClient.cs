using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreWiki.Areas.Identity.Services
{
	/// <summary>
	/// Client to check HaveIBeenPawned Db if password are broken
	/// https://www.troyhunt.com/ive-just-launched-pwned-passwords-version-2/
	/// https://haveibeenpwned.com/Passwords
	///
	/// Api doc: https://haveibeenpwned.com/API/v2
	/// NOTE: this api is rate limited to one request per 1500ms pr IP address.
	/// If we want to to client validation, We should do it from javascript directly to not use this one
	/// </summary>

	public class HIBPClient
	{
		private readonly HttpClient _client;
		private readonly Uri _baseUri = new Uri("https://api.pwnedpasswords.com/range/");
		private readonly ILogger<HIBPClient> _logger;

		public HIBPClient(HttpClient client, ILogger<HIBPClient> logger)
		{
			_client = client;
			_client.BaseAddress = _baseUri;
			_client.DefaultRequestHeaders.Add("api-version", "2");
			_client.DefaultRequestHeaders.Add("User-Agent", "CoreWiki");
			_logger = logger;
		}

		/// <summary>
		/// Returns number if hits password has in HIBP db
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Task<int> GetHitsPlainAsync(string password)
		{
			var hash = Sha1Hash(password);
			return await GetHitsAsync(hash);
		}

		/// <summary>
		/// Returns number if hits hash has in HIBP db
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public async Task<int> GetHitsAsync(string hashedpassword)
		{
			var res = await CallApiAsync(hashedpassword);

			// Find EndOfhash in results from HIBP
			// HIBP returns end of all hashes that matches with the start of our hash
			var regex = new Regex($"({hashedpassword.Substring(5)})[:](\\d+)");
			var matches = regex.Matches(res);
			if (matches.Count == 0)
			{
				return 0;
			}
			var t = matches[0].Groups[2].Value;
			if (int.TryParse(t, out var val))
			{
				return val;
			}

			return 0;
		}

		/// <summary>
		/// Queries HIBP with the first 5 chars of the provided sha1 string
		/// </summary>
		/// <param name="sha1string"></param>
		/// <returns></returns>
		private async Task<string> CallApiAsync(string sha1string)
		{
			var startOfHash = FirstFive(sha1string);
			try
			{
				var response = await _client.GetAsync(startOfHash);
				var results = await response.Content.ReadAsStringAsync();
				return results;
			}
			catch (Exception e )
			{
				//TODO: should we throw or is it ok to return "ok" password on error
				_logger.LogError(e, $"{nameof(CallApiAsync)} error from HIBP api");
				return "";
			}
		}

		public static string Sha1Hash(string input)
		{
			using (var sha1 = new SHA1Managed())
			{
				var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
				var sb = new StringBuilder(hash.Length * 2);

				foreach (var b in hash)
				{
					sb.Append(b.ToString("X2"));
				}

				return sb.ToString();
			}
		}

		private static string FirstFive(string input) => input.Substring(0, 5);
	}
}
