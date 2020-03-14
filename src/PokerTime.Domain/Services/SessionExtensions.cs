// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : RetrospectiveExtensions.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Services {
    using System;
    using Entities;

    public static class SessionExtensions {
        public static bool IsStarted(this Retrospective retrospective) {
            if (retrospective == null) throw new ArgumentNullException(nameof(retrospective));

            switch (retrospective.CurrentStage) {
                case SessionStage.NotStarted:
                case SessionStage.Finished:
                    return false;
                case SessionStage.Writing:
                case SessionStage.Discuss:
                case SessionStage.Grouping:
                case SessionStage.Voting:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException(nameof(retrospective));
            }
        }
    }
}
