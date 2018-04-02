#r "Microsoft.Azure.Documents.Client"

using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;

using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

public static void Run(string input, TraceWriter log)
{
    RequestPrediction(log).Wait();
    log.Info($"C# manually triggered function called with input: {input}");
}

static async Task RequestPrediction(TraceWriter log)
{
    var fileUri = "[URL to Image File]";

    var client = new HttpClient();

    // Request headers
    client.DefaultRequestHeaders.Add("Training-key", "[Training Key]");

    var uri = "https://southcentralus.api.cognitive.microsoft.com/customvision/v1.1/Training/projects/[Project ID]/predictions/query";

    string urlStr = "{\"Url\": \"" + fileUri + "\"}";
    log.Info(urlStr);
    using (var content = new StringContent(urlStr))
    {
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        HttpResponseMessage response = await client.PostAsync(uri, content);
        string bodyStr = await response.Content.ReadAsStringAsync();
        log.Info(bodyStr);
    }
}

static async Task MakePredictionRequest(string imageFilePath, TraceWriter log)
{
    var client = new HttpClient();

    // Request headers - replace this example key with your valid subscription key.
    client.DefaultRequestHeaders.Add("Prediction-Key", "[Prediction Key]");

    // Prediction URL - replace this example URL with your valid prediction URL.
    string url = "http://southcentralus.api.cognitive.microsoft.com/customvision/v1.0/prediction/[Project ID]/image?iterationId=[Iteration ID]";

    HttpResponseMessage response;
    string bodystr;
    byte[] byteData = new byte[]{};

    using (var content = new ByteArrayContent(byteData))
    {
        content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        response = await client.PostAsync(url, content);
        bodyStr = await response.Content.ReadAsStringAsync();
        Console.WriteLine(bodyStr);
    }
    log.Info(bodyStr);
}
