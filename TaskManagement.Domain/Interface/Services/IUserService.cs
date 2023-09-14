﻿using TaskManagement.Domain.Dtos.User;

namespace TaskManagement.Domain.Interface.Services;

public interface IUserService
{
    Task<Guid> CreateUserAsync(UserCreate addressCreate);
}