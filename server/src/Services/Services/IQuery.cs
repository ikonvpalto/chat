namespace ChatServer.Services.Services;

public interface IQuery<in TParam, TResult>
{
    Task<TResult> QueryAsync(TParam param, CancellationToken cancellationToken);
}

public interface IQuery<TResult>
{
    Task<TResult> QueryAsync(CancellationToken cancellationToken);
}
