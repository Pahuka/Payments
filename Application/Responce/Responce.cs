namespace Application.Responce;

public class Responce<T> : IResponce<T>
{
	public T Data { get; set; }
	public string Description { get; set; }
}