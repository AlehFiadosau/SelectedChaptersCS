using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCarInspection.Models;

namespace WebCarInspection.Components
{
    public class GetViolation : ViewComponent
    {
        private readonly IService<Violation, int> _violationService;
        private readonly IMapper _mapper;

        public GetViolation(IService<Violation, int> violationService,
            IMapper mapper)
        {
            _violationService = violationService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var violation = await _violationService.GetByIdAsync(id);
                var data = _mapper.Map<ViolationViewModel>(violation);

                return View(data);
            }
            catch (NotFoundException)
            {
                var data = new ViolationViewModel();

                return View(data);
            }
        }
    }
}
