using System;

namespace Softplan.ViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Authenticated { get; set; }
    }
}