using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Persistence.EntityConfigurations.Entry;

public class EntryConfiguration : BaseEntityConfiguration<Dictionary.Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Dictionary.Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("Entry",DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.Entries)
            .HasForeignKey(x => x.CreatedById);

    }
}
