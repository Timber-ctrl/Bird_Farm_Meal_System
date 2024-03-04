using Domain.Models.Creates;
using Domain.Models.Filters;
using Domain.Models.Pagination;
using Domain.Models.Updates;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUnitOfMeasurementService
    {
        Task<IActionResult> GetUnitOfMeasurements(UnitOfMeasurementFilterModel filter, PaginationRequestModel pagination);
        Task<IActionResult> GetUnitOfMeasurements(Guid id);
        Task<IActionResult> CreateUnitOfMeasurements(UnitOfMeasurementCreateModel model);
        Task<IActionResult> UpdateUnitOfMeasurements(Guid id, UnitOfMeasurementUpdateModel model);
    }
}
