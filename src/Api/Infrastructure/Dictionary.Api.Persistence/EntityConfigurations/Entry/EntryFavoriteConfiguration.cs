using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Persistence.EntityConfigurations.Entry;

public class EntryFavoriteConfiguration : BaseEntityConfiguration<EntryFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("EntryFavorite", DictionaryContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryFavorites)
            .HasForeignKey(x => x.EntryId);

        builder.HasOne(x => x.CreatedUser)
            .WithMany(x => x.EntryFavorites)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
