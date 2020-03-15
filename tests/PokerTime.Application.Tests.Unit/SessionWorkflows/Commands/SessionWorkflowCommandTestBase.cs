// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionWorkflowCommandTestBase.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.SessionWorkflows.Commands {
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Application.SessionWorkflows.Common;
    using Domain.Entities;
    using Microsoft.EntityFrameworkCore;
    using NSubstitute;
    using NUnit.Framework;
    using PokerTime.Common;
    using Support;

    public abstract class SessionWorkflowCommandTestBase : CommandTestBase {
#nullable disable
        protected Session Session { get; private set; }
        protected string SessionId { get; private set; }
        protected ISessionStatusUpdateDispatcher SessionStatusUpdateDispatcherMock { get; set; }
        protected ISystemClock SystemClockMock { get; set; }
#nullable restore

        [OneTimeSetUp]
        public async Task OneTimeSetup() {
            var session = new Session {
                Title = "Yet another test",
                Participants =
                {
                    new Participant { Name = "John", Color = Color.BlueViolet },
                    new Participant { Name = "Jane", Color = Color.Aqua },
                },
                HashedPassphrase = "abef",
                CurrentStage = SessionStage.NotStarted
            };

            this.SessionId = session.UrlId.StringId;
            this.Session = session;
            this.ConfigureRetrospective(session);

            this.Context.Sessions.Add(session);
            await this.Context.SaveChangesAsync(CancellationToken.None);
        }

        [SetUp]
        public void SetUp() {
            this.SessionStatusUpdateDispatcherMock = Substitute.For<ISessionStatusUpdateDispatcher>();
            this.SystemClockMock = Substitute.For<ISystemClock>();
        }

        protected void RefreshObject() {
            using IPokerTimeDbContext newEditContext = this.Context.CreateForEditContext();
            this.Session = newEditContext.Sessions.FirstOrDefault(x => x.Id == this.Session.Id);
        }

        protected virtual void ConfigureRetrospective(Session session) { }
    }
}
