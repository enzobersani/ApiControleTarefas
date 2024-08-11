using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace API.ControleTarefas.Infrastructure.Repositories
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;

        public CollaboratorRepository(ApiControleTarefasDbContext context, INotificationService notification)
        {
            _context = context;
            _notifications = notification;
        }
        public async Task AddAsync(CollaboratorEntity collaborator)
        {
            await _context.Collaborators.AddAsync(collaborator);
        }

        public async Task<CollaboratorEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Collaborators.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetById", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CollaboratorEntity>> GetByName(string name)
        {
            try
            {
                return await _context.Collaborators.AsNoTracking().Where(x => x.Name == name).ToListAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetByName", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }
        public async Task<List<CollaboratorEntity>> GetAllCollaborators()
        {
            try
            {
                return await _context.Collaborators.AsNoTracking().Where(x => x.IsInactive == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetById", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public IQueryable<CollaboratorEntity> Query()
        {
            return _context.Collaborators.AsQueryable();
        }
    }
}
