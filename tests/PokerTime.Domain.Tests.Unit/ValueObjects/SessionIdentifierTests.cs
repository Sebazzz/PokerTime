// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionIdentifierTests.cs
//  Project         : PokerTime.Domain.Tests.Unit
// ******************************************************************************

namespace PokerTime.Domain.Tests.Unit.ValueObjects {
    using System.Collections.Generic;
    using Domain.Services;
    using Domain.ValueObjects;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SessionIdentifierTests {
        private readonly ISessionIdentifierService _sessionIdentifierService = new SessionIdentifierService();

        [Test]
        [Retry(1)]
        public void SessionIdentifier_CreateNew_ReturnsRandomId() {
            // Given
            var generatedIds = new HashSet<string>(1000);

            // When / then
            for (int count = 1000; count > 0; count--) {
                SessionIdentifier identifier = this._sessionIdentifierService.CreateNew();

                Assert.That(generatedIds.Add(identifier.StringId), Is.True, $"Non-unique identifier created: {identifier}");
            }
        }

        [Test]
        [Repeat(100)]
        public void SessionIdentifier_CreateNew_CreatesValidId() {
            // Given
            SessionIdentifier sessionIdentifier = this._sessionIdentifierService.CreateNew();

            // When
            bool isValid = this._sessionIdentifierService.IsValid(sessionIdentifier.StringId);

            // Then
            Assert.IsTrue(isValid, $"Id {sessionIdentifier} is not valid");
        }


        [Test]
        [Repeat(100)]
        public void SessionIdentifier_CreateNew_CreatesIdOfLengthLessThanOrEqualTo32() {
            // Given / when
            SessionIdentifier sessionIdentifier = this._sessionIdentifierService.CreateNew();

            // Then
            Assert.That(sessionIdentifier.StringId, Has.Length.LessThanOrEqualTo(32), $"Id {sessionIdentifier} is not valid");
        }
    }
}
