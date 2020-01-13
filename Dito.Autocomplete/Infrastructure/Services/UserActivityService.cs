using Dito.Autocomplete.Infrastructure.Repositories;
using Dito.Autocomplete.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dito.Autocomplete.Infrastructure.Services
{
    public interface IUserActivityService
    {
        Task<IEnumerable<UserActivity>> GetAll();
        Task<UserActivity> GetById(string id);
        Task<string> Create(UserActivityRequest request);
    }

    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _userActivityRepository;

        public UserActivityService(IUserActivityRepository userActivityRepository)
        {
            _userActivityRepository = userActivityRepository;
        }

        public async Task<IEnumerable<UserActivity>> GetAll()
        {
            return await _userActivityRepository.GetAll();
        }

        public async Task<UserActivity> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("O id de pesquisa da atividade de usuário não pode ser nulo ou vazio.");

            return await _userActivityRepository.GetById(id);
        }

        public async Task<string> Create(UserActivityRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("O objeto de atividade de usuário não pode ser nulo.");

            if (string.IsNullOrEmpty(request.Event))
                throw new ArgumentException("O campo event é obrigatório.");

            if (string.IsNullOrEmpty(request.TimeStamp))
                throw new ArgumentException("O campo time stamp é obrigatório.");

            return await _userActivityRepository.Create(new UserActivity(request.Event, request.TimeStamp));
        }
    }
}
