namespace application.professional;

public interface IUsecase<in I, out O>
{
  O Execute(I input);
}