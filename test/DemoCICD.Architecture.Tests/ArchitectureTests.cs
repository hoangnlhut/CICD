using DemoCICD.Application.Abstractions.Message;
using FluentAssertions;
using NetArchTest.Rules;

namespace DemoCICD.Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "DemoCICD.Domain";
        private const string ApplicationNamespace = "DemoCICD.Application";
        private const string InfrastructureNamespace = "DemoCICD.Infrastructure";
        private const string PersistenceNamespace = "DemoCICD.Persistence";
        private const string PresentationNamespace = "DemoCICD.Presentation";
        private const string ApiNamespace = "DemoCICD.API";

        #region Test1: Verify that the "DemoCICD.Domain" project does not have any dependencies on other projects.
        [Fact]
        public void DomainProject_Should_Not_HaveDependencies_OnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PersistenceNamespace,
                PresentationNamespace,
                ApiNamespace,
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Test2: Verify that the "DemoCICD.Application" project only depends on the "DemoCICD.Domain" project.
        [Fact]
        public void ApplicationProject_Should_Only_HaveDependencies_OnDomainProject()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherShouldNotProjects = new[]
            {
                InfrastructureNamespace,
                PersistenceNamespace,
                PresentationNamespace,
                ApiNamespace,
            };

            // Act
            var testResultUnRef = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherShouldNotProjects)
                .GetResult();

            // Assert
            testResultUnRef.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Test3: Verify that the "DemoCICD.Infrastructure" project only depends on the "DemoCICD.Persistence" and "DemoCICD.Application" projects.
        [Fact]
        public void InfrastructureProject_Should_Only_HaveDependencies_OnPersistenceAndApplicationProjects()
        {
            // Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

            var otherShouldNotProjects = new[]
            {
                DomainNamespace,
                PresentationNamespace,
                ApiNamespace,
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherShouldNotProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Test4: Verify that the "DemoCICD.Persistence" project only depends on the "DemoCICD.Domain" project.
        [Fact]
        public void PersistenceProject_Should_Only_HaveDependencies_OnDomainProject()
        {
            // Arrange
            var assembly = typeof(Persistence.AssemblyReference).Assembly;

            var otherShouldNotProjects = new[]
            {
                ApplicationNamespace,
                InfrastructureNamespace,
                PresentationNamespace,
                ApiNamespace
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherShouldNotProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Test5: Verify that the "DemoCICD.Presentation" project only depends on the "DemoCICD.Infrastructure" and "DemoCICD.Application" projects.
        [Fact]
        public void PresentationProject_Should_Only_HaveDependencies_OnInfrastructureAndApplicationProjects()
        {
            // Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;

            var otherShouldNotProjects = new[]
            {
                DomainNamespace,
                PersistenceNamespace,
                ApiNamespace,
            };

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherShouldNotProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Test6: Verify that the "DemoCICD.API" project only depends on the "DemoCICD.Presentation", "DemoCICD.Infrastructure", "DemoCICD.Persistence" and "DemoCICD.Application" projects.
        [Fact]
        public void ApiProject_Should_Only_HaveDependencies_OnPresentationInfrastructurePersistenceAndApplicationProjects()
        {
            // Arrange
            var assembly = typeof(API.AssemblyReference).Assembly;

            var otherShouldNotProjects = DomainNamespace;

            // Act
            var testResult = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherShouldNotProjects)
                .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion


        #region ========================Command Tests==========================
        #region Command interface should have naming convention of ending with "Command"
        [Fact]
        public void CommandInterface_Should_Have_NamingConvention_EndingWithCommand()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Should()
                .HaveNameEndingWith("Command")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region CommandT interface should have naming convention of ending with "Command"
        [Fact]
        public void CommandTInterface_Should_Have_NamingConvention_EndingWithCommand()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommand<>))
                .Should()
                .HaveNameEndingWith("Command")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Command Handler interface should have naming convention of ending with "CommandHandler"
        [Fact]
        public void CommandHandlerInterface_Should_Have_NamingConvention_EndingWithCommandHandler()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion


        #region Command Handler interface with input and output should have naming convention of ending with "CommandHandler"
        [Fact]
        public void CommandHandlerInterface_WithInputAndOutput_Should_Have_NamingConvention_EndingWithCommandHandler()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .HaveNameEndingWith("CommandHandler")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Command should be sealed class
        [Fact]
        public void Command_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommand))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region CommandT should be sealed class
        [Fact]
        public void CommandT_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommand<>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Command Handler should be sealed class
        [Fact]
        public void CommandHandler_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Command HandlerT should be sealed class
        [Fact]
        public void CommandHandlerT_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(ICommandHandler<,>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion
        #endregion

        #region ========================Query Tests==========================
        #region Query interface should have naming convention of ending with "Query"
        [Fact]
        public void QueryInterface_Should_Have_NamingConvention_EndingWithQuery()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQuery))
                .Should()
                .HaveNameEndingWith("Query")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region QueryT interface should have naming convention of ending with "Query"
        [Fact]
        public void QueryTInterface_Should_Have_NamingConvention_EndingWithQuery()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .Should()
                .HaveNameEndingWith("Query")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Query Handler interface should have naming convention of ending with "QueryHandler"
        [Fact]
        public void QueryHandlerInterface_Should_Have_NamingConvention_EndingWithQueryHandler()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion


        #region Query Handler interface with input and output should have naming convention of ending with "QueryHandler"
        [Fact]
        public void QueryHandlerInterface_WithInputAndOutput_Should_Have_NamingConvention_EndingWithQueryHandler()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .HaveNameEndingWith("QueryHandler")
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Query should be sealed class
        [Fact]
        public void Query_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQuery))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region QueryT should be sealed class
        [Fact]
        public void QueryT_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQuery<>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Query Handler should be sealed class
        [Fact]
        public void QueryHandler_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion

        #region Query HandlerT should be sealed class
        [Fact]
        public void QueryHandlerT_Should_Be_SealedClass()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            // Act
            var testResult = Types.InAssembly(assembly)
                .That()
                .ImplementInterface(typeof(IQueryHandler<,>))
                .Should()
                .BeSealed()
                .GetResult();
            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
        #endregion
        #endregion

    }
}
