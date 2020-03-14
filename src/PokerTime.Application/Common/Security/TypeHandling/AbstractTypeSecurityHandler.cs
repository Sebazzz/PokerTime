// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : AbstractTypeSecurityHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Security.TypeHandling {
    using System;
    using Domain.Entities;
    using Models;

    internal abstract class AbstractTypeSecurityHandler<TEntity> : ITypeSecurityHandler where TEntity : class {
        public void HandleOperation(SecurityOperation operation, Session session, object entityObject, in CurrentParticipantModel currentParticipant) {
            var entity = entityObject as TEntity;

            if (entity == null) {
                return;
            }

            switch (operation) {
                case SecurityOperation.AddOrUpdate:
                    this.HandleAddOrUpdate(session, entity, currentParticipant);
                    break;
                case SecurityOperation.Delete:
                    this.HandleDelete(session, entity, currentParticipant);
                    break;
                default:
                    throw new NotImplementedException(operation.ToString());
            }
        }

        protected abstract void HandleAddOrUpdate(Session session, TEntity entity, in CurrentParticipantModel currentParticipant);
        protected abstract void HandleDelete(Session session, TEntity entity, in CurrentParticipantModel currentParticipant);
    }
}
