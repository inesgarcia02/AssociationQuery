namespace WebApi.Controllers
{
    public interface IRabbitMQConsumerController
    {
        public void StartConsuming();
        void ConfigQueue(string queueName);
    }
}