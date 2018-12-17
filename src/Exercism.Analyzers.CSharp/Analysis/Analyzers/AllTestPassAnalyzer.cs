using System.Collections.Immutable;
using Exercism.Analyzers.CSharp.Analysis.Testing;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Exercism.Analyzers.CSharp.Analysis.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    internal class AllTestPassAnalyzer : DiagnosticAnalyzer
    {
        private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
            id: "EXERCISM0002",
            title: "All tests pass",
            messageFormat: "The solution does not pass all tests.",
            category: "SolutionCorrectness",
            defaultSeverity: DiagnosticSeverity.Error,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

        public override void Initialize(AnalysisContext context)
            => context.RegisterCompilationAction(AnalyzeCompilation);

        private static void AnalyzeCompilation(CompilationAnalysisContext context)
        {
            var testRunSummary = InMemoryXunitTestRunner.RunTests(context.Compilation).Result;
            
            if (testRunSummary.Skipped > 0 || testRunSummary.Failed > 0)
                context.ReportDiagnostic(Diagnostic.Create(Rule, Location.None));
        }
    }
}