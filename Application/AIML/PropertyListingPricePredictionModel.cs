using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace Application.AIML
{
    public class PropertyListingPricePredictionModel
    {
        private readonly MLContext mlContext;
        private ITransformer model;
        public PropertyListingPricePredictionModel() => mlContext = new MLContext();

        public void Train(List<PropertyListingData> dataPoints)
        {
            var trainingData = mlContext.Data.LoadFromEnumerable(dataPoints);
            var options = new SdcaRegressionTrainer.Options
            {
                LabelColumnName = nameof(PropertyListingData.Label),
                FeatureColumnName = nameof(PropertyListingData.Features),
                ConvergenceTolerance = 0.02f,
                MaximumNumberOfIterations = 30,
                BiasLearningRate = 0.1f
            };
            var pipeline = mlContext.Regression.Trainers.Sdca(options);
            model = pipeline.Fit(trainingData);
        }

        public float Predict(PropertyListingData propertyListingData)
        {
            var singleTestData = mlContext.Data.LoadFromEnumerable(new[] { propertyListingData });
            var transformedTestData = model.Transform(singleTestData);
            var prediction = mlContext.Data.CreateEnumerable<Prediction>(transformedTestData, reuseRowObject: false).First();
            return prediction.Score;
        }

        private class Prediction
        {
            public float Label { get; set; }
            public float Score { get; set; }
        }
    }
}
