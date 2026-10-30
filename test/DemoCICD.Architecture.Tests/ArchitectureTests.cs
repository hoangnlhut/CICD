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


    }
}
