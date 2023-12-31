﻿using Domain.Entities.Abstract;

namespace Domain.Entities;

public class HVS : EntityBase
{
	public double CurrentValue { get; set; }
	public double TotalResult { get; set; }
	public User? User { get; set; }
	public Guid UserId { get; set; }
}