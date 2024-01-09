using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Api.Domain.Models;
using Dictionary.Api.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DictionaryContext dbContext) : base(dbContext)
    {
    }
}
