namespace Domain.Entities.Abstract;

public interface IEntityBase
{
	public Guid Id { get; set; }
	public DateTime CreatedDate { get; set; }
}