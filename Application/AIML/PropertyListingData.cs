using Microsoft.ML.Data;

namespace Application.AIML
{
    public class PropertyListingData
    {
        public float Label { get; set; }
        [VectorType(2)]
        public float[] Features { get; set; }
    }
}
