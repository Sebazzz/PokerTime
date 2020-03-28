// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : TestCaseBuilder.cs
//  Project         : PokerTime.Web.Tests.Integration
// ******************************************************************************

namespace PokerTime.Web.Tests.Integration.Common {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Abstractions;
    using Application.Common.Models;
    using Application.Poker.Commands;
    using Application.PredefinedParticipantColors.Queries.GetAvailablePredefinedParticipantColors;
    using Application.Sessions.Commands.JoinPokerSession;
    using Application.Sessions.Queries.GetParticipantsInfo;
    using Application.SessionWorkflows.Commands;
    using Application.Symbols.Queries;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using PokerTime.Common;

    public sealed class TestCaseBuilder {
        private readonly IServiceScope _scope;
        private readonly string _sessionId;
        private readonly Queue<Func<Task>> _actions;
        private readonly Dictionary<string, ParticipantInfo> _participators;
        private readonly FriendlyIdEntityDictionary _entityIds;

        private (Type Type, int Id) _lastAddedItem;

        public TestCaseBuilder(IServiceScope scope, string sessionId) {
            this._scope = scope;
            this._sessionId = sessionId;
            this._actions = new Queue<Func<Task>>();
            this._participators = new Dictionary<string, ParticipantInfo>(StringComparer.InvariantCultureIgnoreCase);
            this._entityIds = new FriendlyIdEntityDictionary();
        }

