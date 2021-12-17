using FluentValidation.Results;
using System;

namespace Financeiro.Domain.Core.Messages
{
    public abstract class CommandBase : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected CommandBase()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException();
        }
    }
}
