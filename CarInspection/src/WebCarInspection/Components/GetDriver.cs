using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetDriver : ViewComponent
    {
        private readonly IService<Driver, int> _driverService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDriver> _logger;

        public GetDriver(IService<Driver, int> driverService,
            IMapper mapper,
            ILogger<GetDriver> logger)
        {
            _driverService = driverService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var driver = await _driverService.GetByIdAsync(id);
                var data = _mapper.Map<DriverViewModel>(driver);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new DriverViewModel();

                return View(data);
            }
        }
    }
}
