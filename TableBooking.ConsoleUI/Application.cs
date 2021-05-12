﻿using Core.Entities;
using Core.UseCases;
using System;

namespace TableBooking.ConsoleUI
{
    public class Application : IApplication
    {
        private readonly RestaurantsInteractor _restaurantsInteractor;
        private readonly UserAuthorizationInteractor _userAuthorizationInteractor;

        public Application(RestaurantsInteractor restaurantsInteractor, UserAuthorizationInteractor userAuthorizationInteractor)
        {
            _restaurantsInteractor = restaurantsInteractor;
            _userAuthorizationInteractor = userAuthorizationInteractor;
        }

        public void Run()
        {
            _userAuthorizationInteractor.Register("gp", "1488");
            var restaurants = _restaurantsInteractor.GetAllRestaurants();
            foreach (var item in restaurants)
            {
                Console.WriteLine($"{item.Name} {item.OpenedFrom}-{item.OpenedTill}");
            }
            _restaurantsInteractor.AddRestaurant(
                new RestaurantEntity
                {
                    Name = "dom s prikolom",
                    Address = "prikolni address",
                    City = "gorod prikolov",
                    OpenedFrom = new TimeSpan(4, 20, 0),
                    OpenedTill = new TimeSpan(22, 8, 0)
                }
            );

            Console.WriteLine("\nnovi restaran atkrilsa((\n");

            restaurants = _restaurantsInteractor.GetAllRestaurants();
            foreach (var item in restaurants)
            {
                Console.WriteLine($"{item.Name} {item.OpenedFrom}-{item.OpenedTill}");
            }
        }
    }
}