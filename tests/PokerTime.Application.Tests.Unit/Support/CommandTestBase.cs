// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : CommandTestBase.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Support {
    using System;
    using Persistence;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Not necessary for test base class")]
    public class CommandTestBase : IDisposable {
        protected PokerTimeDbContext Context { get; }

        public CommandTestBase() {
            this.Context = ReturnDbContextFactory.Create();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "Not necessary for test")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize", Justification = "Not necessary for test")]
        public void Dispose() => ReturnDbContextFactory.Destroy(this.Context);
    }
}
