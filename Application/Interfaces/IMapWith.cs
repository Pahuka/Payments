using AutoMapper;

namespace Application.Interfaces;

public interface IMapWith<T>
{
	void Mapping(Profile entity)
	{
		entity.CreateMap(typeof(T), GetType());
	}
}