using Microsoft.AspNetCore.Identity;

namespace ChatServer.Database.Entities.Identity;

public sealed class User : IdentityUser<Guid>
{
}
