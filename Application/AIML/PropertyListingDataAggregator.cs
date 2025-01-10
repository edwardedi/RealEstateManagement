using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using Domain.Entities;

namespace Application.AIML
{
    public class PropertyListingDataAggregator
    {
        public List<PropertyListingData> PropertyListingData { get; set; }
        public PropertyListingDataAggregator()
        {
            PropertyListingData = new List<PropertyListingData>();
        }

        public List<PropertyListingData> GetPropertyListingData()
        {
            string filePath = "data.csv";
            foreach (var line in File.ReadLines(filePath))
            {
                var fields = line.Split(',');

                string Price = fields[0];
                string NumberOfBedrooms = fields[1];
                string SquareFootage = fields[2];
                if (Price == null || NumberOfBedrooms == null || SquareFootage == null)
                {
                    continue;
                }
                PropertyListingData.Add(new PropertyListingData
                {
                    Label = float.Parse(Price),
                    Features = new float[] { float.Parse(NumberOfBedrooms), float.Parse(SquareFootage) }
                });
            }
            return PropertyListingData;
        }
        public int GetPropertyListingDataCount()
        {
            return PropertyListingData.Count;
        }
    }
}
