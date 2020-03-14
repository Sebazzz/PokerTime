// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SessionStage.cs
//  Project         : PokerTime.Domain
// ******************************************************************************

namespace PokerTime.Domain.Entities {
    public enum SessionStage {
        /// <summary>
        /// The retrospective is not started yet and waiting for a facilitator to appear
        /// </summary>
        NotStarted,

        /// <summary>
        /// The retrospective is underway: participants are currently writing down their findings. Notes are private to the creator.
        /// </summary>
        Discussion,

        /// <summary>
        /// The retrospective is underway: written down notes are currently being discussed. Notes cannot be modified anymore. Notes are public.
        /// </summary>
        Estimation,

        /// <summary>
        /// The retrospective is underway: notes are currently being grouped by the facilitator.
        /// </summary>
        EstimationDiscussion,

        /// <summary>
        /// The retrospective has been finished
        /// </summary>
        Finished
    }
}
