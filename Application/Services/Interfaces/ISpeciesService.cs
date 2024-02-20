using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services.Interfaces
{
    public interface ISpeciesService
    {
        Task<IActionResult> GetSpecies(SpeciesFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetSpecies(Guid id);
        Task<IActionResult> CreateSpecies(SpeciesCreateModel model);
        Task<IActionResult> UpdateSpecies(Guid id, SpeciesUpdateModel model);
    }
}
