using Microsoft.AspNetCore.Mvc;
using Translator.Core.Interfaces;
using Translator.Data.Entities;
using Translator.Data.Repositories;

namespace Translator.Web.Controllers
{
    public class TranslationController : Controller
    {
        private readonly ITranslationRepository _repository;
        private readonly ITranslator _translator;

        public TranslationController(ITranslationRepository repository, ITranslator translator)
        {
            _repository = repository;
            _translator = translator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> TranslateToLeetSpeak(string text)
        {
            var translated = string.Empty;

            try
            {
                translated = await _translator.Translate(text);

                await _repository.AddAsync(new Translation
                {
                    InputText = text,
                    TranslatedText = translated,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = "Translation failed: " + ex.Message
                });
            }

            return Json(new
            {
                success = true,
                translatedText = translated
            });
        }
    }
}
