using System;
using System.Threading.Tasks;
namespace minrva
{
	public interface IAuthenticate
	{
		Task<bool> Authenticate();
		Task<string> GetUserId();
		Task<bool> LogoutAsync();
	}
}
