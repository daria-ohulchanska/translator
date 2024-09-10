using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Translator.Core.Interfaces;
using Translator.Core.Services.Translators;

namespace Translator.Tests.Services.Translators
{
    public class CachedTranslatorTests
    {
        [Fact]
        public async Task Translate_WithCachedText_ReturnsCachedText()
        {
            // Arrange
            var text = "Hello, World!";
            var cachedText = "Bonjour, le monde!";

            var mockTranslator = new Mock<ITranslator>();
            mockTranslator.Setup(t => t.Translate(text)).ReturnsAsync(cachedText);

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            memoryCache.Set(text, cachedText);

            var translator = new CachedTranslator(mockTranslator.Object, memoryCache);

            // Act
            var result = await translator.Translate(text);

            // Assert
            cachedText.Should().NotBeNullOrEmpty();
            cachedText.Should().BeEquivalentTo(result);

            mockTranslator.Verify(t => t.Translate(It.IsAny<string>()), Times.Never());
        }

        [Fact]
        public async Task Translate_WithNewText_ReturnsTranslatedText()
        {
            // Arrange
            var text = "Hello, World!";
            var cachedText = "Bonjour, le monde!";

            var mockTranslator = new Mock<ITranslator>();
            mockTranslator.Setup(t => t.Translate(text)).ReturnsAsync(cachedText);

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var translator = new CachedTranslator(mockTranslator.Object, memoryCache);

            // Act
            var result = await translator.Translate(text);

            // Assert
            cachedText.Should().NotBeNullOrEmpty();
            cachedText.Should().BeEquivalentTo(result);

            mockTranslator.Verify(t => t.Translate(text), Times.Once());
        }
    }
}
