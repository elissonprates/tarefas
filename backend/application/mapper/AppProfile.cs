using AutoMapper;
using Contracts;
using Domain.Entities;

namespace Application;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<TarefaEntity, TarefaDTO>();
        CreateMap<TarefaDTO, TarefaEntity>();
    }
}
