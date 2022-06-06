namespace BankingProjectWebApiApp.Response
{
    public class AccountLoginResponse
    {
        public string Name { get; set; }
        public int AccountNumber { get; set; }
        public string  Roll { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
