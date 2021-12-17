using System;

using MediatR;

namespace Financeiro.Domain.Core.Messages
{
    public abstract class Command<T> : CommandBase, IRequest<T>
    {
       
    }
}
