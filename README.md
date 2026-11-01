🚀.NET Core Clean Architecture & CICD

🧩 Patterns & Practices Implemented
✔ CQRS (Command Query Responsibility Segregation) 
✔ Repository & Specification Pattern


🛠 Tech Stack
Backend: .NET 9, ASP.NET Core WebAPI
Database: SQL Server, PostgreSQL, Redis
Messaging: RabbitMQ
Security: Identity Microservice (JWT)
Containerization: Docker, Docker Compose
Cloud Ready: Kubernetes, Azure CI/CD, Service Mesh

📡 Code Structure 
Solution/
├── Solution items/
│   ├── .editorconfig
│   ├── Directory.Build.props
│   ├── README.md
│   └── .gitignore
├── src -->'Source code'/
│   ├── API --> 'Expose apis for application'/
│   │   ├── (ref: Application-Infrastructure--Persistence-Presentation)
│   │   └── Folders/
│   │       ├── Abstractions
│   │       └── DependencyInjection/
│   │           ├── Extensions
│   │           └── Options
│   ├── ==================================================================
│   ├── Application --> 'Handler usercase using mediatR library'/
│   │   ├── (ref: Domain) => User Repository interface at domainlayer
│   │   └── Folders/
│   │       ├── Abstractions
│   │       ├── Behaviors
│   │       ├── DependencyInjection/
│   │       │   ├── Extensions
│   │       │   └── Options
│   │       └── UserCases/
│   │           ├── V1/
│   │           │   ├── Commands/
│   │           │   │   ├── Account
│   │           │   │   ├── Identity
│   │           │   │   ├── Catalog
│   │           │   │   ├── Communication
│   │           │   │   ├── Customer
│   │           │   │   ├── Order
│   │           │   │   ├── Enventory
│   │           │   │   └── ...
│   │           │   └── Queries/
│   │           │       ├── Account
│   │           │       ├── Identity
│   │           │       ├── Catalog
│   │           │       ├── Communication
│   │           │       ├── Customer
│   │           │       ├── Order
│   │           │       ├── Enventory
│   │           │       └── ...
│   │           ├── V2
│   │           └── ...
│   ├── ==================================================================
│   ├── Domain --> 'Store entity and bussiness logic -> Core application'/
│   │   ├── (ref: None)
│   │   └── Folders/
│   │       ├── Abstractions/
│   │       │   ├── IDomainEvent.cs
│   │       │   ├── IMessage.cs
│   │       │   ├── Aggregates/
│   │       │   │   ├── IAggregateRoot : IEntity
│   │       │   │   └── abstract class AggregateRoot<TValidator> : Entity<TValidator>, IAggregateRoot
│   │       │   ├── Entities/
│   │       │   │   ├── IEntity.cs
│   │       │   │   └── abstract class Entity<TValidator> : IEntity
│   │       │   └── Repositories --> Interface
│   │       ├── Aggregates
│   │       ├── Entities/
│   │       │   ├── Profiles
│   │       │   ├── Addresses
│   │       │   ├── CatalogItems
│   │       │   ├── OrderItems
│   │       │   ├── PaymentMethods
│   │       │   ├── CartItems
│   │       │   ├── InventoryItems
│   │       │   └── ...
│   │       ├── Enumerations
│   │       └── ValueObject/
│   │           ├── Product(string Description, string Name, string Brand, string Category, string Unit, string Sku)
│   │           ├── Currency(string IsoCode, string Symbol, int DecimalPlaces, string Name, string Country, string CultureInfo)
│   │           ├── Money(decimal Amount, Currency Currency)
│   │           ├── Email(string Address, string Subject, string Body)
│   │           └── Address(string Street, string City, string State, string ZipCode, string Country, int? Number, string? Complement)
│   ├── ==================================================================
│   ├── Infrastructure --> 'Integration with external likes Job, email, provider token ...'/
│   │   ├── (ref: Application-Persistence)
│   │   └── Folders/
│   │       ├── Abstractions
│   │       ├── Authentication
│   │       ├── BackgroundJobs/
│   │       │   └── ProcessOutboxMessagesJob.cs
│   │       ├── DependencyInjection/
│   │       │   ├── Extensions
│   │       │   └── Options
│   │       └── EmailService
│   ├── ==================================================================
│   ├── Infrastructure.MessageBus --> 'Working with rabbitMQ: Consume Command and Event'/
│   │   ├── (ref: Application)
│   │   └── Folders/
│   │       ├── Abstractions
│   │       ├── Consumers/
│   │       │   ├── Commands
│   │       │   └── Events
│   │       ├── DependencyInjection/
│   │       │   ├── Extensions
│   │       │   └── Options
│   │       ├── PipeFilters
│   │       └── PipeObservers
│   ├── ==================================================================
│   ├── Persistance | Infrastructure.EF --> 'Working with database'/
│   │   ├── (ref: Domain)
│   │   └── Fodlers/
│   │       ├── Configurations
│   │       ├── Constants/
│   │       │   └── TableNames.cs
│   │       ├── Infrastructure/
│   │       │   └── PrivateResolver.cs
│   │       ├── Interceptors/
│   │       │   ├── ConvertDomainEventsToOutboxMessagesInterceptor.cs
│   │       │   └── UpdateAuditableEntitiesInterceptor.cs
│   │       ├── Migrations
│   │       ├── Outbox/
│   │       │   └── OutboxMessage.cs
│   │       ├── Repositories
│   │       ├── ApplicationDbContext.cs
│   │       └── UnitOfWork.cs
│   ├── ==================================================================
│   ├── Infrastructure.Mongo
│   ├── (ref: Domain)
│   ├── Folders/
│   │   ├── Abstractions/
│   │   │   ├── IMongoDbContext.cs
│   │   │   └── MongoDbContext.cs
│   │   ├── DependencyInjection/
│   │   │   ├── Extensions
│   │   │   └── Options
│   │   ├── Idempotence/
│   │   │   └── IdempotentDomainEventHandler.cs
│   │   ├── ProjectionDbContext.cs
│   │   └── ProjectionGateway.cs --> Repository
│   ├── ==================================================================
│   ├── Presentation  --> 'Define api using apicontroller or minimalapi(recommand but will hard maintanin)' /
│   │   ├── (ref: Application-Infrastructure)
│   │   └── Folders/
│   │       ├── Abstractions/
│   │       │   ├── ApiController.cs
│   │       │   └── ApplicationApi.cs
│   │       ├── Controllers/
│   │       │   ├── V1
│   │       │   ├── V2
│   │       │   └── ...
│   │       ├── APIs/
│   │       │   ├── Accounts => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── Catalogs => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── Communications => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── Identities => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── Orders => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── Payments => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   ├── ShoppingCarts => Asp.Versioning.Builder =>> HasApiVersion
│   │       │   └── Warehouses => Asp.Versioning.Builder =>> HasApiVersion
│   │       └── DependencyInjection/
│   │           ├── Extensions
│   │           └── Options
│   ├── ======================
│   └── Contract --> 'Defined common for project'/
│       └── Folders/
│           ├── Abstractions
│           ├── DataTransferObjects
│           ├── Enumerations
│           ├── JsonConverters
│           ├── Shared/
│           │   ├── Result.cs
│           │   └── ResultT.cs
│           └── Services/
│               ├── Account/
│               │   ├── Account.proto
│               │   ├── Command.cs
│               │   ├── DomainEvent.cs
│               │   ├── Projection.cs
│               │   └── Query.cs
│               ├── Catalog
│               ├── Communication
│               ├── Identity
│               ├── Order
│               ├── Payment
│               ├── ShoppingCart
│               └── Warehouse
├── ==================================================================
└── test --> 'Unit test' /
    ├── Architecture.Tests
    ├── API.Tests
    ├── Application.Tests
    ├── Domain.Tests
    ├── Infrastructure.Tests
    ├── Persistance.Tests
    └── Presentation.Tests



