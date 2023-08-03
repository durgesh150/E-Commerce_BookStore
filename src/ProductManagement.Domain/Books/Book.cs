using ProductManagement.Authors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ProductManagement.Books
{
    public class Book : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Title { get; set; }

        public virtual float Price { get; set; }

        [CanBeNull]
        public virtual string? Description { get; set; }
        public Guid AuthorId { get; set; }

        public Book()
        {

        }

        public Book(Guid id, Guid authorId, string title, float price, string description)
        {

            Id = id;
            Check.NotNull(title, nameof(title));
            Title = title;
            Price = price;
            Description = description;
            AuthorId = authorId;
        }

    }
}