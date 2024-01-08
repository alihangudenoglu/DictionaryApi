using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentVoteConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentVote", DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentVotes)
            .HasForeignKey(x => x.EntryCommentId);

    }
}
