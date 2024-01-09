using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Persistence.Repositories;

public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(DictionaryContext dbContext) : base(dbContext)
    {
    }
}
