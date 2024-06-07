using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;

namespace Practica4.Controllers
{

    public class SentirController : Controller
    {
        private readonly ILogger<SentirController> _logger;
        private readonly PredictionEnginePool<Sentir.ModelInput, Sentir.ModelOutput> _predictionEnginePool;

        public SentirController(ILogger<SentirController> logger, PredictionEnginePool<Sentir.ModelInput, Sentir.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        
        {
            return View("Views/Sentir/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Comentario(string comentario)
        {
            var input = new Sentir.ModelInput
            {
                Col0 = comentario
            };


           Sentir.ModelOutput prediction = _predictionEnginePool.Predict(input);


            ViewBag.Resultado = prediction.PredictedLabel;

             return View("Views/Sentir/Evaluacion.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}