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


6. Structure code for all projects: 
	see StructureCode.txt file in E:\LEARNING\SELF_TRAINING_FOLDER\.NET_Projects\A999_DotNet_Microservices\TranDongCourse\TAI_LIEU_COURSE\Senior_.net\Course 1\Documents folder
						
7. Application project:
	i. Install the following NuGet packages:
		- MediatR
		- MediatR.Extensions.Microsoft.DependencyInjection
		- FluentValidation
		- FluentValidation.DependencyInjectionExtensions
	
