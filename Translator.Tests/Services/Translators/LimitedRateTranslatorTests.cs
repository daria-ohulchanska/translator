using Moq;
using Translator.Core.Interfaces;
using Translator.Core.Services.Translators;

namespace Translator.Tests.Services.Translators
{
    public class LimitedRateTranslatorTests
    {
        [Fact]
        public async Task Translate_WhenRateLimitReached_ShouldThrowException()
        {
            // Arrange
            var mockTranslator = new Mock<ITranslator>();
            mockTranslator.Setup(t => t.Translate(It.IsAny<string>()))
                .ReturnsAsync("translated text");

            var limitedRateTranslator = new LimitedRateTranslator(mockTranslator.Object, 1, 1);

            // Act
            await limitedRateTranslator.Translate("text");

            // Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => limitedRateTranslator.Translate("text"));
        }
    }
}
