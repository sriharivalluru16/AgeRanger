namespace AgeRanger.Specs.Framework
{
  using System;
  using System.Net;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;
  using Newtonsoft.Json;
  using TechTalk.SpecFlow;

  public class Api
  {
    private  string apiUrl;

    public Api(string url)
    {
      this.apiUrl = url;
    }

    private HttpResponseMessage ResponseMessage
    {
      get { return ScenarioContext.Current.Get<HttpResponseMessage>("ResponseMessage"); }
      set { ScenarioContext.Current.Set(value, "ResponseMessage"); }
    }

    private string ResponseBody
    {
      get { return ScenarioContext.Current.Get<string>("ResponseBody"); }
      set { ScenarioContext.Current.Set(value, "ResponseBody"); }
    }

    public bool GetResponse( string apiUriPath = "", object data = null)
    {
      try
      {
        var request = new HttpRequestMessage
        {
          RequestUri = new Uri(this.apiUrl + apiUriPath),
          Method = data == null ? HttpMethod.Get :HttpMethod.Post
        };

        if (request.Method == HttpMethod.Post)
        {
          SendRequest(x => x.PostAsJsonAsync(this.apiUrl + apiUriPath, data))
          .Wait();
        }
        else
        {
          SendRequest(x => x.SendAsync(request))
            .Wait();
        }
      }
      catch (AggregateException exception)
      {
        return false;
      }

      return true;
    }

    private async Task SendRequest(Func<HttpClient, Task<HttpResponseMessage>> request, Action<HttpClient> setup = null)
    {
      this.ResponseMessage = null;
      this.ResponseBody = null;

      using (var client = new HttpClient())
      {
        if (setup != null)
        {
          setup(client);
        }
       
        this.ResponseMessage = await request(client);
        this.ResponseBody = await this.ResponseMessage.Content.ReadAsStringAsync();
      }
    }}
}
