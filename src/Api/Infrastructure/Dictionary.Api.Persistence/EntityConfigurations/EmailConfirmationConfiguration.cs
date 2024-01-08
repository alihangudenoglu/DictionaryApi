using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Persistence.EntityConfigurations;

public class EmailConfirmationConfiguration : BaseEntityConfiguration<EmailConfirmation>
{
    public override void Configure(EntityTypeBuilder<EmailConfirmation> builder)
    {
        base.Configure(builder);

        builder.ToTable("EmailConfirmation",DictionaryContext.DEFAULT_SCHEMA);

    }
}
