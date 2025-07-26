namespace bulkbuy.api.Models
{
    public class AuthResult
    {
        public AuthResult(bool success = false, string? message = null, string? token = null)
        {
            Success = success;
            Message = message ?? string.Empty;
            Token = token ?? string.Empty;
        }
        
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        
        
    } 
}

