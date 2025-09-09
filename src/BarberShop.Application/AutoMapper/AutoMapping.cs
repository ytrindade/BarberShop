using AutoMapper;
using BarberShop.Communication.Requests;
using BarberShop.Communication.Responses;
using BarberShop.Domain.Entities;
using BarberShop.Domain.Repositories;
using BarberShop.Domain.Repositories.Revenues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Application.AutoMapper;
public class AutoMapping : Profile
{   
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRevenueJson, Revenue>();
    }

    private void EntityToResponse()
    {
        CreateMap<Revenue, ResponseRegisteredRevenueJson>();
        CreateMap<Revenue, ResponseShortRevenueJson>();
        CreateMap<Revenue, ResponseRevenueJson>();
    }
}
