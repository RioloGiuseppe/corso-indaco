﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using sample.Data.Entities;
using sample.Data.Models;
using sample.Models;
using sample.Options;
using sample.Services;
using sample.Services.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILogger _loggerCustom;
        private readonly BaseConfig _baseConfig;
        private readonly IDataStorage _dataStorage;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger,
            IOptions<BaseConfig> baseConfig,
            IDataStorage dataStorage,
            ILoggerFactory loggerFactory,
            IMapper mapper)
        {
            _logger = logger;
            _loggerCustom = loggerFactory.CreateLogger("Custom Logger");
            _baseConfig = baseConfig.Value;
            _dataStorage = dataStorage;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(_baseConfig.AppName);
            _logger.LogInformation(_baseConfig.AppName);
            _logger.LogWarning(_baseConfig.AppName);

            _dataStorage.AddUser(new()
            {
                Id = "1",
                FirstName = "Giovanni",
                Lastname = "Rossi",
                Addresses = new()
                {
                    new()
                    {
                        City = "Milano",
                        PostalCode = "0001",
                        Street = "Via Roma"
                    }
                },
                Username = "gio.ross"
            });
            UserModel m = _mapper.Map<UserModel>(_dataStorage.GetUser("1"));

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
