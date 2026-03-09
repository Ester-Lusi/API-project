using AutoMapper;
using Dtos;
using Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories;
using System.Collections.Generic;

namespace Services
{
    public class UserService :IUserService
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IPasswordService _iPasswordService;
        private readonly IMapper _imapper;

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
        public async Task<UserDto> AddUser(UserDto user, string Password)
        {
            if (_iPasswordService.GetPasswordStrength(Password) < 2)
                return null;
            User userDtoToUser = _imapper.Map<UserDto, User>(user);
            User user1 = await _iUserRepository.AddUser(userDtoToUser);
            UserDto userDto = _imapper.Map<User, UserDto>(user1);
            return userDto;
        }
        public async Task<UserDto> FindUser(LoginUser user)
        {
            User res = await _iUserRepository.FindUser(user);
            UserDto userDTO = _imapper.Map<User, UserDto>(res);
            return userDTO;
        }
        public async Task<bool> UpdateUser(int id, UserDto user, string Password)
        {
            int pass = _iPasswordService.GetPasswordStrength(Password);
            if (pass < 2)
                return false;
            User userToUpdate = _imapper.Map<UserDto,User>(user);
            userToUpdate.Id = id;
            userToUpdate.Password = Password;
            await _iUserRepository.UpdateUser(userToUpdate);
            return true;
        }
        public void DeleteUser(int id)
        {
            _iUserRepository.DeleteUser(id);
        }

    }
}
