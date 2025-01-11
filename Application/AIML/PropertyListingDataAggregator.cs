namespace Application.AIML
{
    public class PropertyListingDataAggregator
    {
        public List<PropertyListingData> PropertyListingData { get; set; }
        public PropertyListingDataAggregator()
        {
            PropertyListingData = new List<PropertyListingData>();
        }

        public List<PropertyListingData> GetPropertyListingData(bool trainingData = true)
        {
            string filePath = "data.csv";
            int lineCount = File.ReadLines(filePath).Count();
            int currentLine = 0;
            foreach (var line in File.ReadLines(filePath))
            {
                currentLine++;
                if (trainingData == true && currentLine > lineCount * 0.75)
                    break;
                if (trainingData == false && currentLine <= lineCount * 0.75)
                    continue;
                var fields = line.Split(',');

                string Price = fields[0];
                string NumberOfBedrooms = fields[1];
                string SquareFootage = fields[2];
                if (Price == null || NumberOfBedrooms == null || SquareFootage == null || 
                    float.Parse(Price) == 0 || float.Parse(NumberOfBedrooms) == 0 || float.Parse(SquareFootage) == 0)
                {
                    continue;
                }
                PropertyListingData.Add(new PropertyListingData
                {
                    Label = float.Parse(Price),
                    Features = new float[] { float.Parse(NumberOfBedrooms), (float)(float.Parse(SquareFootage) * 0.092903) }
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
