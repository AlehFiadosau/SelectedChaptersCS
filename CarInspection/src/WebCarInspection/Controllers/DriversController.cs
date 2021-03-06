﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WebCarInspection.Core;
using WebCarInspection.Interfaces;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Controllers
{
    [Authorize(Roles = RoleNames.User)]
    public class DriversController : Controller
    {
        private readonly IApiClientHelper _client;

        public DriversController(IApiClientHelper client)
        {
            _client = client;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ShowDrivers()
        {
            var result = await _client.GetAsync("drivers");
            var content = await _client.ReadAsJsonAsync<List<DriverViewModel>>(result);

            return View(content);
        }

        [HttpGet]
        public IActionResult CreateDriver()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> CreateDriver(DriverViewModel driver)
        {
            if (!ModelState.IsValid)
            {
                return CreateDriver(driver);
            }

            return CreateDriverInternal(driver);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateDriver(int id)
        {
            var result = await _client.GetAsync($"drivers/{id}");
            var data = await _client.ReadAsJsonAsync<DriverViewModel>(result);

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateDriver(DriverViewModel driver)
        {
            var result = await _client.PutAsync("drivers", driver);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowDrivers));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return await UpdateDriver(driver);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteDriver(int id)
        {
            await _client.DeleteAsync($"drivers/{id}");

            return RedirectToAction(nameof(ShowDrivers));
        }

        private async Task<IActionResult> CreateDriverInternal(DriverViewModel driver)
        {
            var result = await _client.PostAsync("drivers", driver);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return RedirectToAction(nameof(ShowDrivers));
            }
            else
            {
                var exMessage = await result.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, exMessage);

                return CreateDriver();
            }
        }
    }
}