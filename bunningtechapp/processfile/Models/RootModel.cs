using System.Collections.Generic;

namespace ProcessJSONFile.Model
{
    public class RootModel 
    { 
        public List<Product> products { get; set; } 
        public Transmissionsummary transmissionsummary { get; set; }       
    }
}