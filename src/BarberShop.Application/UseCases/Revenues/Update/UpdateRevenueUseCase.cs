using AutoMapper;
using BarberShop.Communication.Requests;
using BarberShop.Domain.Repositories;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionsBase;

namespace BarberShop.Application.UseCases.Revenues.Update;
public class UpdateRevenueUseCase : IUpdateRevenueUseCase
{
    private readonly IRevenuesUpdateOnly _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateRevenueUseCase(IRevenuesUpdateOnly repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(long id, RequestRevenueJson request)
    {
        Validate(request);

        var entity = await _repository.GetById(id);

        if (entity is null)
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);

        _mapper.Map(request, entity);

        _repository.Update(entity);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestRevenueJson request)
    {
        var validator = new RevenueValidator();

        var result = validator.Validate(request);

        if(!result.IsValid)
        {
            var errors = result.Errors.Select(failure => failure.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
