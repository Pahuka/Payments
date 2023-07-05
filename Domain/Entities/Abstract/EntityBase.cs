namespace Domain.Entities.Abstract;

public class EntityBase : IEntityBase
{
	public EntityBase()
	{
		Id = Guid.NewGuid();
		CreatedDate = DateTime.Now;
	}

	public Guid Id { get; set; }
	public DateTime CreatedDate { get; set; }
}