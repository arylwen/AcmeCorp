# AcmeCorp
.net core 8 web api sample application.

## Run:
```
git clone https://github.com/arylwen/AcmeCorp.git
cd AcmeCorp
docker compose build
docker compose up
```
## Try:

Navigate to: http://localhost:3500/index.html

AcmeCorp API presents the Swagger UI. Use the default value of the token presented in the UI.

*Do not use in production.*

## Cleanup:
```
docker compose down
```
## Features:
* SOLID and DDD principles
* Authentication/Authorization with API Key
* sqlite based
* Unit tests
* Integration tests
* docker integration
    * ‘docker-compose up’ starts the API
    *  performs DB migrations 

## Possible next steps
* sql server based
* add the repository pattern
* add more unit/integration tests
* add line items
* customize/brand the swagger UI
* improve documentation
* add IaC for different cloud platforms

## SOLID principles
Here's an overview of how the code implements SOLID principles:

1. **Single Responsibility Principle (SRP):** The classes and methods are divided into different responsibilities which helps to maintain readability, scalability, and test-ability. For example, `Customer` class has only one responsibility that is customer management. Similarly, `Order` class also has only one responsibility of order management.

2. **Liskov Substitution Principle (LSP):** The `Customer` and `Order` classes are designed in such a way that their subclasses can replace instances of superclass without causing any issues. 

3. **Separation of Concerns (SOC):** Each concern is separated into its own module, which makes it easier to manage and understand the codebase. For instance, customer management related functions are grouped in `CustomerController` while order management related functions are grouped in `OrderController`.

## DDD principles
The code implements principles of Domain-Driven Design:

1. **Strong Boundaries:** The codebase is separated into different layers based on their responsibilities. This separation helps in managing complexity by keeping each layer focused on its own area. For example, `AcmeCorpAPIContext` acts as an interface to the underlying database and doesn't directly handle any business logic.

2. **Ubiquitous Language:** The code uses domain-specific language (like `Customer`, `Order`) instead of generic terms or technical jargon. This helps in keeping the code understandable by both developers and domain experts as it reduces ambiguity.

3. **Domain Entities:** The entities are defined in the domain layer with their own responsibilities. For example, the `Customer` class has properties like `Name` and `ContactInfo` while managing its orders (which are instances of `Order`). This encapsulation helps to keep the business logic related to a specific entity within that entity's class itself.

4. **Aggregate root:** The CustomerInfo is represented as an "owned" entity of Customer. Customer equates with an aggrgate root and the CustomerInfo is the aggregate.