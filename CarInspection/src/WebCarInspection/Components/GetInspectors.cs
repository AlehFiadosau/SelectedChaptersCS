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
    public class GetInspectors : ViewComponent
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;

        public GetInspectors(IService<Inspector, int> inspectorService,
            IMapper mapper)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var inspectors = await _inspectorService.GetAllAsync();
                var data = _mapper.Map<List<InspectorViewModel>>(inspectors);
                ViewBag.Inspectors = new SelectList(data, "Id", "FirstName");
            }
            catch (NotFoundException)
            {
                var data = new List<InspectorViewModel>();
                ViewBag.Inspectors = new SelectList(data, "Id", "FirstName");
            }

            return View();
        }
    }
}
