﻿namespace Shared.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() {}

    public NotFoundException(string message) : base(message) {}
}
