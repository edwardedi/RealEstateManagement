namespace Application.AIML
{
    public class PropertyLisingDataGenerator
    {
        public static List<PropertyListingData> GeneratePropertyListingData()
        {
            var propertyListingData = new List<PropertyListingData>
            {
                new PropertyListingData
                {
                    Price = 100000,
                    SquareFootage = 1000,
                    NumberOfBedrooms = 2
                },
                new PropertyListingData
                {
                    Price = 200000,
                    SquareFootage = 2000,
                    NumberOfBedrooms = 3
                },
                new PropertyListingData
                {
                    Price = 300000,
                    SquareFootage = 3000,
                    NumberOfBedrooms = 4
                },
                new PropertyListingData
                {
                    Price = 400000,
                    SquareFootage = 4000,
                    NumberOfBedrooms = 5
                },
                new PropertyListingData
                {
                    Price = 500000,
                    SquareFootage = 5000,
                    NumberOfBedrooms = 6
                }
            };
            return propertyListingData;
        }
    }
}
