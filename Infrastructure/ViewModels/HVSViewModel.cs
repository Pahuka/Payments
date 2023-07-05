using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class HVSViewModel : ViewModelBase
{
	public HVSViewModel()
	{
	}

	public HVSViewModel(HVS hvs)
	{
		Id = hvs.Id;
		Date = hvs.CreatedDate;
		CurrentValue = hvs.CurrentValue;
		TotalResult = hvs.TotalResult;
		UserId = hvs.UserId;
	}

	[DisplayName("Текущие показания")]
	[Required(ErrorMessage = "Значение не должно быть пустым")]
	public double CurrentValue { get; set; }

	public double TotalResult { get; set; }
	public UserViewModel? User { get; set; }
	public Guid UserId { get; set; }
}