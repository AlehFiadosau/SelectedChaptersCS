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
    public class GetInspectors : ViewComponent
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInspectors> _logger;

        public GetInspectors(IService<Inspector, int> inspectorService,
            IMapper mapper,
            ILogger<GetInspectors> logger)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var inspectors = await _inspectorService.GetAllAsync();
                var data = _mapper.Map<List<InspectorViewModel>>(inspectors);
                ViewBag.Inspectors = new SelectList(data, "Id", "FirstName");
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new List<InspectorViewModel>();
                ViewBag.Inspectors = new SelectList(data, "Id", "FirstName");
            }

            return View();
        }
    }
}