        public TestCaseBuilder HasExistingParticipant(string participantName) {
            this._actions.Enqueue(async () => {
                TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] attempting to record presence of existing participant [{participantName}]");

                var dbContext = this._scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();
                Participant participant = await dbContext.Participants.FirstAsync(x => x.Name == participantName && x.Session.UrlId.StringId == this._sessionId);

                this._participators.Add(participant.Name, new ParticipantInfo {
                    Id = participant.Id,
                    Name = participant.Name,
                    Color = new ColorModel(), // Doesn't matter
                    IsFacilitator = participant.IsFacilitator
                });

                this.RecordAddedId<Participant>(participant.Id);

                TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] recorded presence of existing participant [{participantName}] with ID #{participant.Id}");
            });
            return this;
        }

        public TestCaseBuilder WithParticipant(string name, bool isFacilitator, string passphrase = null) {
            string RandomByte() {
                return TestContext.CurrentContext.Random.NextByte().ToString("X2", Culture.Invariant);
            }

            AvailableParticipantColorModel availableParticipantColor = null;
            this._actions.Enqueue(async () => {
                this._scope.SetNoAuthenticationInfo();
                var yellowColor = new ColorModel {
                    R = Color.Yellow.R,
                    G = Color.Yellow.G,
                    B = Color.Yellow.B,
                };

                IList<AvailableParticipantColorModel> response = await this._scope.Send(new GetAvailablePredefinedParticipantColorsQuery(this._sessionId));
                availableParticipantColor = response.FirstOrDefault(x => !x.HasSameColors(yellowColor)); // Yellow is a bad color for testing
            });

            return this.EnqueueMediatorAction(() => new JoinPokerSessionCommand {
                Name = name,
                Color = availableParticipantColor?.HexString ?? (RandomByte() + RandomByte() + RandomByte()),
                JoiningAsFacilitator = isFacilitator,
                Passphrase = passphrase,
                SessionId = this._sessionId
            }, p => {
                if (this._participators.ContainsKey(p.Name)) {
                    Assert.Inconclusive($"Trying to register existing participant: {p.Name}");
                }

                this.RecordAddedId<ParticipantInfo>(p.Id);
                this._participators.Add(p.Name, p);
            });
        }

        public TestCaseBuilder NewRound(string title) {
            this.EnqueueMediatorAction(
                () => new InitiateDiscussionStageCommand { UserStoryTitle = title, SessionId = this._sessionId },
                _ => Task.CompletedTask);

            return this.EnqueueMediatorAction(
                () => new InitiateEstimationStageCommand { SessionId = this._sessionId },
                _ => Task.CompletedTask);
        }

        public TestCaseBuilder CloseEstimationPhase() {
            return this.EnqueueMediatorAction(
                () => new InitiateEstimationDiscussionStageCommand { SessionId = this._sessionId },
                _ => Task.CompletedTask);
        }


        public TestCaseBuilder PlayCard(string participantName, string stringValue) {
            ICollection<SymbolModel> symbols = null;
            this.EnqueueMediatorAction(participantName, () => {
                IPokerTimeDbContext dbContext =
                    this._scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();

                Session session = dbContext.Sessions.First(x => x.UrlId.StringId == this._sessionId);

                return new GetSymbolsQuery(session.SymbolSetId);
            }, r => symbols = r.Symbols);

            this.EnqueueMediatorAction(participantName,
                () => {
                    if (symbols == null) {
                        throw new InvalidOperationException("Symbols query didn't return a response");
                    }

                    SymbolModel requestedSymbol = symbols.FirstOrDefault(x => x.AsString == stringValue);
                    if (requestedSymbol == null) {
                        throw new InvalidOperationException(
                            $"Unable to find symbol '{stringValue}' in list of symbols: {String.Join("|", symbols.Select(x => x.AsString))}");
                    }

                    IPokerTimeDbContext dbContext =
                        this._scope.ServiceProvider.GetRequiredService<IPokerTimeDbContext>();
                    int userStoryId = dbContext.UserStories.
                        Where(x => x.Session.UrlId.StringId == this._sessionId).
                        OrderByDescending(x => x.Id).
                        Select(x => x.Id).
                        FirstOrDefault();

                    return new PlayCardCommand(this._sessionId, userStoryId, requestedSymbol.Id);
                },
                _ => Task.CompletedTask);

            return this;
        }

        public TestCaseBuilder OutputId(Action<int> callback) {
            this._actions.Enqueue(() => {
                if (this._lastAddedItem == default) {
                    throw new InvalidOperationException("A call to OutputId should follow a call to an entity creating action");
                }

                TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] Outputting last added item {this._lastAddedItem.Type} with ID #{this._lastAddedItem.Id} to callback ({callback.GetMethodInfo().Name})");
                callback.Invoke(this._lastAddedItem.Id);

                return Task.CompletedTask;
            });

            return this;
        }

        public TestCaseBuilder Callback(Action<TestCaseBuilder> callback) {
            this._actions.Enqueue(() => {
                callback(this);
                return Task.CompletedTask;
            });

            return this;
        }

        /// <summary>
        /// Call after entity creation actions
        /// </summary>
        /// <param name="friendlyId"></param>
        /// <returns></returns>
        public TestCaseBuilder WithId(string friendlyId) {
            this._actions.Enqueue(() => {
                if (this._lastAddedItem == default) {
                    throw new InvalidOperationException("A call to WithId should follow a call to an entity creating action");
                }

                this._entityIds.Set(friendlyId, this._lastAddedItem.Type, this._lastAddedItem.Id);

                return Task.CompletedTask;
            });

            return this;
        }
        public TestCaseBuilder WithRetrospectiveStage(SessionStage stage) => this.EnqueueRetrospectiveAction(r => r.CurrentStage = stage);

        private ParticipantInfo GetParticipatorInfo(string name) {
            if (!this._participators.TryGetValue(name, out ParticipantInfo val)) {
                Assert.Inconclusive($"Test case error: participantName {name} not found");
                return null;
            }

            return val;
        }

        public async Task Build() {
            int actionNumber = 1;
            while (this._actions.TryDequeue(out Func<Task> action)) {
                try {
                    await action();
                }
                catch (Exception ex) {
                    throw new InvalidOperationException($"Error execution action #{actionNumber}: {action}", ex);
                }

                actionNumber++;
            }
        }

        private void RecordAddedId<T>(int id) {
            TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] Recording last added item: [{typeof(T)}] with ID #{id}");
            this._lastAddedItem = (typeof(T), id);
        }

        private TestCaseBuilder EnqueueRetrospectiveAction(Action<Session> action) {
            this._actions.Enqueue(() => this._scope.SetSession(this._sessionId, action));

            return this;
        }

        private TestCaseBuilder EnqueueMediatorAction<TResponse>(Func<IRequest<TResponse>> requestFunc, Func<TResponse, Task> responseProcessor) => this.EnqueueMediatorAction<TResponse>(null, requestFunc, responseProcessor);

        private TestCaseBuilder EnqueueMediatorAction<TResponse>(string participantName, Func<IRequest<TResponse>> requestFunc, Func<TResponse, Task> responseProcessor) {
            this._actions.Enqueue(async () => {
                IRequest<TResponse> request = requestFunc();

                if (participantName == null) {
                    TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] Executing request [{request}] with no participant");

                    this._scope.SetNoAuthenticationInfo();
                }
                else {
                    TestContext.WriteLine($"[{nameof(TestCaseBuilder)}] Executing request [{request}] with participant {participantName}");

                    ParticipantInfo participantInfo = this.GetParticipatorInfo(participantName);
                    this._scope.SetAuthenticationInfo(new CurrentParticipantModel(participantInfo.Id, participantInfo.Name, participantInfo.Color.HexString, participantInfo.IsFacilitator));
                }

                try {
                    TResponse response = await this._scope.Send(request, CancellationToken.None);
                    await responseProcessor.Invoke(response);
                }
                catch (Exception ex) {
                    throw new InvalidOperationException($"Action failed [{request}] with participant {participantName}: {ex.Message}", ex);
                }
            });

            return this;
        }

        private TestCaseBuilder EnqueueMediatorAction<TResponse>(string participantName, Func<IRequest<TResponse>> requestFunc, Action<TResponse> responseProcessor) =>
            this.EnqueueMediatorAction(participantName, requestFunc, r => {
                responseProcessor.Invoke(r);
                return Task.CompletedTask;
            });

        private TestCaseBuilder EnqueueMediatorAction<TResponse>(Func<IRequest<TResponse>> requestFunc, Action<TResponse> responseProcessor) =>
            this.EnqueueMediatorAction(requestFunc, r => {
                responseProcessor.Invoke(r);
                return Task.CompletedTask;
            });

        private sealed class FriendlyIdEntityDictionary {
            private readonly Dictionary<string, (Type Type, int Id)> _dataStore;

            public FriendlyIdEntityDictionary() {
                this._dataStore = new Dictionary<string, (Type, int)>(StringComparer.Ordinal);
            }

            public int Get(string friendlyId, Type type) {
                if (!this._dataStore.TryGetValue(friendlyId, out (Type Type, int Id) item)) {
                    throw new ArgumentException($"Entity {type} with id '{friendlyId}' is not found");
                }

                return item.Id;
            }

            public void Set(string friendlyId, Type type, int itemId) {
                try {
                    this._dataStore[friendlyId] = (type, itemId);
                }
                catch (ArgumentException) {
                    throw new ArgumentException($"Entity {type} with id '{friendlyId}' is already exists");
                }
            }
        }
    }
}
