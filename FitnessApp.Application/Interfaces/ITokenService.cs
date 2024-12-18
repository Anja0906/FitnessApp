
namespace FitnessApp.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, int userId);
    }
}
