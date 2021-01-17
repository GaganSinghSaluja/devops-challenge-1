using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using ProcessFileData.Model;
using ProcessFileData.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace processfile.data
{
    public class ProcessData
    {
        public void InsertData(List<ProductData> products, TransmissionsummaryData transmissionsummary)
        {
            using (var db = new ProcessFileDataContext())
            {
                var strategy = db.Database.CreateExecutionStrategy();

                strategy.Execute(() =>
                {
                    using (var context = new ProcessFileDataContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            // Creates the database if not exists
                            context.Database.EnsureCreated();

                            foreach (ProductData p in products)
                            {
                                context.ProductData.Add(p);
                            }
                            context.TransmissionsummaryData.Add(transmissionsummary);

                            // Saves changes
                            context.SaveChanges();
                            transaction.Commit();
                        }
                    }
                });
            }

        }
        public bool CheckTransmissionsummaryDataExist(string Id)
        {
            // Gets and prints all books in database
            using (var context = new ProcessFileDataContext())
            {
                TransmissionsummaryData ts = context.TransmissionsummaryData.Find(Id);
                if (ts == null) return false;

            }
            return true;
        }

        public List<StatisticData> DisplayStatistics()
        {
            // Gets and prints all books in database
            using (var context = new ProcessFileDataContext())
            {

                var commandText = @"Select IF(l3 like ""%Tools%"",""Power Tools"",l3) as l3, location, Sum(qty) as total from ProductData group by location, l3;";
                var result = context.StatisticData.FromSqlRaw(commandText).ToList();
                return result;

            }

        }


    }
}