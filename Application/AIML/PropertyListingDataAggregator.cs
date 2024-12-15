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
                PropertyListingData.Add(new PropertyListingData
                {
                    Price = float.Parse(Price),
                    NumberOfBedrooms = float.Parse(NumberOfBedrooms),
                    SquareFootage = float.Parse(SquareFootage)
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
