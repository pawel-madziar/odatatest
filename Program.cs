// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

string[] configKeys = ["RPConfigConnection", "RPJwtSignaturePublicKey"];

HttpMessageHandler handler = new HttpClientHandler()
{
    UseDefaultCredentials = true,
};

HttpClient client = new HttpClient(handler);


client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

foreach (string configKey in configKeys)
{
    var resp = await client.GetAsync($"https://uat-client.domesticandgeneral.com/_api/web/lists/GetByTitle('RP Configuration List')/items?$filter=RP_ConfigKey eq '{configKey}'&$select=RP_ConfigKey,RP_ConfigValue");

    resp.EnsureSuccessStatusCode();

    var ttt = await resp.Content.ReadAsStringAsync();

    var jn = JsonObject.Parse(ttt);
    var configValue = jn["value"]![0]!["RP_ConfigValue"]!.ToString();


    Console.WriteLine(configKey);
    Console.WriteLine(configValue);
    Console.WriteLine("==================================");
    Console.WriteLine();

}
