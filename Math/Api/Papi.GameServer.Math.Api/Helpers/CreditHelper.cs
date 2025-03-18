namespace Papi.GameServer.Math.Api.Helpers
{
    public static class CreditHelper
    {
        private const long INTERNAL_CREDIT_DENOMINATION = 100;

        public static decimal ConvertInternalCredit2Money(long internalCredit)
        {
            return (decimal)internalCredit / INTERNAL_CREDIT_DENOMINATION;
        }
    }
}