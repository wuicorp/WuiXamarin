using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WuiAlertsApp.Models;
using System.Linq;

namespace WuiAlertsApp.Repository
{
	public class WuiRepository:IWuiRepository
	{
		public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
		{
			try
			{
				var client = new HttpClient();

				var result = await client.GetStringAsync(WuiApiUrls.Vehicles);
				var deserializedResult = JsonConvert.DeserializeObject<IEnumerable<Vehicle>>(result);

				return deserializedResult;
			}
			catch(Exception ex) {
				var message = ex.Message;
			}

			return null;
		}

		public async Task<User> GetMyDetailsAsync()
		{
			try
			{
				var client = new HttpClient();
				var result = await client.GetStringAsync(WuiApiUrls.Users);

				var deserializedResult = JsonConvert.DeserializeObject<User>(result);

				return deserializedResult;

			}
			catch (Exception ex)
			{
				var message = ex.Message;
			}

			return null;
		}

		public async Task<IEnumerable<WuiAlert>> GetWuisAsync()
		{
			try
			{
				var client = new HttpClient();

				var tt = "[{ \"id\": \"1\", \"type\": \"crash\", \"action\": \"sent\", \"status\": \"pending\", \"vehicle\": { \"id\": \"1\", \"identifier\": \"6699CMZ\" }}, { \"id\": \"1\", \"type\": \"park\", \"action\": \"received\", \"status\": \"confirmed\", \"vehicle\": { \"id\": \"3\", \"identifier\": \"7777JPQ\" }} ]";

				var result = await client.GetStringAsync(WuiApiUrls.Wuis);
				var deserializedResult = JsonConvert.DeserializeObject<IEnumerable<WuiAlert>>(result);

//				if (deserializedResult!=null && deserializedResult.Any())
//				{
//					App.Database.ClearWuis();
//
//					foreach(var wui in deserializedResult)
//					{
//						App.Database.SaveWui(wui);
//					}
//				}

				return deserializedResult;
			}
			catch (Exception ex)
			{
				var message = ex.Message;
			}

			return null;
		}


		public async Task<WuiAlert> UpdateWuiAsync(WuiAlert wui)
		{
			try
			{
				var client = new HttpClient();
				client.DefaultRequestHeaders.TryAddWithoutValidation("authorization", "Bearer <password-access-token>");

//				using (var response = await httpClient.PutAsync("wuis/" + wui.Id, wui))
//				{
//					string responseData = await response.Content.ReadAsStringAsync();
//				}
//				var tt = "[{ \"id\": \"1\", \"type\": \"crash\", \"action\": \"sent\", \"status\": \"pending\", \"vehicle\": { \"id\": \"1\", \"identifier\": \"6699CMZ\" }}, { \"id\": \"1\", \"type\": \"park\", \"action\": \"received\", \"status\": \"confirmed\", \"vehicle\": { \"id\": \"3\", \"identifier\": \"7777JPQ\" }} ]";
//
//				var result = await client.PutAsync(WuiApiUrls.Wuis);
//				var deserializedResult = JsonConvert.DeserializeObject<IEnumerable<WuiAlert>>(result);
//
//				return deserializedResult;

				return null;
			}
			catch (Exception ex)
			{
				var message = ex.Message;
			}

			return null;
		}
	}

	public interface IWuiRepository
	{
		Task<IEnumerable<Vehicle>> GetVehiclesAsync();

		Task<User> GetMyDetailsAsync();

		Task<IEnumerable<WuiAlert>> GetWuisAsync();
		Task<WuiAlert> UpdateWuiAsync(WuiAlert wui);
	}
}

