using API.ControleTarefas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Task = API.ControleTarefas.Domain.Entities.Task;

namespace API.ControleTarefas.Infrastructure
{
    public class ApiControleTarefasDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public ApiControleTarefasDbContext(DbContextOptions<ApiControleTarefasDbContext> options) : base(options) { }
    }
}
