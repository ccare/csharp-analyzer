using Exercism.Analyzers.CSharp.IntegrationTests.Helpers;
using Xunit;

namespace Exercism.Analyzers.CSharp.IntegrationTests
{
    public abstract class AnalyzerTests
    {
        private readonly string _slug;
        private readonly string _name;

        protected AnalyzerTests(string slug, string name) =>
            (_slug, _name) = (slug, name);
        
        protected void ShouldBeApprovedAsOptimal(string code) =>
            ShouldHaveStatusWithoutComment(code, "approve_as_optimal");

        protected void ShouldBeApprovedWithComment(string code, string comment) =>
            ShouldHaveStatusWithComment(code, "approve_with_comment", comment);

        protected void ShouldBeApprovedWithComments(string code, params string[] comments) =>
            ShouldHaveStatusWithComments(code, "approve_with_comment", comments);

        protected void ShouldBeDisapprovedWithComment(string code, string comment) =>
            ShouldHaveStatusWithComment(code, "disapprove_with_comment", comment);

        protected void ShouldBeDisapprovedWithComments(string code, params string[] comments) =>
            ShouldHaveStatusWithComments(code, "disapprove_with_comment", comments);

        protected void ShouldBeReferredToMentor(string code) =>
            ShouldHaveStatusWithoutComment(code, "refer_to_mentor");

        private void ShouldHaveStatusWithoutComment(string code, string status)
        {
            var analysisRun = Analyze(code);

            Assert.Equal(status, analysisRun.Status);
            Assert.Empty(analysisRun.Comments);
        }

        private void ShouldHaveStatusWithComment(string code, string status, string comment)
        {
            var analysisRun = Analyze(code);

            Assert.Equal(status, analysisRun.Status);
            Assert.Single(analysisRun.Comments, comment);
        }

        private void ShouldHaveStatusWithComments(string code, string status, params string[] comments)
        {
            var analysisRun = Analyze(code);

            Assert.Equal(status, analysisRun.Status);
            foreach (var comment in comments)
                Assert.Contains(comment, analysisRun.Comments);
        }

        private TestSolutionAnalysisRun Analyze(string code) => TestSolutionAnalyzer.Run(_slug, _name, code);
    }
}