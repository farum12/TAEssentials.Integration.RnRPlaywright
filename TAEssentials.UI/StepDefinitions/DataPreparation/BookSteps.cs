using System.Net;
using Reqnroll;
using TAEssentials.UI.DataClasses;
using TAEssentials.BE.Clients;
using TAEssentials.UI.Constants;
using TAEssentials.UI.Extensions;
using TAEssentials.UI.Utils;

namespace TAEssentials.UI.StepDefinitions.DataPreparation
{
    [Binding]
    public class BookSteps
    {
        private ScenarioContext _scenarioContext;
        private readonly ProductsClient _productsClient;
        private readonly DataGenerator _dataGenerator;

        public BookSteps(ScenarioContext scenarioContext, DataGenerator dataGenerator, ProductsClient productsClient)
        {
            _scenarioContext = scenarioContext;
            _dataGenerator = dataGenerator;
            _productsClient = productsClient;
        }

        
        [Given(@"API: New Book is exists in the system")]
        public async Task GivenAPINewBookIsExistsInTheSystem()
        {
            var book = _dataGenerator.FakeBook;

            _productsClient.SetAuthToken(TokenProvider.AdminToken);
            var bookResponse = await _productsClient.CreateProductAsync(new()
            {
                Name = book.Name,
                Description = book.Description,
                Author = book.Author,
                Genre = book.Genre,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                LowStockThreshold = book.LowStockThreshold,
                Isbn = book.Isbn,
                Type = book.Type
            });

            bookResponse.Should().HaveHttpStatusCode(HttpStatusCode.Created);

            _scenarioContext.Add(ScenarioContextConstants.Book, book);
        }
    }
}