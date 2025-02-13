using System.Threading.Channels;

namespace BackgroundServiceWithChannel
{
    public class EmailQueueService
    {
        private readonly Channel<Func<Task>> _queue = Channel.CreateUnbounded<Func<Task>>();

        public void Enqueue(Func<Task> workItem) => _queue.Writer.TryWrite(workItem);

        public async Task<Func<Task>> DequeueAsync() => await _queue.Reader.ReadAsync();
    }
}
