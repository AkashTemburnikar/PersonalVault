using MediatR;
using System;

namespace PersonalVault.Domain.Events
{
    /// <summary>
    /// A domain event that is raised when a Note is created.
    /// </summary>
    public class NoteCreatedEvent : INotification
    {
        public Guid NoteId { get; }
        public string Title { get; }
        public DateTime CreatedAt { get; }

        public NoteCreatedEvent(Guid noteId, string title, DateTime createdAt)
        {
            NoteId = noteId;
            Title = title;
            CreatedAt = createdAt;
        }
    }
}