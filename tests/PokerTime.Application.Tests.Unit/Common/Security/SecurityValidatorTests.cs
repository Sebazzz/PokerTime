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

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNote_WrongParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Writing);
            var note = new Note { ParticipantId = 1 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(2, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, note).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNote_UnauthenticatedParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Writing);
            var note = new Note { ParticipantId = 1 };
            this._currentParticipantService.Reset();
            Assume.That(this._currentParticipantService.GetParticipant().Result, Is.EqualTo(default(CurrentParticipantModel)));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, note).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_AllowsOperationOnNote_CorrectParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Writing);
            var note = new Note { ParticipantId = 212 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(212, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, note).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.Nothing);
        }

        [Test]
        [TestCase(SessionStage.NotStarted)]
        [TestCase(SessionStage.Discuss)]
        [TestCase(SessionStage.Grouping)]
        [TestCase(SessionStage.Voting)]
        [TestCase(SessionStage.Finished)]
        public void SecurityValidator_DisallowsOperationsOnNote_InStages(SessionStage stage) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(stage);
            var note = new Note { ParticipantId = 252 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(252, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, SecurityOperation.AddOrUpdate, note).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNoteGroup_NotFacilitator(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Grouping);
            var noteGroup = new NoteGroup { Title = "G" };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(2, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteGroup).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNoteGroup_UnauthenticatedParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Grouping);
            var noteGroup = new NoteGroup { Title = "G" };
            this._currentParticipantService.Reset();
            Assume.That(this._currentParticipantService.GetParticipant().Result, Is.EqualTo(default(CurrentParticipantModel)));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteGroup).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_AllowsOperationOnNote_IsFacilitator(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Grouping);
            var noteGroup = new NoteGroup { Title = "G" };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(212, String.Empty, true));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteGroup).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.Nothing);
        }

        [Test]
        [TestCase(SessionStage.NotStarted)]
        [TestCase(SessionStage.Discuss)]
        [TestCase(SessionStage.Writing)]
        [TestCase(SessionStage.Voting)]
        [TestCase(SessionStage.Finished)]
        public void SecurityValidator_DisallowsOperationsOnNoteGroup_InStages(SessionStage stage) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(stage);
            var noteGroup = new NoteGroup { Title = "G" };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(252, String.Empty, true));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, SecurityOperation.AddOrUpdate, noteGroup).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }



        // ----

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNoteVote_WrongParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Voting);
            var noteVote = new NoteVote { ParticipantId = 1 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(2, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteVote).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_DisallowsOperationOnNoteVote_UnauthenticatedParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Voting);
            var noteVote = new NoteVote { ParticipantId = 1 };
            this._currentParticipantService.Reset();
            Assume.That(this._currentParticipantService.GetParticipant().Result, Is.EqualTo(default(CurrentParticipantModel)));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteVote).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        [Test]
        [TestCase(SecurityOperation.AddOrUpdate)]
        [TestCase(SecurityOperation.Delete)]
        public void SecurityValidator_AllowsOperationOnNoteVote_CorrectParticipant(SecurityOperation securityOperation) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(SessionStage.Voting);
            var noteVote = new NoteVote { ParticipantId = 212 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(212, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, securityOperation, noteVote).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.Nothing);
        }

        [Test]
        [TestCase(SessionStage.NotStarted)]
        [TestCase(SessionStage.Writing)]
        [TestCase(SessionStage.Discuss)]
        [TestCase(SessionStage.Grouping)]
        [TestCase(SessionStage.Finished)]
        public void SecurityValidator_DisallowsOperationsOnNoteVote_InStages(SessionStage stage) {
            // Given
            Retrospective retro = GetRetrospectiveInStage(stage);
            var noteVote = new NoteVote { ParticipantId = 252 };
            this._currentParticipantService.SetParticipant(new CurrentParticipantModel(252, String.Empty, false));

            // When
            TestDelegate action = () => this._securityValidator.EnsureOperation(retro, SecurityOperation.AddOrUpdate, noteVote).GetAwaiter().GetResult();

            // Then
            Assert.That(action, Throws.InstanceOf<OperationSecurityException>());
        }

        private static Retrospective GetRetrospectiveInStage(SessionStage sessionStage) {
            return new Retrospective {
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
