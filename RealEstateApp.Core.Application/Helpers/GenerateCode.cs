namespace RealEstateApp.Core.Application.Helpers
{
    public static class GenerateCode
    {
        public static string GenerateAccountCode(DateTime model)
        {
            string formattedDate = model.ToString("MddHmss");

            if (int.TryParse(formattedDate, out int accountCode))
            {
                accountCode %= 1000000000;
                return accountCode.ToString();
            }
            else
            {
                throw new InvalidOperationException("No se pudo generar el código de cuenta");
            }
        }
    }
}
