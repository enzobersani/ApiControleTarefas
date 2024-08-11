using API.ControleTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml;
using TaskEntity = API.ControleTarefas.Domain.Entities.TaskEntity;

namespace API.ControleTarefas.Infrastructure
{
    public class ApiControleTarefasDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<CollaboratorEntity> Collaborators { get; set; }
        public DbSet<TimeTrackerEntity> TimeTrackers { get; set; }
        public ApiControleTarefasDbContext(DbContextOptions<ApiControleTarefasDbContext> options) : base(options) { }
    }
}
