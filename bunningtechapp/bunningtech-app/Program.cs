using System;
using System.IO;
using BunningTech.ProcessFiles;
using System.Threading.Tasks;
using System.Data;



namespace bunningtech_app
{
    class Program
    {
        static void Main(string[] args)
        {

            //Mounted Volume path on the Container
            // [Your Folder Location]:/app/data/
         

            try
            {
                string path = Directory.GetCurrentDirectory();
                string sourcePath = System.IO.Path.Combine(path, "data");
                string targetPath = System.IO.Path.Combine(path, "processed");
                Console.WriteLine("Hello from Docker Compose");

                ProcessFile pf = new ProcessFile();
               pf.ProcessJSONFiles(sourcePath, targetPath);          
                
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
