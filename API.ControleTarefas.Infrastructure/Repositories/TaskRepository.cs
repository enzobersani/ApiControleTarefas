using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System;

namespace API.ControleTarefas.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;

        public TaskRepository(ApiControleTarefasDbContext context, INotificationService notification)
        {
            _context = context;
            _notifications = notification;
        }

        public async Task AddAsync(TaskEntity task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public void Update(TaskEntity task)
        {
            _context.Tasks.Update(task);
        }

        public async Task<TaskEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetById", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public IQueryable<TaskEntity> Query()
        {
            try
            {
                return _context.Tasks.AsQueryable();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("Query", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }    
    }
}
