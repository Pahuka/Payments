namespace Application.Responce;

public interface IResponce<T>
{
	T Data { get; set; }
	public string Description { get; set; }
}