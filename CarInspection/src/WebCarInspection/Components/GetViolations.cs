using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetViolations : ViewComponent
    {
        private readonly IService<Violation, int> _violationService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetViolations> _logger;

        public GetViolations(IService<Violation, int> violationService,
            IMapper mapper,
            ILogger<GetViolations> logger)
        {
            _violationService = violationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var violations = await _violationService.GetAllAsync();
                var data = _mapper.Map<List<ViolationViewModel>>(violations);
                ViewBag.Violations = new SelectList(data, "Id", "Name");
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<ViolationViewModel>();
                ViewBag.Violations = new SelectList(data, "Id", "Name");
            }

            return View();
        }
    }
}
