using Microsoft.AspNetCore.Mvc;

public abstract class BaseController : ControllerBase
{
    private readonly ILogger<BaseController> _logger;

    protected BaseController(ILogger<BaseController> logger)
    {
        _logger = logger;
    }

    protected int GetUserId()
    {
        if (User == null || !User.Claims.Any())
        {
            _logger.LogError("User claims are not available.");
            throw new InvalidOperationException("User claims are not available.");
        }

        var userIdClaim = User.Claims.FirstOrDefault(i => i.Type == "UserId");
        if (userIdClaim == null)
        {
            _logger.LogError("UserId claim is missing.");
            throw new InvalidOperationException("UserId claim is missing.");
        }

        if (!int.TryParse(userIdClaim.Value, out var userId))
        {
            _logger.LogError("UserId claim is not a valid integer.");
            throw new InvalidOperationException("UserId claim is not a valid integer.");
        }

        return userId;
    }
}