using Application.AIML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RealEstateManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyListingPricePredictionController : ControllerBase
    {
        private readonly PropertyListingPricePredictionModel propertyListingPricePredictionModel;
        public PropertyListingPricePredictionController()
        {
            propertyListingPricePredictionModel = new PropertyListingPricePredictionModel();
            var data = new PropertyListingDataAggregator();
            var sampleData = data.GetPropertyListingData();
            propertyListingPricePredictionModel.Train(sampleData);
        }

        [HttpPost("predict")]
        public ActionResult<float> PredictPrice(PropertyListingData propertyListing)
        {
            return propertyListingPricePredictionModel.Predict(propertyListing);
        }
    }
}
