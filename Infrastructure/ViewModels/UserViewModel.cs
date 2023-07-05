using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace Infrastructure.ViewModels;

public class UserViewModel : ViewModelBase
{
	public UserViewModel()
	{
		EnergyStatistic = new List<EnergyViewModel>();
		GVSStatistic = new List<GVSViewModel>();
		HVSStatistic = new List<HVSViewModel>();
	}

	public UserViewModel(User user)
	{
		Id = user.Id;
		Date = user.CreatedDate;
		FirstName = user.FirstName;
		LastName = user.LastName;
		Login = user.Login;
		Password = user.Password;
		HasEnergyMeter = user.HasEnergyMeter;
		HasGvsMeter = user.HasGvsMeter;
		HasHvsMeter = user.HasHvsMeter;
		PeopleCount = user.PeopleCount;
		EnergyStatistic = new List<EnergyViewModel>();
		GVSStatistic = new List<GVSViewModel>();
		HVSStatistic = new List<HVSViewModel>();
	}

	[DisplayName("Имя")]
	[Required(ErrorMessage = "Укажите имя")]
	[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина имени должна быть в диапазоне от {2}-{1} символов")]
	public string FirstName { get; set; }

	[DisplayName("Фамилия")]
	[Required(ErrorMessage = "Укажите фамилию")]
	[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина фамилии должна быть в диапазоне от {2}-{1} символов")]
	public string LastName { get; set; }

	[DisplayName("Логин")]
	[Required(ErrorMessage = "Укажите логин")]
	[StringLength(50, MinimumLength = 3, ErrorMessage = "Длина логина должна быть в диапазоне от {2}-{1} символов")]
	public string Login { get; set; }

	[DisplayName("Пароль")]
	[Required(ErrorMessage = "Укажите пароль")]
	[StringLength(50, MinimumLength = 3, ErrorMessage = "Длинна пароля должна быть в диапазоне {1}-{2}")]
	public string Password { get; set; }

	public IList<EnergyViewModel> EnergyStatistic { get; set; }
	public IList<GVSViewModel> GVSStatistic { get; set; }
	public IList<HVSViewModel> HVSStatistic { get; set; }

	[DisplayName("Количество прописанных в помещении людей")]
	[Required(ErrorMessage = "Укажите количество прописанных людей")]
	[Range(1, 99, ErrorMessage = "Количество прописанных людей может быть в диапазоне {1}-{2}")]
	public int PeopleCount { get; set; }

	[DisplayName("Наличие прибора учета холодной воды")]
	public bool HasHvsMeter { get; set; }

	[DisplayName("Наличие прибора учета горячей воды")]
	public bool HasGvsMeter { get; set; }

	[DisplayName("Наличие прибора учета электроэнергии")]
	public bool HasEnergyMeter { get; set; }

	public double GetCurrentTotal(IEnumerable<double> statCount)
	{
		return Math.Round(statCount.Sum(), 2);
	}

	public double GetAllTotal()
	{
		return Math.Round(GetCurrentTotal(HVSStatistic.Select(x => x.TotalResult))
		                  + GetCurrentTotal(GVSStatistic.Select(x => x.TotalResultTE))
		                  + GetCurrentTotal(GVSStatistic.Select(x => x.TotalResultTN))
		                  + GetCurrentTotal(EnergyStatistic.Select(x => x.TotalResult)), 2);
	}
}