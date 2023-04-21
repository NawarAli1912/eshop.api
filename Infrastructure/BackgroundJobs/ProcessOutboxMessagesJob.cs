using Domain.SharedKernel.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Persistence;
using Persistence.Outbox;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private const int MaxRetries = 5;

    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;

    public ProcessOutboxMessagesJob(
        ApplicationDbContext context,
        IPublisher publisher,
        ILogger<ProcessOutboxMessagesJob> logger)
    {
        _dbContext = context;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await
                _dbContext.Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .OrderByDescending(m => m.OccurredOnUtc)
                .Take(20)
                .ToListAsync(context.CancellationToken);

        foreach (var message in messages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            if (domainEvent is null)
            {
                _logger.LogError("Domain event is null.");
                continue;
            }

            try
            {
                await _publisher.Publish(domainEvent, context.CancellationToken);
                message.ProcessedOnUtc = DateTime.UtcNow;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to process domain event {message.Type}");
                message.RetryCount++;
                if (message.RetryCount >= MaxRetries)
                {
                    message.Error = ex.Message.ToString();
                }
            }

            await _dbContext.SaveChangesAsync();
        }

    }
}
