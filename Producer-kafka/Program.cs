using Confluent.Kafka;
using Newtonsoft.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:29092",
    Acks = Acks.All

};

using var producer = new ProducerBuilder<Null, string>(config).Build();
try
{
    string? state;
    while ((state = Console.ReadLine()) != null)
    {
        var response = await producer.ProduceAsync("teste-kafka",
            new Message<Null, string>
            {
                Value = JsonConvert.SerializeObject(
                new Weather(state, 70))
            });
        Console.WriteLine(response.Value);
    }
}
catch (ProduceException<Null, string> exc)
{
    Console.WriteLine(exc.Message);
}

public record Weather(string State, int Temperature);
