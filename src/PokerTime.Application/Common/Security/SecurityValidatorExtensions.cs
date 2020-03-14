// ******************************************************************************
//  ©  Sebastiaan Dammann | damsteen.nl
// 
//  File:           : SecurityValidatorExtensions.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Common.Security {
    using System;
    using System.Threading.Tasks;
    using Domain.Entities;

    public static class SecurityValidatorExtensions {
        public static ValueTask EnsureAddOrUpdate(this ISecurityValidator securityValidator, Session session, object entity) {
            if (securityValidator == null) throw new ArgumentNullException(nameof(securityValidator));
            return securityValidator.EnsureOperation(session, SecurityOperation.AddOrUpdate, entity);
        }
        public static ValueTask EnsureDelete(this ISecurityValidator securityValidator, Session session, object entity) {
            if (securityValidator == null) throw new ArgumentNullException(nameof(securityValidator));
            return securityValidator.EnsureOperation(session, SecurityOperation.Delete, entity);
        }

    }
}
