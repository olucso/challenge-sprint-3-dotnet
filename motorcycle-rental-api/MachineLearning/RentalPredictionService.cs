using Microsoft.ML;
using motorcycle_rental_api.MachineLearning.Models;

namespace motorcycle_rental_api.MachineLearning
{
    public class RentalPredictionService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public RentalPredictionService()
        {
            _mlContext = new MLContext();

            var data = new List<RentalInputModel>
            {
                new() { Days = 1, DailyValue = 100, ClientFidelity = 0, TotalValue = 100 },
                new() { Days = 3, DailyValue = 90,  ClientFidelity = 1, TotalValue = 270 },
                new() { Days = 5, DailyValue = 80,  ClientFidelity = 1, TotalValue = 400 },
                new() { Days = 2, DailyValue = 120, ClientFidelity = 0, TotalValue = 240 },
                new() { Days = 7, DailyValue = 75,  ClientFidelity = 1, TotalValue = 525 }
            };

            var trainingData = _mlContext.Data.LoadFromEnumerable(data);

            var pipeline = _mlContext.Transforms.Concatenate(
                                "Features",
                                nameof(RentalInputModel.Days),
                                nameof(RentalInputModel.DailyValue),
                                nameof(RentalInputModel.ClientFidelity))
                           .Append(_mlContext.Regression.Trainers.Sdca(
                                labelColumnName: nameof(RentalInputModel.TotalValue),
                                maximumNumberOfIterations: 100));

            _model = pipeline.Fit(trainingData);
        }

        public float Predict(RentalInputModel input)
        {
            input.TotalValue = 0;

            var predictionEngine = _mlContext.Model.CreatePredictionEngine<RentalInputModel, RentalPrediction>(_model);
            var result = predictionEngine.Predict(input);
            return result.Score;
        }
    }
}
