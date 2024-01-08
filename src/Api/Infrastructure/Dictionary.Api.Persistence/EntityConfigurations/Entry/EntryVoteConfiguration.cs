using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Persistence.EntityConfigurations.Entry;

public class EntryVoteConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryVote", DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryVotes)
            .HasForeignKey(x => x.EntryId);

    }
}
