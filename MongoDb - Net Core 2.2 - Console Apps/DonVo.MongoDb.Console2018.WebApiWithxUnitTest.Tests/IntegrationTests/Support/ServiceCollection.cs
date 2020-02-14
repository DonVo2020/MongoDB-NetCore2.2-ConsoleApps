using Xunit;

namespace Bingo.Specification.IntegrationTests.Support
{
    [CollectionDefinition("HttpTest")]
    public class ServiceCollection : ICollectionFixture<ServiceFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces for Bingo.
    }
}
