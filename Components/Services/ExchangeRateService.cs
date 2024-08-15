using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BrazilianExchangeHelper
{
    /// <summary>
    /// Serviço para obter dados completos de taxas de câmbio oficiais do Banco Central do Brasil
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// Inicializa uma nova instância do serviço de taxas de câmbio.
        /// </summary>
        /// <param name="cache">O cache de memória a ser utilizado para armazenar as taxas de câmbio em cache.</param>
        public ExchangeRateService(IMemoryCache cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// Obtém os dados completos de taxas de câmbio para uma determinada data e moeda.
        /// </summary>
        /// <param name="date">A data para a qual as taxas de câmbio devem ser obtidas.</param>
        /// <param name="currency">A moeda para a qual as taxas de câmbio devem ser obtidas.</param>
        /// <returns>Um objeto ExchangeRateModel contendo os dados das taxas de câmbio.</returns>
        public async Task<ExchangeRateModel> GetFullExchangeData(string date, string currency)
        {
            string exchangeDate = "'" + date + "'";
            string fullUrl = $"https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/CotacaoMoedaDia(moeda=@moeda,dataCotacao=@dataCotacao)?@moeda='{currency}'&@dataCotacao={exchangeDate}&$top=100&$format=json&$select=cotacaoCompra,cotacaoVenda,dataHoraCotacao,tipoBoletim";
            var cacheKey = $"ExchangeRate_{date}_{currency}";


            if (_cache?.TryGetValue(cacheKey, out ExchangeRateModel cachedRate) ?? false)
            {
                return cachedRate;
            }

            var model = new ExchangeRateModel();

            try
            {
                using HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ExchangeRateModelRoot>(content);

                    if (!(result == null || result.Value.Count == 0))

                    {
                        var returnLast = result.Value.Last();
                        model.ExchangeDateTime = returnLast.ExchangeDateTime;
                        model.ExchangePurchase = returnLast.ExchangePurchase;
                        model.ExchangeSale = returnLast.ExchangeSale;
                        model.Bulletin = returnLast.Bulletin;
                        model.ParityPurchase = returnLast.ParityPurchase;
                        model.ParitySale = returnLast.ParitySale;

                        var cacheOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                        _cache.Set(cacheKey, model, cacheOptions);
                        return model;
                    }
                }
                else
                {
                    HandleError(model, currency);
                }
            }
            catch (HttpRequestException)
            {
                HandleError(model, currency);
            }

            return model;
        }
        private static ExchangeRateModel HandleError(ExchangeRateModel model, string currency)
        {
            if (currency == "BRL")
            {
                model.ExchangeSale = 1;
                model.ExchangePurchase = 1;
                model.ExchangeDateTime = null;
                model.Bulletin = "Não encontrado";
                return model;
            }
            model.ExchangeSale = 0;
            model.ExchangePurchase = 0;
            model.ExchangeDateTime = null;
            model.Bulletin = "Não encontrado";

            return model;
        }
    }
}
