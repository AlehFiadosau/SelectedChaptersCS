using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebCarInspection.ViewModels;

namespace WebCarInspection.Components
{
    public class GetViolations : ViewComponent
    {
        private readonly IService<Violation, int> _violationService;
        private readonly IMapper _mapper;

        public GetViolations(IService<Violation, int> violationService,
            IMapper mapper)
        {
            _violationService = violationService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var violations = await _violationService.GetAllAsync();
                var data = _mapper.Map<List<ViolationViewModel>>(violations);
                ViewBag.Violations = new SelectList(data, "Id", "Name");
            }
            catch (NotFoundException)
            {
                var data = new List<ViolationViewModel>();
                ViewBag.Violations = new SelectList(data, "Id", "Name");
            }

            return View();
        }
    }
}
