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
    public class GetViolation : ViewComponent
    {
        private readonly IService<Violation, int> _violationService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetViolation> _logger;

        public GetViolation(IService<Violation, int> violationService,
            IMapper mapper,
            ILogger<GetViolation> logger)
        {
            _violationService = violationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var violation = await _violationService.GetByIdAsync(id);
                var data = _mapper.Map<ViolationViewModel>(violation);
                ViewBag.Name = data.Name;

                return View();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return View();
            }
        }
    }
}
