using MediatR;
using PersonalVault.Domain.Events;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalVault.Application.Notes.Events.Handlers
{
    public class NoteCreatedEventHandler : INotificationHandler<NoteCreatedEvent>
    {
        public Task Handle(NoteCreatedEvent notification, CancellationToken cancellationToken)
        {
            // For demonstration, write to debug output.
            Debug.WriteLine($"[DomainEvent] Note created: ID = {notification.NoteId}, Title = {notification.Title}, CreatedAt = {notification.CreatedAt}");
            // In a real application, you could trigger additional actions here (e.g., sending a notification, updating a cache, etc.)
            return Task.CompletedTask;
        }
    }
}