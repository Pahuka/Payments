﻿using Domain.Entities;

namespace Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
	Task<User> Get(string login);
}