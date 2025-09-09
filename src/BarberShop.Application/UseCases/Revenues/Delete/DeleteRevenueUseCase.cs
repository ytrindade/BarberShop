using BarberShop.Domain.Repositories;
using BarberShop.Domain.Repositories.Revenues;
using BarberShop.Exception;
using BarberShop.Exception.ExceptionsBase;

namespace BarberShop.Application.UseCases.Revenues.Delete;
public class DeleteRevenueUseCase : IDeleteRevenueUseCase
{
    private readonly IRevenuesWriteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRevenueUseCase(IRevenuesWriteOnlyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result is false)
            throw new NotFoundException(ResourceErrorMessages.REVENUE_NOT_FOUND);

        await _unitOfWork.Commit();
    }
}
