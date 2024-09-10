using FluentAssertions;
using Moq;
using Moq.Protected;
using Translator.Core.Services.Translators;

namespace Translator.Tests.Services.Translators
{
    public class LeetSpeakTranslatorTests
    {
        [Fact]
        public async Task Translate_ShouldReturnTranslatedText()
        {
            var mockHttpClient = new Mock<HttpClient>();
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{\"contents\":{\"translated\":\"H3ll0, w0rld!\"}}")
                });

            var client = new HttpClient(handlerMock.Object);

            var translator = new LeetSpeakTranslator(client);

            var text = "Hello, World!";
            var expectedTranslation = "H3ll0, w0rld!";

            var translated = await translator.Translate(text);

            translated.Should().NotBeNullOrEmpty();
            translated.Should().Be(expectedTranslation);
        }
    }
}
