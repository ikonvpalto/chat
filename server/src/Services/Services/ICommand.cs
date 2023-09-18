namespace ChatServer.Services.Services;

public interface ICommand<in TParam>
{
    Task DoAsync(TParam param, CancellationToken cancellationToken);
}

public interface ICommand
{
    Task DoAsync(CancellationToken cancellationToken);
}
