using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Notification;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.ControleTarefas.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;

        public UserRepository(ApiControleTarefasDbContext context, INotificationService notification)
        {
            _context = context;
            _notifications = notification;
        }
        public async Task AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<UserEntity> GetByUserName(string userName)
        {
            try
            {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserName.Trim() == userName.Trim());
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetByUserName", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }

        public async Task<UserEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("GetById", $"Ocorreu um erro: {ex.Message}");
                return null;
            }
        }
    }
}
