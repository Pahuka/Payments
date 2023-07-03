using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class UserViewModel : ViewModelBase, IMapWith<User>
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Login { get; set; }
	public string Password { get; set; }
	public IEnumerable<StatisticViewModel> Statistic { get; set; }
	public bool HasHvsMeter { get; set; }
	public bool HasGvsMeter { get; set; }
	public bool HasEnergyMeter { get; set; }

	private void Mapping(Profile entity)
	{
		entity.CreateMap<User, UserViewModel>()
			.ForMember(userVm => userVm.Login,
				opt => opt.MapFrom(user => user.Login))
			.ForMember(userVm => userVm.Password,
				opt => opt.MapFrom(user => user.Password))
			.ForMember(userVm => userVm.FirstName,
				opt => opt.MapFrom(user => user.FirstName))
			.ForMember(userVm => userVm.LastName,
				opt => opt.MapFrom(user => user.LastName))
			.ForMember(userVm => userVm.HasEnergyMeter,
				opt => opt.MapFrom(user => user.HasEnergyMeter))
			.ForMember(userVm => userVm.HasGvsMeter,
				opt => opt.MapFrom(user => user.HasGvsMeter))
			.ForMember(userVm => userVm.HasHvsMeter,
				opt => opt.MapFrom(user => user.HasHvsMeter))
			.ForMember(userVm => userVm.Statistic,
				opt => opt.MapFrom(user => user.Statistic.Select(x => x.Id)));
	}
}