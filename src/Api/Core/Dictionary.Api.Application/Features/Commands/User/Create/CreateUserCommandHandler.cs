using AutoMapper;
using Dictionary.Api.Application.Interfaces.Repositories;
using Dictionary.Common;
using Dictionary.Common.Events.User;
using Dictionary.Common.Infrastructure;
using Dictionary.Common.Infrastructure.Exceptions;
using Dictionary.Common.Models.RequestModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Api.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (existUser != null)
            throw new DatabaseValidationException("User already exists!");

        var dbUser = _mapper.Map<Dictionary.Api.Domain.Models.User>(request);

        var rows = await _userRepository.AddAsync(dbUser);

        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = request.EmailAddress
            };

            QueueFactory.SendMessageToExchange(exchangeName: DictionaryConstants.UserExchangeName,
                                               exchangeType: DictionaryConstants.DefaultExchangeType,
                                               queueName: DictionaryConstants.UserEmailChangedQueueName,
                                               obj: @event);
        }

        return dbUser.Id;

    }
}
