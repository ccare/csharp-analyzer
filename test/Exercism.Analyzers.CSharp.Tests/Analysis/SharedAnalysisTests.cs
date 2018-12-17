using System.Threading.Tasks;
using Exercism.Analyzers.CSharp.Analysis.Analyzers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Exercism.Analyzers.CSharp.Tests.Analysis
{
    public class SharedAnalysisTests : ExerciseAnalysisTests
    {
        private const string Slug = "shared";

        public SharedAnalysisTests(WebApplicationFactory<Startup> factory) : base(Slug, factory)
        {
        }
        
        [Fact]
        public Task AnalyzeSolutionWithNoDiagnosticsDoesNotReturnComments()
            => VerifyReturnsNoComments("NoDiagnostics");

        [Fact]
        public Task AnalyzeSolutionWithCompileErrorsReturnsComment()
            => VerifyReturnsComments("DoesNotCompile", "The code does not compile.");

        [Fact]
        public Task AnalyzeSolutionWithFailingTestsReturnsComment()
            => VerifyReturnsComments("FailingTests", "The solution does not pass all tests.");
    }
}