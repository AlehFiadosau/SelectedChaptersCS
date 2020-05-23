using AutoMapper;
using BusinessLayer.Ecxeptions;
using BusinessLayer.Entities;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebCarInspection.Models;

namespace WebCarInspection.Components
{
    public class GetInspector : ViewComponent
    {
        private readonly IService<Inspector, int> _inspectorService;
        private readonly IMapper _mapper;

        public GetInspector(IService<Inspector, int> inspectorService,
            IMapper mapper)
        {
            _inspectorService = inspectorService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            try
            {
                var inspector = await _inspectorService.GetByIdAsync(id);
                var data = _mapper.Map<InspectorViewModel>(inspector);

                return View(data);
            }
            catch (NotFoundException)
            {
                var data = new InspectorViewModel();

                return View(data);
            }
        }
    }
}
