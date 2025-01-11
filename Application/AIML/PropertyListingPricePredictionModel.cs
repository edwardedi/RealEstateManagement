using Microsoft.ML;
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

            // Define the data preparation pipeline
            var dataPrepPipeline = mlContext.Transforms.NormalizeMinMax(nameof(PropertyListingData.Features));


            var options = new SdcaRegressionTrainer.Options
            {
                LabelColumnName = nameof(PropertyListingData.Label),
                FeatureColumnName = nameof(PropertyListingData.Features),
                ConvergenceTolerance = 0.02f,
                MaximumNumberOfIterations = 100,
                BiasLearningRate = 0.1f
            };

            var pipeline = dataPrepPipeline.Append(mlContext.Regression.Trainers.Sdca(options));
            model = pipeline.Fit(trainingData);
        }

        public float Predict(PropertyListingData propertyListingData)
        {
            var singleTestData = mlContext.Data.LoadFromEnumerable(new[] { propertyListingData });
            var transformedTestData = model.Transform(singleTestData);
            var prediction = mlContext.Data.CreateEnumerable<Prediction>(transformedTestData, reuseRowObject: false).First();
            return prediction.Score;
        }

        private sealed class Prediction
        {
            public float Score { get; set; }
        }

        public float Evaluate(List<PropertyListingData> dataPoints)
        {
            var testData = mlContext.Data.LoadFromEnumerable(dataPoints);
            var predictions = model.Transform(testData);
            var metrics = mlContext.Regression.Evaluate(predictions, labelColumnName: nameof(PropertyListingData.Label));
            return (float)metrics.RSquared;
        }
    }
}
