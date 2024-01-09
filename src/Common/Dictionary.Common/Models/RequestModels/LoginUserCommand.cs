using Dictionary.Common.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Common.Models.RequestModels;

public class LoginUserCommand:IRequest<LoginUserViewModel>
{
    public string EmailAddress { get; set; }
    public string Password { get; set; }

    public LoginUserCommand(string emailAddress, string password)
    {
        EmailAddress = emailAddress;
        Password = password;
    }

}
