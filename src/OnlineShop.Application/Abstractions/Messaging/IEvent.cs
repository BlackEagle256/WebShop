using MediatR;

namespace OnlineShop.Application.Abstractions.Messaging;

/// <summary>
/// Represents the event interface.
/// </summary>
public interface IEvent : INotification
{
}