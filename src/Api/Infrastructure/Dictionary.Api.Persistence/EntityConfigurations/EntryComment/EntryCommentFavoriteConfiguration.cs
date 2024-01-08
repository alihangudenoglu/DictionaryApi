using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentFavoriteConfiguration : BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryCommentFavorite", DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.EntryCommentId);

        builder.HasOne(x => x.CreatedUser)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
