namespace BrazilianExchangeHelper
{
    /// <summary>
    /// Interface para um serviço de taxas de câmbio.
    /// </summary>
    public interface IExchangeRateService
    {
        /// <summary>
        /// Obtém os dados completos de taxas de câmbio para uma determinada data e moeda.
        /// </summary>
        /// <param name="date">A data para a qual as taxas de câmbio devem ser obtidas.</param>
        /// <param name="currency">A moeda para a qual as taxas de câmbio devem ser obtidas.</param>
        /// <returns>Um objeto ExchangeRateModel contendo os dados das taxas de câmbio.</returns>
        Task<ExchangeRateModel> GetFullExchangeData(string date, string currency);
    }
}
