using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Task = System.Threading.Tasks.Task;

namespace API.ControleTarefas.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;

        public ProjectRepository(ApiControleTarefasDbContext context, INotificationService notification)
        {
            _context = context;
            _notifications = notification;
        }
        public async Task AddAsync(ProjectEntity project)
        {
            await _context.Projects.AddAsync(project);
        }

        public void Update(ProjectEntity project)
        {
            _context.Projects.Update(project);
        }

        public async Task<List<ProjectEntity>> GetByName(string name)
        {
            try
            {
                return await _context.Projects.AsNoTracking().Where(x => x.Name.ToUpper() == name.ToUpper() && x.IsInactive == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetByName", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public async Task<List<ProjectEntity>> GetAllProjects()
        {
            try
            {
                return await _context.Projects.AsNoTracking().Where(x => x.IsInactive == false).ToListAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetByName", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public async Task<ProjectEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.IsInactive == false);
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetById", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }
    }
}
