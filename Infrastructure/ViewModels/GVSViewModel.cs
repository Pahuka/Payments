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
		TotalResult = gvs.TotalResult;
		UserId = gvs.UserId;
	}

	public double CurrentValue { get; set; }
	public double TotalResult { get; set; }
	public UserViewModel? User { get; set; }
	public Guid UserId { get; set; }
}