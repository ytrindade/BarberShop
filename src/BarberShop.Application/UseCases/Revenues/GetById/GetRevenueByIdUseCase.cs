using AutoMapper;
using BarberShop.Communication.Responses;
using BarberShop.Domain.Repositories.Revenues;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionsBase;

namespace BarberShop.Application.UseCases.Revenues.GetById;
public class GetRevenueByIdUseCase : IGetRevenueByIdUseCase
{
    private readonly IRevenuesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetRevenueByIdUseCase(IRevenuesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ResponseRevenueJson> Execute(long id)
    {
        var entity = await _repository.GetById(id);

        if(entity is null)
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);

        return _mapper.Map<ResponseRevenueJson>(entity);
    }
}