STEP BY STEP GUIDE TO CREATE A CLEAN ARCHITECTURE PROJECT:
1. Create 2 new folders named "src" and "test" in the project directory.
2. Inside the "src" folder, 
	+1. create a new web API core named "DemoCICD.API"  
	     - reference: the "DemoCICD.Presentation", "DemoCICD.Infrastructure", "DemoCICD.Persistence",  "DemoCICD.Infrastructure" 
	
	+2. create a new class library named "DemoCICD.Domain" 

	+3. create a new class library named "DemoCICD.Application" 
		- reference: the "DemoCICD.Domain"

	+4. create a new class library named "DemoCICD.Infrastructure" 
		- reference: the "DemoCICD.Persistence" and "DemoCICD.Application"

	+5. create a new class library named "DemoCICD.Persistence"
		- reference: the "DemoCICD.Domain"

	+6. create a new class library named "DemoCICD.Presentation"
		- reference: the "DemoCICD.Infrastructure" and "DemoCICD.Application"

3. Inside the "test" folder: add xunit test project named "DemoCICD.Architecture.Tests" : reference to all projects in the "src" folder and add package: FluentAssertions and NetArchTest.Rules

4. Configuration in each project/library
	a. outside for all folder:
		- Add .editorconfig file to the project root directory: for code style conventions.
		- Add text file name "Directory.Build.props" to the project root directory: for common properties that you want to apply to all projects.
			+ we add 2 package StyleCop.Analyzers and SonarAnalyzer.CSharp to the "Directory.Build.props" file for code analysis and style enforcement.

	b. for all projects/libraries:
		- Add a class name "AssemblyReference.cs"  : get all reference information of the current project


5. TDD (write test first) via ArchitectureTests.cs file in the "DemoCICD.Architecture.Tests" project: 3A (Arrange, Act, Assert) pattern
	- Test1: Verify that the "DemoCICD.Domain" project does not have any dependencies on other projects.
	- Test2: Verify that the "DemoCICD.Application" project only depends on the "DemoCICD.Domain" project.
	- Test3: Verify that the "DemoCICD.Infrastructure" project only depends on the "DemoCICD.Persistence" and "DemoCICD.Application" projects.
	- Test4: Verify that the "DemoCICD.Persistence" project only depends on the "DemoCICD.Domain" project.
	- Test5: Verify that the "DemoCICD.Presentation" project only depends on the "DemoCICD.Infrastructure" and "DemoCICD.Application" projects.
	- Test6: Verify that the "DemoCICD.API" project only depends on the "DemoCICD.Presentation", "DemoCICD.Infrastructure", and "DemoCICD.Persistence" and "DemoCICD.Application" projects.

						
6. Application project:
	i. Install the following NuGet packages:
		- MediatR
		- MediatR.Extensions.Microsoft.DependencyInjection
		- FluentValidation
		- FluentValidation.DependencyInjectionExtensions
	

