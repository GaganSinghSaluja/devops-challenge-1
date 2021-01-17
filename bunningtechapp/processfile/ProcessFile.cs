using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Schema;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json;
using ProcessJSONFile.Model;
using ProcessFileData.Model;
using processfile.data;
using System.Linq;


namespace BunningTech.ProcessFiles
{
    public class ProcessFile
    {
        public void ProcessJSONFiles(string sDirt, string dDirt)
        {
            try
            {
                           
                              
                
                string[] files = Directory.GetFiles(sDirt);
                ProcessData processdata = new ProcessData();

                foreach (string file in files)
                {
                    Console.WriteLine("Processing " + Path.GetFileName(file));
                    string json = File.ReadAllText(file);
                    RootModel jsondata = JsonConvert.DeserializeObject<RootModel>(json);

                    int recordcount = jsondata.products.Count;
                    int qtysum = 0;
                    bool isValidRecordCount = true;
                    bool isValidQuantityCount = true;
                    bool isFileAlreadyProcess = false;
                    List<ProductData> productdatas = new List<ProductData>();
                    TransmissionsummaryData transmissionsummaryData = new TransmissionsummaryData();

                    foreach (Product p in jsondata.products)
                    {
                        qtysum += p.qty;
                        ProductData pd = new ProductData();
                        pd.sku = p.sku;
                        pd.category = p.category;
                        pd.description = p.description;
                        pd.location = p.location;
                        pd.price = p.price;
                        pd.qty = p.qty;
                        string[] s = p.category.Split(">");
                        pd.l3 = s[2];
                        productdatas.Add(pd);
                    }
                    transmissionsummaryData.id = jsondata.transmissionsummary.id;
                    transmissionsummaryData.recordcount = jsondata.transmissionsummary.recordcount;
                    transmissionsummaryData.qtysum = jsondata.transmissionsummary.qtysum;

                    if (recordcount != jsondata.transmissionsummary.recordcount) isValidRecordCount = false;
                    if (qtysum != jsondata.transmissionsummary.qtysum) isValidQuantityCount = false;

                    //Validate recordcount and qtysum against transmissionsummary
                    if ((isValidRecordCount) && (isValidQuantityCount))
                    {
                        //Check if the file has been processed before
                        //Check if the TransmissionSummaryID Already exit in the database
                        isFileAlreadyProcess = processdata.CheckTransmissionsummaryDataExist(jsondata.transmissionsummary.id);

                        if (!isFileAlreadyProcess)
                        {
                            //Update Database with the data
                            processdata.InsertData(productdatas, transmissionsummaryData);
                            Console.WriteLine("Completed  " + Path.GetFileName(file));
                        }
                        else
                        {
                            Console.WriteLine("Skipped   " + Path.GetFileName(file));
                        }
                    }
                    else
                    {
                        if (!isValidQuantityCount) Console.WriteLine("Discarding  " + Path.GetFileName(file) + ", incorrect qtysum");
                        if (!isValidRecordCount) Console.WriteLine("Discarding  " + Path.GetFileName(file) + ", incorrect recordcount");
                    }

                    //Read & Print Product Details from the database
                    //Print out aggregate of L3 categories and total qty stock per store
                    if (!isFileAlreadyProcess)
                    {
                        List<StatisticData> statisticDatas = new List<StatisticData>();

                        statisticDatas = processdata.DisplayStatistics();


                        var result = statisticDatas.GroupBy(x => new
                        {
                            x.location,
                            x.l3
                        })
                            .Select(x => new { l3 = x.Key.l3, location = x.Key.location, total = x.Sum(y => y.total) });


                        List<StatisticData> s2 = new List<StatisticData>();
                        foreach (var r in result)
                        {
                            s2.Add(new StatisticData
                            {
                                l3 = r.l3,
                                location = r.location,
                                total = r.total
                            });
                        }
                        foreach (StatisticData sd1 in statisticDatas)
                        {

                            foreach (StatisticData r in s2)
                            {
                                if (r.location == sd1.location && r.l3 == sd1.l3) sd1.total = r.total;
                            }
                            Console.WriteLine(sd1.l3 + " - " + sd1.location + " - " + sd1.total);

                        }
                        
                    }
                    //Move the File to different folder 
                    string destFile = System.IO.Path.Combine(dDirt, Path.GetFileName(file));
                    System.IO.File.Move(file, destFile, true);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

}
