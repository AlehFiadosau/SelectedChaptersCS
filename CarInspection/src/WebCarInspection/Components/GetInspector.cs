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
    public class GetInspector : ViewComponent
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetInspector> _logger;

        public GetInspector(IService<Inspector, int> inspectorService,
            IMapper mapper,
            ILogger<GetInspector> logger)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var inspector = await _inspectorService.GetByIdAsync(id);
                var data = _mapper.Map<InspectorViewModel>(inspector);

                return View(data);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                var data = new InspectorViewModel();

                return View(data);
            }
        }
    }
}
