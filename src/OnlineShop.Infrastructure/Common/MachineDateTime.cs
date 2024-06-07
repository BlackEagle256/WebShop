﻿using OnlineShop.Application.Abstractions.Common;

namespace OnlineShop.Infrastructure.Common;
/// <summary>
/// Represents the machine date time service.
/// </summary>
internal sealed class MachineDateTime : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}