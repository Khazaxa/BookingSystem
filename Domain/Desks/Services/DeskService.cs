using Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace Domain.Desks.Services;

internal class DeskService(IAppConfiguration _configuration) : IDeskService
{
}