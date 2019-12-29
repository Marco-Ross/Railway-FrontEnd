using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Railway_FrontEnd.domain.timings;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net;

namespace Railway_FrontEnd
{
    class RestClient
    {
		public async Task<List<object>> MakeRequest(string httpMethod, string railwayEndPoint, object contentObject = null)
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.Credentials = new NetworkCredential("Kaylin", "pass02");

			HttpClient httpClient = new HttpClient(handler);

			//httpClient.DefaultRequestHeaders.Accept.Clear();
			//httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			// Call asynchronous network methods in a try/catch block to handle exceptions.
			try
			{
				string baseURI = "http://localhost:8080/railway/";

				//All customers that get on the train should be removed once they reach their destination
				switch (httpMethod)
				{
					case "GETALL":
						
						// Get body from request
						var responseBodyAll = await httpClient.GetStringAsync(baseURI + railwayEndPoint);

						var deserializedProductAll = JsonConvert.DeserializeObject<List<object>>(responseBodyAll);
						return deserializedProductAll;

					case "GET":
						
						// Get body from request
						var responseBodyGet = await httpClient.GetStringAsync(baseURI + railwayEndPoint);

						List<object> singleObject = new List<object>();
						var deserializedProductGet = JsonConvert.DeserializeObject<object>(responseBodyGet);
						singleObject.Add(deserializedProductGet);
						return singleObject;

					case "POST":

						var postContent = JsonConvert.SerializeObject(contentObject);
						var postBuffer = System.Text.Encoding.UTF8.GetBytes(postContent);
						var postByteContent = new ByteArrayContent(postBuffer);
						postByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

						var result = await httpClient.PostAsync(baseURI + railwayEndPoint, postByteContent);
						List<object> postist = new List<object>();
						int length = (int)result.Content.Headers.ContentLength.Value;
						if(length == 0)
						{
							postist.Add(null);
						}else
						{
							postist.Add(length);
						}
						
						return postist;

					case "UPDATE":

						var putContent = JsonConvert.SerializeObject(contentObject);
						var putBuffer = System.Text.Encoding.UTF8.GetBytes(putContent);
						var putByteContent = new ByteArrayContent(putBuffer);
						putByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

						await httpClient.PutAsync(baseURI + railwayEndPoint, putByteContent);

						break;
					case "DELETE":

						await httpClient.DeleteAsync(baseURI + railwayEndPoint);

						break;
					default:
						return null;
				}
				return null;
				
			}
			catch (HttpRequestException e)
			{
				return null;
			}	
		}


	}

	
}
