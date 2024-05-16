using Domain.Entities;

namespace Common.Helpers
{
    public static class PlanHelper
    {
        private static IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }

        public static ICollection<PlanDetail> GeneratePlanDetail(Plan plan)
        {
            try
            {
                var planDetails = new List<PlanDetail>();
                foreach (DateTime date in EachDay(plan.From, plan.To))
                {
                    var planDetail = new PlanDetail
                    {
                        Id = Guid.NewGuid(),
                        Date = date,
                        PlanId = plan.Id,
                        Status = false
                    };
                    planDetails.Add(planDetail);
                }
                return planDetails;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
