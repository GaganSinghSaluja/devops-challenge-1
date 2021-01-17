namespace ProcessFileData.Model
{
  public class ProductData
  {
    public string sku { get; set; } 
    public string description { get; set; } 
    public string category { get; set; } 
    public double price { get; set; } 
    public string location { get; set; }
    public string l3 { get; set; } 
    public int qty { get; set; }
  }

  public class TransmissionsummaryData
  {
    public string id { get; set; } 
    public int recordcount { get; set; } 
    public int qtysum { get; set; }
  }
  public class StatisticData
  {
    public string l3 { get; set; } 
    public string location { get; set; }
    public int total { get; set; }
  }
}