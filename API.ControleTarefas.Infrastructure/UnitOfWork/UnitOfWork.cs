using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using API.ControleTarefas.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region variables
        private readonly ApiControleTarefasDbContext _context;
        private readonly INotificationService _notifications;
        private IUserRepository _userRepository;
        private IProjectRepository _projectRepository;
        private ITaskRepository _taskRepository;
        private ICollaboratorRepository _collaboratorRepository;
        private ITimeTrackerRepository _timeTrackerRepository;
        #endregion

        #region constructor
        public UnitOfWork(ApiControleTarefasDbContext context, INotificationService notifications)
        {
            _context = context;
            _notifications = notifications;
        }
        #endregion

        #region public methods getters
        public IUserRepository UserRepository
            => _userRepository ??= new UserRepository(_context, _notifications);

        public IProjectRepository ProjectRepository
            => _projectRepository ??= new ProjectRepository(_context, _notifications);

        public ITaskRepository TaskRepository
            => _taskRepository ??= new TaskRepository(_context, _notifications);

        public ICollaboratorRepository CollaboratorRepository
            => _collaboratorRepository ??= new CollaboratorRepository(_context, _notifications);

        public ITimeTrackerRepository TimeTrackerRepository
            => _timeTrackerRepository ??= new TimeTrackerRepository(_context, _notifications);
        #endregion

        #region methods
        public async Task<bool> CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _notifications.AddNotification("CommitAsync", $"Ocorreu um erro ao salvar {ex.ToString()}");
                return false;
            }

            return true;
        }
        #endregion
    }
}
