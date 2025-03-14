using Azure;
using Azure.AI.DocumentIntelligence;
using System.IO;
using Azure.Core; // <- for BinaryData

public class DocumentIntelligenceService
{
    private readonly DocumentIntelligenceClient _client;

    public DocumentIntelligenceService(IConfiguration config)
    {
        var endpoint = new Uri(config["DocumentIntelligence:Endpoint"]);
        var key = new AzureKeyCredential(config["DocumentIntelligence:Key"]);
        _client = new DocumentIntelligenceClient(endpoint, key);
    }

    public async Task<Dictionary<string, string>> ExtractPassportDataAsync(Stream fileStream)
    {
        var fields = new Dictionary<string, string>();

        // ✅ Create BinaryData from the file stream
        var binaryData = BinaryData.FromStream(fileStream);

        var response = await _client.AnalyzeDocumentAsync(
            WaitUntil.Completed,
            "prebuilt-idDocument",
            binaryData
        );

        var result = response.Value;

        foreach (var document in result.Documents)
        {
            foreach (var field in document.Fields)
            {
                fields[field.Key] = field.Value?.Content ?? "";
            }
        }

        return fields;
    }
}
