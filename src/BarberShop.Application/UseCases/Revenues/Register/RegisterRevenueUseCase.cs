using AutoMapper;
using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;
using BarberShop.Domain.Repositories.Revenues;
using BarberShop.Domain.Repositories;
using BarberShop.Exception.ExceptionsBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarberShop.Domain.Entities;

namespace BarberShop.Application.UseCases.Revenues.Register;
public class RegisterRevenueUseCase : IRegisterRevenueUseCase
{
    private readonly IRevenuesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterRevenueUseCase(IRevenuesWriteOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ResponseRegisteredRevenueJson> Execute(RequestRevenueJson request)
    {
        Validator(request);

        var entity = _mapper.Map<Revenue>(request);

        await _repository.Add(entity);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredRevenueJson>(entity);
    }

    private void Validator(RequestRevenueJson request)
    {
        var validator = new RevenueValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
