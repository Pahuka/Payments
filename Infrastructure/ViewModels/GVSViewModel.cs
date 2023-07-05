using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class GVSViewModel : ViewModelBase
{
	public GVSViewModel()
	{
	}

	public GVSViewModel(GVS gvs)
	{
		Id = gvs.Id;
		Date = gvs.CreatedDate;
		CurrentValue = gvs.CurrentValue;
		TotalResultTN = gvs.TotalResultTN;
		TotalResultTE = gvs.TotalResultTE;
		UserId = gvs.UserId;
	}

	[DisplayName("Текущие показания")]
	[Required(ErrorMessage = "Значение не должно быть пустым")]
	public double CurrentValue { get; set; }

	public double TotalResultTN { get; set; }
	public double TotalResultTE { get; set; }
	public UserViewModel? User { get; set; }
	public Guid UserId { get; set; }
}