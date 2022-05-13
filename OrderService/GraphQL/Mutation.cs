using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace OrderService.GraphQL
{
    public class Mutation
    {
        public async Task<OrderOutput> SubmitOrderAsync(
            OrderData input,
            [Service] IOptions<KafkaSettings> settings)
        {
            var dts = DateTime.Now.ToString();
            var key = "order-" + dts;
            var val = JsonConvert.SerializeObject(input);

            var result = await KafkaHelper.SendMessage(settings.Value, "orderkafka", key, val);

            OrderOutput resp = new OrderOutput
            {
                TransactionDate = dts,
                Message = "Order was submitted successfully"
            };

            if (!result)
                resp.Message = "Failed to submit data";
         
            return await Task.FromResult(resp);
        }
    }
}
