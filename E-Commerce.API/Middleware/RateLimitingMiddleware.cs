namespace E_Commerce.API.Middleware
{
    public class RateLimitingMiddleware
    {

        private readonly RequestDelegate _next;
        private static Dictionary<string, DateTime> _requests = new();

        public RateLimitingMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            var key = context.Connection.RemoteIpAddress.ToString();
            if (_requests.ContainsKey(key) && (DateTime.Now - _requests[key]).TotalSeconds < 1)
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Too many requests.");
                return;
            }

            _requests[key] = DateTime.Now;
            await _next(context);
        }
    }
}
