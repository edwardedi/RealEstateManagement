using Microsoft.ML;

namespace Application.AIML
{
    public class PropertyListingPricePredictionModel
    {
        private readonly MLContext mlContext;
        private ITransformer model;
        public PropertyListingPricePredictionModel() => mlContext = new MLContext();

        public void Train(List<PropertyListingData> trainingData)
        {
            var dataView = mlContext.Data.LoadFromEnumerable(trainingData);
            var pipeline = mlContext.Transforms.Conversion.ConvertType("NumberOfBedroomsFeaturized", nameof(PropertyListingData.NumberOfBedrooms))
                    .Append(mlContext.Transforms.Conversion.ConvertType("SquareFootageFeaturized", nameof(PropertyListingData.SquareFootage)))
                    .Append(mlContext.Transforms.Concatenate("Features", "NumberOfBedroomsFeaturized", "SquareFootageFeaturized"))
                    .Append(mlContext.Regression.Trainers.Sdca(labelColumnName: nameof(PropertyListingData.Price), maximumNumberOfIterations: 100));


            model = pipeline.Fit(dataView);
        }

        public float Predict(PropertyListingData propertyListingData)
        {
            var predictionEngine = mlContext.Model.CreatePredictionEngine<PropertyListingData, PropertyListingDataPrediction>(model);
            var prediction = predictionEngine.Predict(propertyListingData);
            return prediction.Price;
        }
    }
}
