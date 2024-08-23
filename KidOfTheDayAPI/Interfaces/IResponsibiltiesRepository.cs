﻿using KidOfTheDayAPI.Models;

namespace KidOfTheDayAPI.Interfaces
{
    public interface IResponsibiltiesRepository
    {
        public Task<List<Responsibility>> GetKidsResponsibilities(int kidId);
        public Task AddResponsibility(Responsibility responsibility);
    }
}
