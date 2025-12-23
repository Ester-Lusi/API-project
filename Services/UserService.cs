using AutoMapper;
using Dtos;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        IUserRepository _iUserRepository;
        IPasswordService _iPasswordService;
        //AutoMapper _imapper;
        IMapper _imapper;

        public UserService(IUserRepository iUserRepository, IPasswordService iPasswordService, IMapper mapper)
        {
            _iUserRepository = iUserRepository;
            _iPasswordService = iPasswordService;
            _imapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            IEnumerable<User> users = await _iUserRepository.GetUsers();
            IEnumerable<UserDto> usersDto = _imapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
            return usersDto;
        }
        public async Task<UserDto> GetById(int id)
        {
            User user = await _iUserRepository.GetById(id);
            UserDto userDto = _imapper.Map<User, UserDto>(user);
            return userDto;
        }
        public async Task<UserDto> AddUser(User user)
        {
            if (_iPasswordService.CheckStrength(user.Password).Strength < 2)
                return null;
            User user1 = await _iUserRepository.AddUser(user);
            UserDto userDto = _imapper.Map<User, UserDto>(user1);
            return userDto;
        }
        public async Task<User> FindUser(User user)
        {
            return await _iUserRepository.FindUser(user);
        }
        public async Task<int> UpdateUser(int id, User user)
        {
            var result = Zxcvbn.Core.EvaluatePassword(user.Password);
            if (result.Score > 2)
                _iUserRepository.UpdateUser(id, user);
            return result.Score;
        }
        public void DeleteUser(int id)
        {
            _iUserRepository.DeleteUser(id);
        }

    }
}
