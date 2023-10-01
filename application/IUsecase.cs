namespace application.professional;

public interface IUsecase<I, O>
{
  O Execute(I input);
}