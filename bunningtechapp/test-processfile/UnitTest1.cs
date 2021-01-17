using System;
using Xunit;
using BunningTech.ProcessFiles;
using System.IO;
using System.Text;

namespace test_processfile
{
    public class UnitTest1
    {
        [Fact]
        public void TestProcessFile()
        {
            var output = new StringWriter();
            Console.SetOut(output);

            ProcessFile pf = new ProcessFile();            
            pf.ProcessJSONFiles(@"C:\sample-data", @"C:\Processed");   

            StringBuilder sb = new StringBuilder();          
            sb.AppendLine("Processing drop-1.json");
            sb.AppendLine("Completed drop-1.json");
            sb.AppendLine("Power Tools - Artarmon - 20");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Oakleigh - 7");
            sb.AppendLine("Processing drop-2.json");
            sb.AppendLine("Discarding drop-2.json, incorrect qtysum");
            sb.AppendLine("Power Tools - Artarmon - 20");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Oakleigh - 7");
            sb.AppendLine("Processing drop-3.json");
            sb.AppendLine("Completed drop-3.json");
            sb.AppendLine("Power Tools - Artarmon - 21");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Notting Hill - 44");
            sb.AppendLine("Power Tools - Oakleigh - 12");
            sb.AppendLine("Tiles - Oakleigh - 1");

            Assert.Equal(output.ToString(), sb.ToString());
        }
    }
}
