using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagement.Authors
{
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string FirstName { get; set; }

        [CanBeNull]
        public virtual string? LastName { get; set; }

        [NotNull]
        public virtual string Email { get; set; }

        [CanBeNull]
        public virtual string? Bio { get; set; }

        public Author()
        {

        }

        public Author(Guid id, string firstName, string lastName, string email, string bio)
        {

            Id = id;
            Check.NotNull(firstName, nameof(firstName));
            Check.NotNull(email, nameof(email));
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Bio = bio;
        }

    }
}