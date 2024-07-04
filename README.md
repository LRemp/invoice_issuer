# Invoice Issuer

This task implementation focuses on invoice final price calculation based on client and service provider location and tax rules applied in their respective countries.

## Project structure

The project is divided into two main projects:

- API project
- Unit tests project

## Used techniques

The API project is structured with vertical slice architecture, due to it's easy to understand implementation principles and low complexity structure. One slice responsible to one specific feature. The implemented architecture solution provides suitable environment for further project development development.

API endpoints were implemented using Minimal APIs rather than standard controllers as a more modern approach, since it is an API service and does not contain views.

Used external API services are registered as HttpClients in IHttpClientFactory and are easily configurable. The use of IHttpClientFactory takes the responsibility of creating http clients, updating their DNS records and ensures availability of registered endpoints.

External VAT tax data API service works based on "Pagination" principle. A simillar technique was implemented in VATDataService for searching for a record through the pages.

## Used Technologies and Packages

- .NET 8.0
- xUnit
- FluentAssertions
- Moq
- Github Actions
