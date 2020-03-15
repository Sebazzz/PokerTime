// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AbstractStageCommand.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.SessionWorkflows.Commands
{
    #nullable disable

    public abstract class AbstractStageCommand
    {
        public string SessionId { get; set; }
    }
}
