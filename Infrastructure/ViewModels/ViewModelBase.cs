using System.ComponentModel.DataAnnotations;

namespace Infrastructure.ViewModels;

public abstract class ViewModelBase
{
	public Guid Id { get; set; }

	[DataType(DataType.Date)] public DateTime Date { get; set; }
}