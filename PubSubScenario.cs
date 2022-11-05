using StackExchange.Redis;
using System.Threading.Channels;

namespace RedisTestbench;


public class PubSubScenario : IScenario
{
    static readonly ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
        new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" }
        });
    
    public async Task Run()
    {
        var db = redis.GetDatabase();

        await db.StringSetAsync("foo:bar", "hello world!");

        string? s = await db.StringGetAsync("foo:bar");

        var pubsub = redis.GetSubscriber();

        await Task.WhenAll(new[]
        {
            RunPub(pubsub),
            RunSub(pubsub)
        });

        await pubsub.PublishAsync("test-channel", "This is a test message!!", CommandFlags.FireAndForget);
        Console.Write("Message Successfully sent to test-channel");

        Console.WriteLine(s);
    }

    private async Task RunPub(ISubscriber subscriber)
    {
        for (int i = 0; i < 10; i++)
        {
            await subscriber.PublishAsync("test-channel", $"message {i}");

            await Task.Delay(500);
        }
    }

    private async Task RunSub(ISubscriber subscriber)
    {
        await subscriber.SubscribeAsync("test-channel", (channel, message) => Console.WriteLine($"Received {message} from {channel}"));
    }
}