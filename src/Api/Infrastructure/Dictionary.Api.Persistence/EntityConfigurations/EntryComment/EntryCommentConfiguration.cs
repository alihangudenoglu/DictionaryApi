using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentConfiguration : BaseEntityConfiguration<Dictionary.Api.Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Dictionary.Api.Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryComment", DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.EntryId);

    }
}
