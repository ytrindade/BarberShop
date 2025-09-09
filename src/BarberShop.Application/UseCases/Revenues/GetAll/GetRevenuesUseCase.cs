using AutoMapper;
using BarberShop.Communication.Responses;
using BarberShop.Domain.Repositories.Revenues;

namespace BarberShop.Application.UseCases.Revenues.GetAll;
public class GetRevenuesUseCase : IGetRevenuesUseCase
{
    private readonly IRevenuesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetRevenuesUseCase(IRevenuesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResponseRevenuesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseRevenuesJson
        {
            Revenues = _mapper.Map < List<ResponseShortRevenueJson>>(result)
        };
    }
}
