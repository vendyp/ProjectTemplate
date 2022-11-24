namespace BoilerPlate.IntegrationTests.Fixtures;

[CollectionDefinition(Constant.DatabaseCollectionDefaultName)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
}