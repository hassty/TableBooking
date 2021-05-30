using Core.Contracts.DataAccess;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.UseCases
{
    public class CancelRestaurantChanges
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CancelRestaurantChanges(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public void Cancel()
        {
            _restaurantRepository.Rollback();
            _restaurantRepository.SaveChanges();
        }
    }
}