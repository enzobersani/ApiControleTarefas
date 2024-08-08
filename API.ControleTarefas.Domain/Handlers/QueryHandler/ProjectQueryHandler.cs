using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using API.ControleTarefas.Domain.Queries;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.ControleTarefas.Domain.Handlers.QueryHandler
{
    public class ProjectQueryHandler : IRequestHandler<SearchProjectQuery, SearchProjectResponseModel>,
                                       IRequestHandler<SearchProjectByIdQuery, ProjectResultModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectQueryHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
        public async Task<SearchProjectResponseModel> Handle(SearchProjectQuery request, CancellationToken cancellationToken)
        {
            List<ProjectEntity> projects;
            if (request.Name is null)
                projects = await _unitOfWork.ProjectRepository.GetAllProjects();
            else
                projects = await _unitOfWork.ProjectRepository.GetByName(request.Name);

            if(_notifications.HasNotifications())
                return new SearchProjectResponseModel();

            if(projects is null || !projects.Any())
                return new SearchProjectResponseModel();

            var projectResultModels = new List<ProjectResultModel>();

            foreach ( var project in projects) 
            {
                projectResultModels.Add(new ProjectResultModel
                {
                    Id = project.Id,
                    Name = project.Name
                });
            }

            var response = new SearchProjectResponseModel
            {
                Items = projectResultModels
            };

            if (!response.TryPage(request.PageSize, request.Page))
                _notifications.AddNotification("Handle", $"Página {request.Page.ToString()} não encontrada!");

            return response;
        }
        public async Task<ProjectResultModel> Handle(SearchProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _unitOfWork.ProjectRepository.GetById(request.Id);
            if (project is null)
                return new ProjectResultModel();

            return new ProjectResultModel
            {
                Id = project.Id,
                Name = project.Name
            };
        }
    }
}
