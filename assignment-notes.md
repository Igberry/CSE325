# Assignment Notes

## 1. Web API CRUD evidence

The ContosoPizza API includes an additional seeded pizza record in the in-memory list:

- Id: 3
- Name: Margherita
- IsGlutenFree: true

Verified API behavior with live requests:

- GET /pizza/1 -> HTTP 200
- POST /pizza -> HTTP 201
- PUT /pizza/4 -> HTTP 204
- DELETE /pizza/4 -> HTTP 204

## 2. Sales summary function

```csharp
void GenerateSalesSummaryReport(IEnumerable<string> salesFiles, double salesTotal, string outputPath)
{
    var report = new StringBuilder();
    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($" Total Sales: {salesTotal:C}");
    report.AppendLine();
    report.AppendLine(" Details:");

    foreach (var file in salesFiles)
    {
        string salesJson = File.ReadAllText(file);
        SalesData? data = JsonConvert.DeserializeObject<SalesData?>(salesJson);
        var fileTotal = data?.Total ?? 0;
        report.AppendLine($"  {Path.GetFileName(file)}: {fileTotal:C}");
    }

    File.WriteAllText(outputPath, report.ToString());
}
```
