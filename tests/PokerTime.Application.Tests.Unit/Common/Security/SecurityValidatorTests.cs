// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SecurityValidatorTests.cs
//  Project         : PokerTime.Application.Tests.Unit
// ******************************************************************************

namespace PokerTime.Application.Tests.Unit.Common.Security {
    using System;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Application.Common.Models;
    using Application.Common.Security;
    using Domain.Entities;
    using NUnit.Framework;
    using NUnit.Framework.Internal;
    using Support;

    [TestFixture]
    public sealed class SecurityValidatorTests {
        private readonly ISecurityValidator _securityValidator;
        private readonly MockCurrentParticipantService _currentParticipantService;

        public SecurityValidatorTests() {
            this._currentParticipantService = new MockCurrentParticipantService();
            this._securityValidator = new SecurityValidator(this._currentParticipantService, TestLogger.For<SecurityValidator>());
        }

        [SetUp]
        public void SetUp() => this._currentParticipantService.Reset();

        private static Session GetRetrospectiveInStage(SessionStage sessionStage) {
            return new Session {
                CurrentStage = sessionStage
            };
        }

        private sealed class MockCurrentParticipantService : ICurrentParticipantService {
            private CurrentParticipantModel _currentParticipant;

            public ValueTask<CurrentParticipantModel> GetParticipant() => new ValueTask<CurrentParticipantModel>(this._currentParticipant);

            public void SetParticipant(CurrentParticipantModel participant) => this._currentParticipant = participant;

            public void Reset() => this._currentParticipant = default;
        }
    }
}
