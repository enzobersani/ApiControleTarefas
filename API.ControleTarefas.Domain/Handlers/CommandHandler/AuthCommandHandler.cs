using API.ControleTarefas.Domain.Commands;
using API.ControleTarefas.Domain.Entities;
using API.ControleTarefas.Domain.Interfaces.Repositories;
using API.ControleTarefas.Domain.Interfaces.UnitOfWork;
using API.ControleTarefas.Domain.Models.Response;
using API.ControleTarefas.Domain.Notification;
using BCrypt.Net;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.ControleTarefas.Domain.Handlers.CommandHandler
{
    public class AuthCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponseModel>,
                                      IRequestHandler<LoginCommand, RegisterUserResponseModel>
    {
        private readonly INotificationService _notifications;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthCommandHandler(INotificationService notifications, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _notifications = notifications;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        #region Public Methods
        public async Task<RegisterUserResponseModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await _unitOfWork.UserRepository.GetByUserName(request.UserName);
            if (userExist is not null)
            {
                _notifications.AddNotification("UserName", "UserName informado já cadastrado!");
                return new RegisterUserResponseModel();
            }

            var hashPassword = HashPassword(request.Password);

            var user = User.New(request, hashPassword);
            user.SetCreationDate();

            await _unitOfWork.UserRepository.AddAsync(user);

            await _unitOfWork.CommitAsync();

            var token = GenerateToken(user.Id, request.UserName);

            return new RegisterUserResponseModel
            {
                Token = token
            };
        }

        public async Task<RegisterUserResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.UserRepository.GetByUserName(request.UserName);
            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                _notifications.AddNotification("Unauthorized", "Nome de usuário ou senha inválidos!");
                return new RegisterUserResponseModel();
            }

            var token = GenerateToken(user.Id, request.UserName);
            return new RegisterUserResponseModel
            {
                Token = token
            };
        }
        #endregion


        #region Private Methods
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private string GenerateToken(Guid id, string userName)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("userName", userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(60);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
