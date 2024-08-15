using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BrazilianExchangeHelper
{
    /// <summary>
    /// Modelo para representar dados de taxas de câmbio.
    /// </summary>
    public class ExchangeRateModel
    {
        /// <summary>
        /// Taxa de compra da moeda.
        /// </summary>
        [Display(Name = "Compra")]
        [JsonProperty("cotacaoCompra")]
        public decimal ExchangePurchase { get; set; }

        /// <summary>
        /// Taxa de venda da moeda.
        /// </summary>
        [Display(Name = "Venda")]
        [JsonProperty("cotacaoVenda")]
        public decimal ExchangeSale { get; set; }

        /// <summary>
        /// Paridade de compra da moeda.
        /// </summary>
        [Display(Name = "Paridade Compra")]
        [JsonProperty("paridadeCompra")]
        public decimal ParityPurchase { get; set; }

        /// <summary>
        /// Paridade de venda da moeda.
        /// </summary>
        [Display(Name = "Paridade Venda")]
        [JsonProperty("paridadeVenda")]
        public decimal ParitySale { get; set; }

        /// <summary>
        /// Data e hora da cotação das taxas de câmbio.
        /// </summary>
        [Display(Name = "Data")]
        [JsonProperty("dataHoraCotacao")]
        public string ExchangeDateTime { get; set; }

        /// <summary>
        /// Tipo de boletim das taxas de câmbio.
        /// </summary>
        [Display(Name = "Boletim")]
        [JsonProperty("tipoBoletim")]
        public string Bulletin { get; set; }
    }

    /// <summary>
    /// Classe raiz para desserialização de dados de taxas de câmbio.
    /// </summary>
    public class ExchangeRateModelRoot
    {
        /// <summary>
        /// Contexto OData dos dados.
        /// </summary>
        [JsonProperty("@odata.context")]
        public string OdataContext { get; set; }

        /// <summary>
        /// Lista de modelos de taxas de câmbio.
        /// </summary>
        [JsonProperty("value")]
        public List<ExchangeRateModel> Value { get; set; }
    }
}
