﻿// ******************************************************************************
//  © 2019 Sebastiaan Dammann | damsteen.nl
// 
//  File:           : DeleteNoteCommandHandler.cs
//  Project         : PokerTime.Application
// ******************************************************************************

namespace PokerTime.Application.Notes.Commands.DeleteNote {
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Common;
    using Common.Abstractions;
    using Common.Security;
    using Domain.Entities;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Notifications.NoteDeleted;

    public sealed class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand> {
        private readonly IReturnDbContextFactory _returnDbContextFactory;
        private readonly ISecurityValidator _securityValidator;
        private readonly IMediator _mediator;

        public DeleteNoteCommandHandler(IReturnDbContextFactory returnDbContextFactory, ISecurityValidator securityValidator, IMediator mediator) {
            this._returnDbContextFactory = returnDbContextFactory;
            this._securityValidator = securityValidator;
            this._mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken) {
            if (request == null) throw new ArgumentNullException(nameof(request));
            using IReturnDbContext dbContext = this._returnDbContextFactory.CreateForEditContext();

            // Get
            Note note = await dbContext.Notes.Include(x => x.Lane).Include(x => x.Retrospective).FirstOrDefaultAsync(x => x.Id == request.NoteId && x.Retrospective.UrlId.StringId == request.SessionId, cancellationToken);

            if (note == null) {
                throw new NotFoundException(nameof(Note), request.NoteId);
            }

            // Validate
            await this._securityValidator.EnsureDelete(note.Retrospective, note);

            // Execute
            dbContext.Notes.Remove(note);
            await dbContext.SaveChangesAsync(cancellationToken);

            // Broadcast
            await this._mediator.Publish(new NoteDeletedNotification(request.SessionId, (int)note.Lane.Id, note.Id), cancellationToken);

            return Unit.Value;
        }
    }


}